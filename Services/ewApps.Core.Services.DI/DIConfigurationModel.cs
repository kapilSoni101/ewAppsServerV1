namespace ewApps.Core.Services.DI {
    /// <summary>
    /// A model contain configuration properties.
    /// </summary>
    public class DIConfigurationModel {

        /// <summary>
        /// The mask value of core services.
        /// </summary>
        public CoreServiceEnum IncludedCoreServices {
            get; set;
        } = 0;

        /// <summary>
        /// It indicates whether [add data service dependencies].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [add data service dependencies]; otherwise, <c>false</c>.
        /// </value>
        public bool AddDSDependencies {
            get; set;
        }

        /// <summary>
        /// It indicates whether [add data dependencies].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [add data dependencies]; otherwise, <c>false</c>.
        /// </value>
        public bool AddDataDependencies {
            get; set;
        }

        /// <summary>
        /// It indicates whether [add other dependencies].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [add other dependencies]; otherwise, <c>false</c>.
        /// </value>
        public bool AddOtherDependencies {
            get; set;
        }

        /// <summary>
        /// It indicates whether [add report dependencies].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [add report dependencies]; otherwise, <c>false</c>.
        /// </value>
        public bool AddReportDependencies {
            get; set;
        }

        /// <summary>
        /// It indicates whether [add application settings].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [add application settings]; otherwise, <c>false</c>.
        /// </value>
        public bool AddAppSettings {
            get; set;
        }

        public bool CoreServiceMask {
            get; set;
        } = false;

        public bool ApplyWebApiConfiguration {
            get; set;
        } = false;

        public bool InitializeBaseConfiguration {
            get; set;
        } = true;
    }

}
