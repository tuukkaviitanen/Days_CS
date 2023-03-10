
using CommandLine;
using ConsoleApp;
using ConsoleApp.Models;

var result = Parser.Default.ParseArguments<ListOptions, AddOptions, DeleteOptions>(args)
    .WithParsed<ListOptions>(HandleListCommand)
    .WithParsed<AddOptions>(HandleAddCommand)
    .WithParsed<DeleteOptions>(HandleDeleteCommand);

static void HandleListCommand(ListOptions options)
{
    var results = EventManager.GetEvents(options);

    foreach(var item in results)
    {
        Console.WriteLine(item);
    }
}

static void HandleAddCommand(AddOptions options)
{
    var result = EventManager.AddEvent(options);

    Console.WriteLine(result);
}

static void HandleDeleteCommand(DeleteOptions options)
{
    var results = EventManager.DeleteEvents(options);

    foreach (var item in results)
    {
        Console.WriteLine(item);
    }
}