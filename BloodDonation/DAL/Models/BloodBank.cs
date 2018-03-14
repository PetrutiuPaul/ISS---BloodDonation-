namespace DAL.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class BloodBank
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [ForeignKey("County")]
        public int County_Id { get; set; }

        public virtual County County { get; set; }

        public virtual ICollection<Blood> Blood { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public virtual ICollection<Hospital> Hospitals { get; set; }
    }
}
