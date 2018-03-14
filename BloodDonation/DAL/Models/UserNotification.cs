namespace DAL.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class UserNotification
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [ForeignKey("Notification")]
        [Required]
        public int Notification_Id { get; set; }
        public virtual Notification Notification { get; set; }
        [ForeignKey("User")]
        [Required]
        public string User_Id { get; set; }
        public virtual User User { get; set; }
    }
}
