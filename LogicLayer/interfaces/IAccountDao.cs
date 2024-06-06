using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.interfaces
{
    public interface IAccountDao
    {
        public bool CreateUser(string username, string password);
        public ApplicationUser SearchAccount(string userName);
    }
}
