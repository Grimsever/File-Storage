using FileStorage.UserLayer;
using FileStorage.UserLayer.UIConsole;
using Microsoft.Extensions.Configuration;
using System.IO;
using FileStorage.UserLayer.ListOfCommands;

namespace FileStorage
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
            var appConfigValue = configuration.GetSection(nameof(Credentials)).Get<Credentials>();

            if (!new FileInfo(appConfigValue.MetaFileInfo.DefaultPath + appConfigValue.MetaFileInfo.NameMetaFile).Exists)
            {
                FileInfo metaFile = new FileInfo(appConfigValue.MetaFileInfo.DefaultPath +
                                                 appConfigValue.MetaFileInfo.NameMetaFile);
                using (metaFile.Create()) {}
            }

            var ui = new ConsoleUI(new User(appConfigValue), new ListCommands(appConfigValue));
        }
    }
}