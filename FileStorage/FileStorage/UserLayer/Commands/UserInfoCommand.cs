using System;
using System.IO;
using System.Linq;

namespace FileStorage.UserLayer.Commands
{
    internal class UserInfoCommand : Command
    {
        public UserInfoCommand(Credentials credentials, string commandStringFormat, string instructions, Commands number)
            : base(credentials, commandStringFormat, instructions, number) { }

        public override void Execute(params string[] data)
        {
            ReadMetaFile();
            ExecutionResult = $"login: {UserCredentials.Login} " +
                              $"\ncreation Date: {UserCredentials.MetaFileInfo.DateCreating.ToString("yyyy-MM-dd")} " +
                              $"\nstorage used: {MetaFile.Sum(x => x.Size)} " +
                              $"\nfile number: {MetaFile.Count}";
        }

        protected override void ReadMetaFile()
        {
            using (FileStream fs = new FileStream(UserCredentials.MetaFileInfo.DefaultPath + UserCredentials.MetaFileInfo.NameMetaFile, FileMode.Open))
            {
                if (fs.Length > 0)
                {
                    MetaFile = ((MetaFile[])Formater.Deserialize(fs)).ToHashSet();
                }
                else
                {
                    ExecutionResult = "The metafile is empty";
                    throw new Exception(ExecutionResult);
                }
            }
        }
    }
}
