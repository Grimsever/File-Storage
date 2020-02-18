using System;
using System.IO;
using System.Linq;

namespace FileStorage.UserLayer.Commands
{
    internal class UploadFileCommand : Command
    {
        public UploadFileCommand(Credentials credentials, string commandStringFormat, string instructions, Commands number)
               : base(credentials, commandStringFormat, instructions, number) { }

        public override void Execute(params string[] data)
        {
            string pathFile = data[0];
            if (new FileInfo(pathFile).Exists)
            {
                var file = new FileInfo(pathFile);
                if (!(new FileInfo(UserCredentials.MetaFileInfo.DefaultPath + file.Name).Exists))
                {
                    ReadMetaFile();
                    file.CopyTo(UserCredentials.MetaFileInfo.DefaultPath + file.Name);
                    WriteToMetaFile(file);
                    ExecutionResult = $"The file {pathFile} has been uploaded \n- file name: {file.Name} \n- file size: {file.Length} byte \n- extension: {file.Extension}";
                }
                else
                {
                    ExecutionResult = "The file already exist";
                    throw new Exception(ExecutionResult);
                }
            }
            else
            {
                ExecutionResult = $"The file {pathFile} doesn`t found";
                throw new Exception(ExecutionResult);
            }
        }
        protected override void WriteToMetaFile(FileInfo file)
        {
            string name = file.Name;
            string extension = file.Extension;
            double size = (double)file.Length;
            DateTime dateCreation = file.CreationTime;
            int downloadNum = 0;
            MetaFile.Add(new MetaFile()
            {
                Name = name,
                Extension = extension,
                Size = size,
                CreationDate = dateCreation,
                DownloadsNumber = downloadNum
            });
            using (FileStream writer = new FileStream(UserCredentials.MetaFileInfo.DefaultPath + UserCredentials.MetaFileInfo.NameMetaFile, FileMode.Open))
            {
                Formater.Serialize(writer, MetaFile.ToArray());
            }
        }
    }
}
