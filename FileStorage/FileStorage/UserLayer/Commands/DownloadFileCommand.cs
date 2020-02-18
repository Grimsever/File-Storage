using System;
using System.IO;
using System.Linq;

namespace FileStorage.UserLayer.Commands
{
    internal class DownloadFileCommand : Command
    {
        public DownloadFileCommand(Credentials credentials, string commandStringFormat, string instructions, Commands number)
            : base(credentials, commandStringFormat, instructions, number) { }

        public override void Execute(params string[] data)
        {
            string nameFile = UserCredentials.MetaFileInfo.DefaultPath + data[0];
            string pathToDownload = data[1];
            pathToDownload += $"\\{data[0]}";

            if (new FileInfo(nameFile).Exists)
            {

                if (!(new FileInfo(pathToDownload).Exists))
                {
                    var file = new FileInfo(nameFile);
                    ReadMetaFile();
                    WriteToMetaFile(file);
                    file.CopyTo(pathToDownload);
                    ExecutionResult = $"The file \"{data[0]}\" has been downloaded";
                }
                else
                {
                    ExecutionResult = "Path is`t correct, or file already exist";
                    throw new Exception(ExecutionResult);
                }
            }
            else
            {
                ExecutionResult = "File name is`t correct, or file doesn`t exist.";
                throw new Exception(ExecutionResult);
            }

        }
        protected override void WriteToMetaFile(FileInfo file)
        {
            MetaFile.FirstOrDefault(x => x.Name == file.Name).DownloadsNumber++;

            using(FileStream writer = new FileStream(UserCredentials.MetaFileInfo.DefaultPath + UserCredentials.MetaFileInfo.NameMetaFile, FileMode.Open))
            {
                Formater.Serialize(writer, MetaFile.ToArray());
            }
        }

    }
}
