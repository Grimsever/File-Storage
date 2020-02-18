using FileStorage.UserLayer;

namespace FileStorage
{
    public class Credentials
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public StorageInfo MetaFileInfo { get; set; }
    }
}