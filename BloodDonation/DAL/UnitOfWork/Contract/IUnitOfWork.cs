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

        void Save();
    }
}