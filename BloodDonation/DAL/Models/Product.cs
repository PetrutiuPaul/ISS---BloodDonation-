namespace DAL.Models
{
    using Common.Enums;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Product
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public ProductType Type { get; set; }

        [Required]
        public DateTime ExpireTime { get; set; }

        [Required]
        public float Amount { get; set; }

        [ForeignKey("BloodBank")]
        public int BloodBank_Id { get; set; }

        public virtual BloodBank BloodBank { get; set; }
    }
}
