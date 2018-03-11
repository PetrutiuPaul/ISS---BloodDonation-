using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Product
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public DateTime ExpireTime { get; set; }

        [ForeignKey("BloodBank")]
        public int BloodBank_Id { get; set; }

        public virtual BloodBank BloodBank { get; set; }
    }
}
