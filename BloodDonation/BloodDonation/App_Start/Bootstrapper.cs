using Autofac;
using Autofac.Integration.Mvc;
using DAL.UnitOfWork;
using DAL.UnitOfWork.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace BloodDonation.App_Start
{

    /// <summary>
    /// Dependency injection container
    /// </summary>
    public class Bootstrapper
    {
        /// <summary>
        /// Runs the dependency injection container
        /// </summary>
        public static void Run()
        {
            SetAutofacContainer();
        }

        /// <summary>
        /// Sets the dependency injection container
        /// </summary>
        private static void SetAutofacContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            
            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }

}