namespace DAL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Common.Enums;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class User : IdentityUser
    {
        [Required]
        public string CNP { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [ForeignKey("County")]
        [Required]
        public int County_Id { get; set; }

        public virtual County County { get; set; }

        [Required]
        public int Locality_Id { get; set; }

        public virtual Locality Locality { get; set; }

        public BloodType? BloodType { get; set; }
        public RhType? RhType { get; set; }

        [ForeignKey("Hospital")]
        public int? Hospital_Id { get; set; }

        public virtual Hospital Hospital { get; set; }

        public virtual ICollection<Request> Requests { get; set; }

        public virtual ICollection<Donation> Donations { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
