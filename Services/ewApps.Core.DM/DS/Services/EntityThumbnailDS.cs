/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar<abadgujar@batchmaster>
 * Date: 22 August 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 22 August 2019
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;
using Microsoft.Extensions.Options;


namespace ewApps.Core.DMService {

    /// <summary>
    /// Represents all the operations to be performed on thumbnail entity.
    /// </summary>
    public class EntityThumbnailDS : BaseDS<EntityThumbnail>, IEntityThumbnailDS {

    #region member varaible
 
    IEntityThumbnailRepository _thumbnailRep;
        // IMapper _mapper;
        //IUnitOfWork _unitOfWork;
        DMServiceSettings _thumbnailUrl;
    IUserSessionManager _sessionManager;

    #endregion

    #region Constructor

    /// <summary>
    /// Initialinzing local variables
    /// </summary>
    /// <param name="thumbnailRep"></param>
    /// <param name="cacheService"></param>
    /// <param name="mapper"></param>
    /// <param name="unitOfWork"></param>
    /// <param name="sessionManager"></param>
    /// <param name="appSetting"></param>
    public EntityThumbnailDS(IEntityThumbnailRepository thumbnailRep, IUserSessionManager sessionManager,IOptions<DMServiceSettings> appSetting) : base(thumbnailRep) {
      _thumbnailRep = thumbnailRep;
     // _mapper = mapper;
     
      _thumbnailUrl = appSetting.Value;
      _sessionManager = sessionManager;
    }

    #endregion Constructor

    #region IThumbnailDataService Members

    /// <summary>
    /// Add Thumbnail in Physical Location 
    /// </summary>
    /// <param name="thumbnailAddDTO"></param>
    /// <returns></returns>
    public ThumbnailIdDetailDTO AddThumbnail(ThumbnailAddAndUpdateDTO thumbnailAddDTO) {
      ThumbnailIdDetailDTO thumbnailIdDetailDTO = new ThumbnailIdDetailDTO();
      UserSession us = _sessionManager.GetSession();
      Guid tenantId = us.TenantId;

      if (thumbnailAddDTO != null) {
                //Get thumbnail properties.
                EntityThumbnail thumbnail = new EntityThumbnail();
                //if(thumbnailAddDTO.ThumbnailOwnerEntityType == (int)CoreEntityTypeEnum.TenantUser) {
                //  thumbnail.TenantId = Guid.Empty;
                //}
                //else {
                //  thumbnail.TenantId = us.TenantId;
                //}
                thumbnail.TenantId = tenantId;

                string modDate = DateTime.UtcNow.ToString();
          string fname = Regex.Replace(modDate, "[^b-lnoq-zB-LNOQ-Z0-9]+", "", RegexOptions.Compiled);
          string fileName = fname + ".jpg";
          thumbnail.FileExtension = ".jpg"; //FileHelper.GetFileExtention(thumbnailAddModel.ThumbnailFileName);
                                            // thumbnail.TenantId = new Guid();

          // Generate thumbnail id
          Guid thumbnailId = Guid.NewGuid();
          if (thumbnailAddDTO.ID != Guid.Empty && thumbnailAddDTO.ID != null) {
            thumbnailId = thumbnailAddDTO.ID;
          }
         // thumbnail.ThumbnailId = thumbnailId;
          // thumbnail.ModifiedDate = Convert.ToDateTime(modDate);

          thumbnailAddDTO.ID = thumbnailId;

          // Save thumbnail on physical location 
          bool thumbSaved = ThumbnailDefaultImageSaveToPhysicalLocation(thumbnailAddDTO.ID, thumbnailAddDTO.ThumbnailBase64String, fileName, thumbnail.TenantId);

          if (thumbSaved) {

            //Map thumbnail add model to thumbnail.
            thumbnailAddDTO.MapToThumbnail(thumbnail);

            thumbnail.FileName = fileName;
            //thumbnail.DocumentId = thumbnailAddDTO.DocumentId;
            thumbnail = Add(thumbnail);
            thumbnailIdDetailDTO.ThumbnailFileName = fileName;

            // Commiting all changes in database.
            _thumbnailRep.Save();
          }

          else {
            //Exception Handling 
          }
          return thumbnailIdDetailDTO;
        }

      
      return null;
    }

    ///<inheritdoc/>
    public ThumbnailIdDetailDTO UpdateThumbnail(ThumbnailAddAndUpdateDTO thumbnailUpdateModel) {
      //Create thumbnail detail and thumbnail Id detail model objects.
      ThumbnailIdDetailDTO thumbnailIdDetailModel = new ThumbnailIdDetailDTO();

            //Get thumbnail entity and check concurrency.
            EntityThumbnail thumbnail = Get(thumbnailUpdateModel.ID);
        if (thumbnail == null) {
          var listOfKeysAndValues = new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string,string>("Name", "Thumbnail"),
                new KeyValuePair<string,string>("Value", Convert.ToString(thumbnailUpdateModel.ID))
               };         
        }
        string modDate = DateTime.UtcNow.ToString();
        string fname = Regex.Replace(modDate, "[^b-lnoq-zB-LNOQ-Z0-9]+", "", RegexOptions.Compiled);
        string newfileName = fname + ".jpg";
        

        //update thumbnail to Physical Location
        bool thumbUpdated = ThumbnailUpdateToPhysicalLocation(thumbnailUpdateModel.ID, thumbnailUpdateModel.ThumbnailBase64String, thumbnail.FileName, newfileName, modDate , thumbnail.TenantId);

