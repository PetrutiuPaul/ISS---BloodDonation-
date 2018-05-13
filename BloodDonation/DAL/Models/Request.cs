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
        public ProductType ProductType { get; set; }

        [Required]
        public Priority Priority { get; set; }

        public BloodType BloodType { get; set; }

        public RhType Rh { get; set; }

        public float Amount { get; set; }

        public virtual User User { get; set; }

        [Required]
        [ForeignKey("User")]
        public string User_Id { get; set; }
    }
}
