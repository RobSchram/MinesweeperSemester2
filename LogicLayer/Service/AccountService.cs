using LogicLayer.interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Service
{
    public class AccountService :IAccountService
    {
        private readonly IAccountDao _accountDao;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
        public AccountService(IAccountDao accountDao) 
        {
            _passwordHasher = new PasswordHasher<ApplicationUser>();
            _accountDao = accountDao;
        }
        public ApplicationUser SearchAccount(string username)
        {
            return _accountDao.SearchAccount(username);
        }
        public bool CreateUser(string username, string password)
        {
            return _accountDao.CreateUser(username, password);
        }
        public string HashPassword(string password)
        {
            var user = new ApplicationUser();
            return _passwordHasher.HashPassword(user, password);
        }

        public bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            var user = new ApplicationUser();
            var result = _passwordHasher.VerifyHashedPassword(user, hashedPassword, providedPassword);
            return result == PasswordVerificationResult.Success;
        }
    }
}
