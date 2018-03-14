namespace DAL.Models
{
    using Common.Enums;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Donation
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public double Amount { get; set; }

        [Required]
        public BloodType BloodType { get; set; }

        [Required]
        public RhType RhType { get; set; }

        [Required]
        public Stage Succesfull { get; set; }

        [Required]
        public string DenialReason { get; set; }

        [ForeignKey("User")]
        [Required]
        public string User_Id { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("BloodBank")]
        [Required]
        public int BloodBank_Id { get; set; }
        public virtual BloodBank BloodBank { get; set; }

        public virtual ICollection<BloodTestResult> BloodTestResults { get; set; }
    }
}
