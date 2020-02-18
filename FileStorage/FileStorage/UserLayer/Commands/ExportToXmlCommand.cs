using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace FileStorage.UserLayer.Commands
{
    internal class ExportToXmlCommand : Command
    {
        public ExportToXmlCommand(Credentials credentials, string commandStringFormat, string instructions, Commands number)
            : base(credentials, commandStringFormat, instructions, number) { }

        public override void Execute(params string[] data)
        {
            string destPath = data[0] != "" ? data[0] + "meta.xml" : UserCredentials.MetaFileInfo.DefaultPath + "meta.xml";
            if (!new FileInfo(destPath).Exists)
            {
                ReadMetaFile();

                using (FileStream writer = new FileStream(destPath, FileMode.OpenOrCreate))
                {
                    new XmlSerializer(typeof(HashSet<MetaFile>)).Serialize(writer, MetaFile);
                }
                ExecutionResult = $"The meta-information has been exported, path = \"{destPath}\"";
            }
            else
            {
                throw new Exception("File already exist!");
            }
        }
    }
}
