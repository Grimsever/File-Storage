using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace FileStorage.UserLayer.Commands
{
    public abstract class Command
    {
        protected string StringFormatCommand { get; }
        public string Instructions { get; }
        public string ExecutionResult { get; set; }
        protected Credentials UserCredentials { get; }
        protected HashSet<MetaFile> MetaFile { get; set; }
        protected BinaryFormatter Formater { get; } 
        protected int numberCommand;

        protected Command(Credentials userCredentials, string commandStringFormat, string instructions, Commands number)
        {
            UserCredentials = userCredentials;
            Formater = new BinaryFormatter();
            MetaFile = new HashSet<MetaFile>();
            StringFormatCommand = commandStringFormat;
            Instructions = instructions;
            numberCommand = (int)number;
        }
        
        public bool IsCommand(int input)
        {
            return (input == numberCommand);
        }

        public abstract void Execute(params string[] data);

        protected virtual void ReadMetaFile()
        {
            using(FileStream fs = new FileStream(UserCredentials.MetaFileInfo.DefaultPath + UserCredentials.MetaFileInfo.NameMetaFile, FileMode.Open))
            {
                if (fs.Length > 0)
                {
                    MetaFile = ((MetaFile[])Formater.Deserialize(fs)).ToHashSet();
                }
            }
        }

        protected virtual void WriteToMetaFile(FileInfo file)
        {
            using(FileStream writer = new FileStream(UserCredentials.MetaFileInfo.DefaultPath + UserCredentials.MetaFileInfo.NameMetaFile, FileMode.Open))
            {
                Formater.Serialize(writer, MetaFile.ToArray());
            }
        }

        public override string ToString()
        {
            return $"{numberCommand}. {StringFormatCommand}"; ;
        }
    }
}
