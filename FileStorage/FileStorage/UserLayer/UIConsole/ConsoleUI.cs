using FileStorage.UserLayer.Commands;
using FileStorage.UserLayer.ListOfCommands;
using System;

namespace FileStorage.UserLayer.UIConsole
{
    internal class ConsoleUI : IConsoleUI
    {
        public string CommandMenu { get; set; }
        public User UserStorage { get; }
        public IListOfCommands ListCommands { get; set; }

        public ConsoleUI(User user, IListOfCommands list_Commands)
        {
            UserStorage = user;
            ListCommands = list_Commands;
            CommandMenu = $"Choose any operation:\n{ListCommands.GetListOfCommands()}q. Exit";
            Start();
        }

        public void Start()
        {
            Enter(out string login, out string password);
            if (UserStorage.LogIn(login, password))
            {
                Console.WriteLine($"Hello {login}!!!");
                Console.WriteLine("Press any key...");
                Console.ReadKey();
                string choice;
                do
                {
                    Console.Clear();
                    Console.WriteLine(CommandMenu);
                    choice = Console.ReadLine();
                    if (int.TryParse(choice, out int numberCommand))
                    {
                        try
                        {
                            Command command = ListCommands.SetCommand(numberCommand);
                            Console.WriteLine("Instructions:");
                            Console.WriteLine(GetInstructions(command));
                            command.Execute(Console.ReadLine(), Console.ReadLine());
                            Console.WriteLine(command.ExecutionResult);
                            Console.ReadKey();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"!!!Exception!!! : {e.Message}");
                        }
                    }
                    else if (choice != "q")
                    {
                        Console.WriteLine($"You made a mistake in input: {choice}. Command was`t found");
                    }
                    else
                    {
                        Console.WriteLine("BYE");
                    }
                }
                while (choice != "q");
            }
            else
            {
                Console.WriteLine("Your data is`t correct");
            }
            Console.ReadKey();
        }

        public void Enter(out string login, out string password)
        {
            Console.WriteLine("Write login");
            login = Console.ReadLine();
            Console.WriteLine("Write password");
            password = Console.ReadLine();
        }

        public string GetInstructions(Command command)
        {
            return command.Instructions;
        }
    }
}