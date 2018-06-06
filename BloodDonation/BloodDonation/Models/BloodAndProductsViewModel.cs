using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodDonation.Models
{
    public class BloodAndProductsViewModel
    {
        public Blood BloodO { get; set; }

        public Blood BloodA { get; set; }

        public Blood BloodB { get; set; }

        public Blood BloodAB { get; set; }

        public Blood BloodON { get; set; }

        public Blood BloodAN { get; set; }

        public Blood BloodBN { get; set; }

        public Blood BloodABN { get; set; }

        public Product RedCell { get; set; }

        public Product WhiteCell { get; set; }

        public Product Plasma { get; set; }

    }
}