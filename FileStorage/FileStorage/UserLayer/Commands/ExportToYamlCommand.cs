using System;
using System.IO;

namespace FileStorage.UserLayer.Commands
{
    internal class ExportToYamlCommand : Command
    {
        public ExportToYamlCommand(Credentials credentials, string commandStringFormat, string instructions, Commands number)
            : base(credentials, commandStringFormat, instructions, number) { }
        public override void Execute(params string[] data)
        {
            string destPath = data[0] != "" ? data[0] + "meta.yaml" : UserCredentials.MetaFileInfo.DefaultPath + "meta.yaml";
            if (!new FileInfo(destPath).Exists)
            {
                ReadMetaFile();

                File.WriteAllText(destPath, new YamlDotNet.Serialization.Serializer().Serialize(MetaFile));
                ExecutionResult = $"The meta-information has been exported, path = \"{destPath}\"";
            }
            else
            {
                throw new Exception("File already exist!");
            }
        }
    }
}
