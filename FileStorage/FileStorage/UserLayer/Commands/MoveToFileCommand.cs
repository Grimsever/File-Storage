using System;
using System.IO;
using System.Linq;

namespace FileStorage.UserLayer.Commands
{
    internal class MoveToFileCommand : Command
    {
        public MoveToFileCommand(Credentials credentials, string commandStringFormat, string instructions, Commands number)
            : base(credentials, commandStringFormat, instructions, number) { }

        public override void Execute(params string[] data)
        {
            string nameFile = UserCredentials.MetaFileInfo.DefaultPath + data[0];
            string newName = UserCredentials.MetaFileInfo.DefaultPath + data[1];
            if (new FileInfo(nameFile).Exists)
            {
                ReadMetaFile();
                var file = new FileInfo(nameFile);
                file.MoveTo(newName);

                var files = MetaFile.FirstOrDefault(x => x.Name == data[0]);
                files.Name = data[1];
                WriteToMetaFile(null);
                ExecutionResult = $"The file \"{data[0]}\" has been moved to \"{data[1]}\"";
            }
            else
            {
                ExecutionResult = "File name is`t correct, or file doesn`t exist.";
                throw new Exception(ExecutionResult);
            }
        }
    }
}
