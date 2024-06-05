using LogicLayer.interfaces;
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
        public AccountService(IAccountDao accountDao) 
        {
            _accountDao = accountDao;
        }
        public bool SearchAccount(string username, string password)
        {
            return _accountDao.SearchAccount(username, password);
        }
        public bool CreateUser(string username, string password)
        {
            return _accountDao.CreateUser(username, password);
        }
    }
}
