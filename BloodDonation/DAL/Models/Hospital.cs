﻿namespace DAL.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Hospital
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [ForeignKey("BloodBank")]
        public int BloodBank_Id { get; set; }

        public virtual BloodBank BloodBank { get; set; }

        public virtual ICollection<User> Doctors { get; set; }
    }
}