        if (thumbUpdated) {

          thumbnailUpdateModel.MapFromThumbnail(thumbnail);

          thumbnail.FileName = newfileName;
          //thumbnail.ModifiedDate = Convert.ToDateTime(modDate);

          //Update thumbnail.
          Update(thumbnail , thumbnail.ID);
          //Assign values to thumbnail Id detail model.
          thumbnailIdDetailModel.ID = thumbnail.ID;
                //thumbnailIdDetailModel.ThumbnailModifiedDate = thumbnail.ModifiedDate;

                // Commiting all changes in database.
                _thumbnailRep.Save();

                //Return thumbnailId detail model.
                return thumbnailIdDetailModel;

        }
        else {        
        }
        return null;
      
    }

    /// <inheritdoc />
    public void DeleteThumbnail(Guid thumbnailId) {

            EntityThumbnail thumbnail = Get(thumbnailId);
      if (thumbnail == null) {
        var listOfKeysAndValues = new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string,string>("Name", "Thumbnail"),
                new KeyValuePair<string,string>("Value", Convert.ToString(thumbnailId))
               };
      }
      else {
        //Delete the thumbnail.                 
        base.Delete(thumbnail);

                // Commiting all changes in database.
                _thumbnailRep.Save();
            }
      
    }

    /// <summary>
    /// Thumbnails the save to physical location.
    /// </summary>
    /// <param name="thumbnailId">The thumbnail identifier.</param>
    /// <param name="base64String">The base64 string.</param>
    /// <param name="fileName">Name of the file.</param>
    public bool ThumbnailDefaultImageSaveToPhysicalLocation(Guid thumbnailId, string base64String, string fileName , Guid tenantId) {

     

        // MessageLogger.Instance.LogMessage("ThumbnailDefaultImageSaveToPhysicalLocation", LoggerCategory.Production, null, false);

       
          UserSession us = _sessionManager.GetSession();

      // TenantId
      //Guid tenantId = us.TenantId; // EwAppSessionManager.GetSession().TenantId;

        // Save the file to physical location with Guid directory

        string filePath = Path.Combine(_thumbnailUrl.ThumbnailRootPath, tenantId.ToString(), thumbnailId.ToString());

        // Create thumbnail directory
        if (!Directory.Exists(filePath))
          Directory.CreateDirectory(filePath);

        // File full path
        string fileFullPath = Path.Combine(filePath, fileName);

        byte[] imageBytes = Convert.FromBase64String(base64String);
        MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);

        // Convert byte[] to Image
        ms.Write(imageBytes, 0, imageBytes.Length);
        Image image = Image.FromStream(ms, true);

        image.Save(fileFullPath);

        image.Dispose();
        ms.Dispose();
        imageBytes = null;
     
      return true;
    }

    /// <summary>
    /// Thumbnails the update to physical location.
    /// </summary>
    /// <param name="thumbnailId">The thumbnail identifier.</param>
    /// <param name="base64String">The base64 string.</param>
    /// <param name="oldfileName">Name of the oldfile.</param>
    /// <param name="newfilename">The newfilename.</param>
    /// <param name="modDate">The mod date.</param>
    /// <returns></returns>
    public bool ThumbnailUpdateToPhysicalLocation(Guid thumbnailId, string base64String, string oldfileName, string newfilename, string modDate ,Guid tenantId) {

      // TenantId
      // Guid tenantId = EwAppSessionManager.GetSession().TenantId;

      UserSession us = _sessionManager.GetSession();

     // Guid tenantId = us.TenantId; // EwAppSessionManager.GetSession().TenantId;

        // Save the file to physical location with Guid directory
        string filePath = Path.Combine(_thumbnailUrl.ThumbnailRootPath, tenantId.ToString(), thumbnailId.ToString());

        // Create thumbnail directory
        if (Directory.Exists(filePath)) {

        }
        else {
          Directory.CreateDirectory(filePath);
        }

        // File full path
        string fileFullPath = Path.Combine(filePath, newfilename);

        //Create byte[] from base64String
        byte[] imageBytes = Convert.FromBase64String(base64String);
        MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);

        // Convert byte[] to Image
        ms.Write(imageBytes, 0, imageBytes.Length);

        Image image = Image.FromStream(ms, true);
        
        Bitmap bmSave = new Bitmap(image);
        Bitmap bmTemp = new Bitmap(bmSave);

        Graphics grSave = Graphics.FromImage(bmTemp);
        grSave.Clear(Color.White);
        grSave.DrawImage(image, 0, 0, image.Width, image.Height);
        bmTemp.Save(fileFullPath, System.Drawing.Imaging.ImageFormat.Jpeg);

        image.Dispose();
        bmSave.Dispose();
        bmTemp.Dispose();
        grSave.Dispose();
     
      return true;
    }

    ///<inheritdoc/>
    public ThumbnailAddAndUpdateDTO GetThumbnailInfoByOwnerEntityId(Guid Id) {
      ThumbnailAddAndUpdateDTO thumbDTO = new ThumbnailAddAndUpdateDTO();

            //UserSession us = _sessionManager.GetSession();
            //Guid tenantId = us.TenantId;
            EntityThumbnail thumbnail = _thumbnailRep.GetThumbnailByOwnerEntityId(Id);
      if (thumbnail != null) {
        //thumbDTO = _mapper.Map<ThumbnailAddAndUpdateDTO>(thumbnail);
        thumbDTO.MapFromThumbnail(thumbnail);
        // thumbDTO.ThumbnailUrl = (_thumbnailUrl.ThumbnailUrl + thumbnail.TenantId.ToString() + "/" + thumbnail.ID + "/" + thumbnail.FileName);
        thumbDTO.ThumbnailUrl = Path.Combine(_thumbnailUrl.ThumbnailUrl, thumbnail.TenantId.ToString(), thumbnail.ID.ToString(), thumbnail.FileName);
        return thumbDTO;
      }
      else {
        return thumbDTO = null;
           }      
          }

    #endregion IThumbnailDataService Members

  }
}
