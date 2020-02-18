using FileStorage.UserLayer.Commands;
using FileStorage.UserLayer.ListOfCommands;

namespace FileStorage.UserLayer.UIConsole
{
    public interface IConsoleUI
    {
        string CommandMenu { get; set; }
        User UserStorage { get; }
        IListOfCommands ListCommands { set; }
        void Enter(out string login, out string pass);
        void Start();
        string GetInstructions(Command command);
    }
}
