using System.IO;

namespace ewApps.Core.DMService {
    public class FileResultData
    {

        /// <summary>
        /// Name of the the file.
        /// </summary>
        public string FileName = "";

        /// <summary>
        /// It is url of a file to view into the browser.
        /// </summary>
        public string FileUrl = "";

        /// <summary>
        /// If file can be seen by hitting the url then this property will mark as true.
        /// </summary>
        public bool UseUrl = false;

        /// <summary>
        /// Mimetype(contenttype) of file.
        /// </summary>
        public string MimeType;

        /// <summary>
        /// Stream of stored file.
        /// </summary>
        public FileStream FileStream;

    }

}
