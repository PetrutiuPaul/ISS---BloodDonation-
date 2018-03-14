namespace DAL.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Notification
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [ForeignKey("Request")]
        [Required]
        public int Request_Id { get; set; }
        public virtual Request Request { get; set; }
        public virtual ICollection<UserNotification> UserNotifications { get; set; }
    }
}
