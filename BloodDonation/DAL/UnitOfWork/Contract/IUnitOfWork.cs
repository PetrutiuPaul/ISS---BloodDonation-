using DAL.Models;
using DAL.Repositories;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.UnitOfWork.Contract
{
    public interface IUnitOfWork
    {
        UserManager<User> User_Manager { get; }

        GenericRepository<User> UserRepository { get; }

        GenericRepository<Hospital> HospitalRepository { get; }


        GenericRepository<Blood> BloodRepository { get; }

        GenericRepository<BloodBank> BloodBankRepository { get; }

        GenericRepository<BloodTestResult> BloodTestResultRepository { get; }

        GenericRepository<County> CountyRepository { get; }

        GenericRepository<Donation> DonationRepository { get; }

        GenericRepository<Locality> LocalityRepository { get; }

        GenericRepository<Notification> NotificationRepository { get; }

        GenericRepository<Product> ProductRepository { get; }

        GenericRepository<Request> RequestRepository { get; }

        GenericRepository<UserNotification> UserNotificationRepository { get; }

        void Save();
    }
}