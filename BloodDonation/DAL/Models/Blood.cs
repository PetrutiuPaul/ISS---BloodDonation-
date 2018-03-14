namespace DAL.Models
{
    using Common.Enums;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Blood
    {  
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public BloodType Type { get; set; }

        [Required]
        public RhType Rh { get; set; }

        [ForeignKey("BloodBank")]
        [Required]
        public int BloodBankId { get; set; }

        public virtual BloodBank BloodBank { get; set; }
    }
}
