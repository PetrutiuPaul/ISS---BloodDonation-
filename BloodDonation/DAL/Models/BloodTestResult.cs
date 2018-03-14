namespace DAL.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class BloodTestResult
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Donation")]
        public int Donation_Id { get; set; }
        public virtual Donation Donation { get; set; }
        [Required]
        public string AnalyzeResult { get; set; }
    }
}
