using System;
using System.IO;

namespace FileStorage.UserLayer.Commands
{
    internal class FileInfoCommand : Command
    {
        public FileInfoCommand(Credentials credentials, string commandStringFormat, string instructions, Commands number)
            : base(credentials, commandStringFormat, instructions, number) { }

        public override void Execute(params string[] data)
        {
            string nameFile = UserCredentials.MetaFileInfo.DefaultPath + data[0];

            if (new FileInfo(nameFile).Exists)
            {
                var file = new FileInfo(nameFile);
                ExecutionResult = $"name: {file.FullName}" +
                                  $"\nextension: {file.Extension}" +
                                  $"\nfile size: {file.Length} byte" +
                                  $"\ncreation date: {file.CreationTime:yyyy-MM-dd}" +
                                  $"\nlogin: {UserCredentials.Login}";
            }
            else
            {
                ExecutionResult = "The file doesn`t exist.";
                throw new Exception(ExecutionResult);
            }
        }
    }
}
