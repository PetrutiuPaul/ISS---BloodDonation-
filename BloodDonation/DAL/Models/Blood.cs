using Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
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
