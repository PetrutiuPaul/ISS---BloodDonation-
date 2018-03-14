namespace DAL.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Locality
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [ForeignKey("County")]
        public int County_Id { get; set; }

        public virtual County County { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
