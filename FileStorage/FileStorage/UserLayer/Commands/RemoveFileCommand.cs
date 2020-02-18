using System;
using System.IO;
using System.Linq;

namespace FileStorage.UserLayer.Commands
{
    internal class RemoveFileCommand : Command
    {
        public RemoveFileCommand(Credentials credentials, string commandStringFormat, string instructions, Commands number)
            : base(credentials, commandStringFormat, instructions, number) { }

        public override void Execute(params string[] data)
        {
            string nameFile = UserCredentials.MetaFileInfo.DefaultPath + data[0];
            if (new FileInfo(nameFile).Exists)
            {
                ReadMetaFile();
                var file = new FileInfo(nameFile);
                WriteToMetaFile(file);
                ExecutionResult = $"The file \"{file.Name}\" has been removed.";
                file.Delete();
            }
            else
            {
                ExecutionResult = $"The file \"{data[0]}\" doesn`t exist in storage!";
                throw new Exception(ExecutionResult);
            }
        }
        protected override void WriteToMetaFile(FileInfo file)
        {
            MetaFile.RemoveWhere(x => x.Name == file.Name);
            using (FileStream writer = new FileStream(UserCredentials.MetaFileInfo.DefaultPath + UserCredentials.MetaFileInfo.NameMetaFile, FileMode.Open))
            {
                Formater.Serialize(writer, MetaFile.ToArray());
            }
        }
    }
}
