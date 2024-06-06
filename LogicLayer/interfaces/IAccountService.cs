using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.interfaces
{
    public interface IAccountService
    {
        public bool CreateUser(string username, string password);
        public ApplicationUser SearchAccount(string userName);
        public string HashPassword(string password);
        public bool VerifyPassword(string hashedPassword, string providedPassword);
    }
}
