using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.DTO {
    public class ErrorLogEmailDTO {
        public string ErrorDate {
            get; set;
        }
        public string MethodName {
            get; set;
        }
        public string ErrorMessage {
            get; set;
        }
        public string StackTrace {
            get; set;
        }
        public string LineNo {
            get; set;
        }
        public string ApplicationName {
            get; set;
        }
        public string UserId {
            get; set;
        }
        public string UserName {
            get; set;
        }
        public string TenantId {
            get; set;
        }
        public string TenantName {
            get; set;
        }
        public string Browser {
            get; set;
        }
        public string BrowserVersion {
            get; set;
        }
        public string OSName {
            get; set;
        }
        public string OSVersion {
            get; set;
        }
        public string Device {
            get; set;
        }
        public string IpAddress {
            get; set;
        }
        public string AppVersion {
            get;
            set;
        }

        public string EwpErrorString {
            get; set;
        }

        public string PortalAppKey {
            get; set;
        }

        public Tuple<string, string> GetErrorEmailDetail() {
            StringBuilder errorEmailBuilder = new StringBuilder();

            errorEmailBuilder.AppendFormat("<b>Error Date (Client):</b> {0}<br><br>", this.ErrorDate);

            errorEmailBuilder.AppendFormat("<b>Log Date (Server):</b> {0} ({1})<br><br>", DateTime.Now.ToString(), System.TimeZoneInfo.Local.DisplayName);

            errorEmailBuilder.AppendFormat("<b>Application Name:</b> {0}<br><br>", this.ApplicationName);

            errorEmailBuilder.AppendFormat("<b>User Name:</b> {0} [{1}]<br><br>", this.UserName, this.UserId);

            errorEmailBuilder.AppendFormat("<b>Tenant Name:</b> {0} [{1}]<br><br>", this.TenantName, this.TenantId);

            errorEmailBuilder.AppendFormat("<b>Device:</b> {0}<br><br>", this.Device);

            errorEmailBuilder.AppendFormat("<b>Browser Name:</b> {0}<br><br>", this.Browser);

            errorEmailBuilder.AppendFormat("<b>Browser Version:</b> {0}<br><br>", this.BrowserVersion);

            errorEmailBuilder.AppendFormat("<b>OS:</b> {0}<br><br>", this.OSName);

            errorEmailBuilder.AppendFormat("<b>OS Version:</b> {0}<br><br>", this.OSVersion);

            errorEmailBuilder.AppendFormat("<b>Application Version:</b> {0}<br><br>", this.AppVersion);

            errorEmailBuilder.AppendFormat("<b>Module:</b> {0}<br><br>", "");

            errorEmailBuilder.AppendFormat("<b>Entity:</b> {0}<br><br>", "");

            errorEmailBuilder.AppendFormat("<b>Error Message:</b> {0}<br><br>", this.ErrorMessage);

            errorEmailBuilder.AppendFormat("<b>Method Name:</b> {0}<br><br>", this.MethodName);

            errorEmailBuilder.AppendFormat("<b>Strack Trace:</b> {0}<br><br>", this.StackTrace);

            errorEmailBuilder.AppendFormat("<b>Ewp Error:</b> {0}<br><br>", this.EwpErrorString);
            if(string.IsNullOrEmpty(this.PortalAppKey)) {
                return new Tuple<string, string>("[WC] Error Occurred", errorEmailBuilder.ToString());
            }
            else {
                return new Tuple<string, string>(string.Format("[WC-{0}] Error Occurred", PortalAppKey), errorEmailBuilder.ToString());
            }
        }
    }
}
