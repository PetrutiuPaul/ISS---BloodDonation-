using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BloodDonation.Models
{
    public class DonationViewModel
    {        
        [Required]
        public int BloodBank_Id { get; set; }
    }
}
