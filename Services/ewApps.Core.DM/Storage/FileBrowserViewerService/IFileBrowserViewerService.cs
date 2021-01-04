using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Core.DMService {
    public interface IFileBrowserViewerService {

        /// <summary>
        /// return FileStorage information required for showing the file in browser.
        /// </summary>
        /// <param name="storageId">Unique id of storaed file.</param>
        /// <param name="storageProviderType">Storaed file location.</param>
        /// <returns></returns>
        FileResultData GetFileInfo(Guid storageId, Dictionary<string, object> strResult);

    }
}
