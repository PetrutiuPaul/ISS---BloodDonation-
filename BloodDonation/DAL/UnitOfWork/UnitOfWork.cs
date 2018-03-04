namespace DAL.UnitOfWork
{
    using DAL.Models;
    using DAL.Repositories;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;

    public class UnitOfWork : IDisposable
    {
        private AppDbContext context = new AppDbContext();
        private GenericRepository<User> userRepository;

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
