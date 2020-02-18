using System;
using FileStorage.UserLayer.Commands;

namespace FileStorage.UserLayer.ListOfCommands
{
    internal class ListCommands : IListOfCommands
    {
        public Command[] ListCommand { get; }
        public ListCommands(Credentials appCredentials)
        {
            ListCommand = new Command[]
            {
                new UserInfoCommand(appCredentials,"user info",
                    "Press Enter twice",
                    Commands.Commands.UserInfo),
                new DownloadFileCommand(appCredentials,"file download",
                    "Write <nameFile> <path to download>. Pls write without <>",
                    Commands.Commands.FileDownload),
                new UploadFileCommand(appCredentials,"file upload",
                    "Write <path-to-file/file> and press Enter twice, for upload in your storage. Pls write without <>",
                    Commands.Commands.FileUpload),
                new MoveToFileCommand(appCredentials,"file move",
                    "Write <source-file-name> <destination-file-name>. Pls write without <>",
                    Commands.Commands.FileMoveTo),
                new RemoveFileCommand(appCredentials, "file remove",
                    "Write <file-name> and press twice Enter. Pls write without <>",
                    Commands.Commands.FileRemove),
                new FileInfoCommand(appCredentials,"file info",
                    "Write <nameFile> and press twice Enter. Pls write without <>",
                    Commands.Commands.FileInfo),
                new ExportToJsonCommand(appCredentials, "file export to format json",
                    "Write <destination-path> and press enter twice. Pls write without <>",
                    Commands.Commands.ExportToJson),
                new ExportToXmlCommand(appCredentials, "file export to format xml",
                    "Write <destination-path> and press enter twice. Pls write without <>",
                    Commands.Commands.ExportToXml),
                new ExportToYamlCommand(appCredentials, "file export to format yaml",
                    "Write <destination-path> and press enter twice. Pls write without <>",
                    Commands.Commands.ExportToYaml),
            };
        }

        public Command SetCommand(int inputCommand)
        {
            foreach (var command in ListCommand)
            {
                if (command.IsCommand(inputCommand))
                {
                    return command;
                }
            }
            throw new Exception("Command not find.");
        }

        public string GetListOfCommands()
        {
            string menuCommands = string.Empty;
            foreach (var command in ListCommand)
            {
                menuCommands += $"{command}\n";
            }
            return menuCommands;
        }
    }
}
