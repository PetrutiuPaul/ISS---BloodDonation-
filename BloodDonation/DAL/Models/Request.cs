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
    public class Request
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string CNP { get; set; }

        [Required]
        public string PatientName { get; set; }

        [Required]
        public Priority Priority { get; set; }

        [Required]
        public BloodType BloodType { get; set; }

        [Required]
        public RhType Rh { get; set; }

        public virtual User Doctor { get; set; }

        [Required]
        [ForeignKey("User")]
        public string User_Id { get; set; }

        public virtual User User { get; set; }
    }
}
