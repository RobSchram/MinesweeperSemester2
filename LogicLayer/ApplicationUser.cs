namespace LogicLayer
{
    public class ApplicationUser
    {
        public int Id { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public void SetId(int id)
        {
            Id = id;
        }
        public void SetUserName(string userName)
        {
            UserName = userName;
        }
        public void SetPassWord(string passWord)
        {
            Password = passWord;
        }
    }
}
