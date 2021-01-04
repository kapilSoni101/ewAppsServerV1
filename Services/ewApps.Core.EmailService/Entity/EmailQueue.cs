using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ewApps.Core.EmailService {
    /// <summary>
    /// This structure logs each system or user generated EmailQueue entity in database.
    /// </summary>
    [Table("EmailQueue", Schema = "core")]
    public class EmailQueue {

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
        public DateTime DeliveryTime {
            get; set;
        }

        [Required]
        public int State {
            get; set;
        }

        public Guid ApplicationId {
            get; set;
        }
        // ToDo: Change NotificationId -> LinkNotificationId
        public Guid NotificationId {
            get; set;
        }

        public Guid CreatedBy {
            get; set;
        }

        public DateTime CreatedOn {
            get; set;
        }

        public Guid UpdatedBy {
            get; set;
        }

        public DateTime UpdatedOn {
            get; set;
        }

        public bool Deleted {
            get; set;
        } = false;

    }
}