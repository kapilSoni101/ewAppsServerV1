using System.IO;

namespace ewApps.Core.DMService {
    public class FileData
    {

        /// <summary>
        /// Name of the the file.
        /// </summary>
        public string FileName = "";

        /// <summary>
        /// Physical stored filepath.
        /// </summary>
        internal string FilePath = "";

        /// <summary>
        /// Mimetype(contenttype) of file.
        /// </summary>
        public string MimeType;

    }

}
