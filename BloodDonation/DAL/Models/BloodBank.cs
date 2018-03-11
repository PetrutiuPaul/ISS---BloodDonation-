using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class BloodBank
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [ForeignKey("County")]
        public int CountyId { get; set; }

        public virtual ICollection<Blood> Blood { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public virtual ICollection<Hospital> Hospitals { get; set; }
    }
}
