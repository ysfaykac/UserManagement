namespace UserService.Api.Model
{
    public class LoginRequestModel
    {
        public LoginRequestModel(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
