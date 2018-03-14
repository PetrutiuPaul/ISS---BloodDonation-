namespace DAL.Models
{
    using Common.Enums;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Request
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string CNP { get; set; }

        [Required]
        public string PatientName { get; set; }

        [Required]
        public Priority Priority { get; set; }

        [Required]
        public BloodType BloodType { get; set; }

        [Required]
        public RhType Rh { get; set; }

        public virtual User User { get; set; }

        [Required]
        [ForeignKey("User")]
        public string User_Id { get; set; }
    }
}
