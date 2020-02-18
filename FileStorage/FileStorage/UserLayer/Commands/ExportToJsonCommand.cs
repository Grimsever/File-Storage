using System;
using System.IO;
using System.Text.Json;

namespace FileStorage.UserLayer.Commands
{
    internal class ExportToJsonCommand : Command
    {
        public ExportToJsonCommand(Credentials userCredentials, string commandStringFormat, string instructions, Commands number)
            : base(userCredentials, commandStringFormat, instructions, number) { }
        public override void Execute(params string[] data)
        {
            string destPath = data[0] != "" ? data[0] + "meta.json" : UserCredentials.MetaFileInfo.DefaultPath + "meta.json";
            if (!new FileInfo(destPath).Exists)
            {
                ReadMetaFile();
                string json = JsonSerializer.Serialize(MetaFile);
                File.WriteAllText(destPath, json);
                ExecutionResult = $"The meta-information has been exported, path = \"{destPath}\"";
            }
            else
            {
                throw new Exception("File already exist!");
            }
        }
    }
}
