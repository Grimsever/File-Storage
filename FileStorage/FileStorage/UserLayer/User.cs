namespace FileStorage.UserLayer
{
    public class User
    {
        private readonly Credentials _userCredentials;
        
        public User(Credentials userCredentials)
        {
            _userCredentials = userCredentials;
        }

        public bool LogIn(string login, string password)
        {
            return (_userCredentials.Login == login && _userCredentials.Password == password);
        }
    }
}
