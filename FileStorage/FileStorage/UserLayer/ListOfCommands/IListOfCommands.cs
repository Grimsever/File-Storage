using FileStorage.UserLayer.Commands;

namespace FileStorage.UserLayer.ListOfCommands
{
    public interface IListOfCommands
    {
        Command[] ListCommand { get; }
        Command SetCommand(int inputCommand);
        string GetListOfCommands();
    }
}
