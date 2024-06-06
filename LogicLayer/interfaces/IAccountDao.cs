namespace LogicLayer.interfaces
{
    public interface IAccountDao
    {
        public bool CreateUser(string username, string password);
        public ApplicationUser SearchAccount(string userName);
    }
}
