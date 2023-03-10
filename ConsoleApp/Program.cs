
using ConsoleApp;
using ConsoleApp.Models;

var options = CommandLineManager.ParseCommand(args);

var result = options.Command switch
{
    Command.List => EventManager.GetEvents(options),
    Command.Add => throw new NotImplementedException(),
    Command.Delete => throw new NotImplementedException(),
};
foreach (var item in result)
{
    Console.WriteLine(item);

}

