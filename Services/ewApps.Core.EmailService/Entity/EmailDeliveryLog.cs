using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ewApps.Core.EmailService {
    [Table("EmailDeliveryLog", Schema = "core")]
    public class EmailDeliveryLog {

        // Entity name as a string.
        public const string EntityName = "EmailDeliveryLog";

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailDeliveryLog"/> class.
        /// </summary>
        public EmailDeliveryLog() {
        }

        [Key]
        public Guid ID {
            get; set;
        }

        [Required]
        public int DeliveryType {
            get; set;
        }

        public int DeliverySubType {
            get; set;
        }

        [Required]
        [MaxLength(4000)]
        public string MessagePart1 {
            get; set;
        }

        [MaxLength(4000)]
        public string MessagePart2 {
            get; set;
        }


        [Required]
        [MaxLength(100)]
        public string Recipient {
            get; set;
        }

        [MaxLength(100)]
        public string CC {
            get; set;
        }

        [MaxLength(100)]
        public string BCC {
            get; set;
        }

        [MaxLength(100)]
        public string ReplyTo {
            get; set;
        }

        [MaxLength(100)]
        public string Sender {
            get; set;
        }

        [MaxLength(100)]
        public string SenderName {
            get; set;
        }

        [Required]
        [MaxLength(100)]
        public string SenderKey {
            get; set;
        }

        [Required]
        public DateTime ScheduledDeliveryTime {
            get; set;
        }

        [Required]
        public DateTime ActualDeliveryTime {
            get; set;
        }

        //[Required]
        //public int State {
        //    get; set;
        //}

        public Guid ApplicationId {
            get; set;
        }

        public Guid NotificationId {
            get; set;
        }

        public Guid CreatedBy {
            get; set;
        }

        public DateTime CreatedOn {
            get; set;
        }

        public bool Deleted {
            get; set;
        } = false;

    }
}
