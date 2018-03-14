namespace DAL.UnitOfWork
{
    using DAL.Models;
    using DAL.Repositories;
    using DAL.UnitOfWork.Contract;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;

    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private AppDbContext context = new AppDbContext();
        private GenericRepository<Blood> bloodRepository;
        private GenericRepository<BloodBank> bloodBankRepository;
        private GenericRepository<BloodTestResult> bloodTestResultRepository;
        private GenericRepository<County> countyRepository;
        private GenericRepository<Donation> donationRepository;
        private GenericRepository<Hospital> hospitalRepository;
        private GenericRepository<Locality> localityRepository;
        private GenericRepository<Notification> notificationRepository;
        private GenericRepository<Product> productRepository;
        private GenericRepository<Request> requestRepository;
        private GenericRepository<User> userRepository;
        private GenericRepository<UserNotification> userNotificationRepository;

        private UserStore<User> userStore;
        private UserManager<User> userManager;
        private RoleStore<IdentityRole> roleStore;
        private RoleManager<IdentityRole> roleManager;


        public UserManager<User> User_Manager
        {
            get
            {
                if (this.roleManager == null)
                {
                    this.userStore = new UserStore<User>(context);
                    this.userManager = new UserManager<User>(userStore);

                    this.roleStore = new RoleStore<IdentityRole>(context);
                    this.roleManager = new RoleManager<IdentityRole>(roleStore);
                }
                return userManager;
            }
        }

        public GenericRepository<User> UserRepository
        {
            get
            {
                if (this.userRepository == null)
                {
                    this.userRepository = new GenericRepository<User>(context);
                }
                return userRepository;
            }
        }

        public GenericRepository<Hospital> HospitalRepository
        {
            get
            {
                if (this.hospitalRepository == null)
                {
                    this.hospitalRepository = new GenericRepository<Hospital>(context);
                }
                return hospitalRepository;
            }
        }

        public GenericRepository<Blood> BloodRepository
        {
            get
            {
                if (this.bloodRepository == null)
                {
                    this.bloodRepository = new GenericRepository<Blood>(context);
                }
                return bloodRepository;
            }
        }
        public GenericRepository<BloodBank> BloodBankRepository
        {
            get
            {
                if (this.bloodBankRepository == null)
                {
                    this.bloodBankRepository = new GenericRepository<BloodBank>(context);
                }
                return bloodBankRepository;
            }
        }
        public GenericRepository<BloodTestResult> BloodTestResultRepository
        {
            get
            {
                if (this.bloodTestResultRepository == null)
                {
                    this.bloodTestResultRepository = new GenericRepository<BloodTestResult>(context);
                }
                return bloodTestResultRepository;
            }
        }
        public GenericRepository<County> CountyRepository
        {
            get
            {
                if (this.countyRepository == null)
                {
                    this.countyRepository = new GenericRepository<County>(context);
                }
                return countyRepository;
            }
        }
        public GenericRepository<Donation> DonationRepository
        {
            get
            {
                if (this.donationRepository == null)
                {
                    this.donationRepository = new GenericRepository<Donation>(context);
                }
                return donationRepository;
            }
        }
        public GenericRepository<Locality> LocalityRepository
        {
            get
            {
                if (this.localityRepository == null)
                {
                    this.localityRepository = new GenericRepository<Locality>(context);
                }
                return localityRepository;
            }
        }
  
        public GenericRepository<Notification> NotificationRepository
        {
            get
            {
                if (this.notificationRepository == null)
                {
                    this.notificationRepository = new GenericRepository<Notification>(context);
                }
                return notificationRepository;
            }
        }

        public GenericRepository<Product> ProductRepository
        {
            get
            {
                if (this.productRepository == null)
                {
                    this.productRepository = new GenericRepository<Product>(context);
                }
                return productRepository;
            }
        }
        public GenericRepository<Request> RequestRepository
        {
            get
            {
                if (this.requestRepository == null)
                {
                    this.requestRepository = new GenericRepository<Request>(context);
                }
                return requestRepository;
            }
        }
        public GenericRepository<UserNotification> UserNotificationRepository
        {
            get
            {
                if (this.userNotificationRepository == null)
                {
                    this.userNotificationRepository = new GenericRepository<UserNotification>(context);
                }
                return userNotificationRepository;
            }
        }


        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
