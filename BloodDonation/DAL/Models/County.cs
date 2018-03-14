namespace DAL.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class County
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Locality> Localities { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
