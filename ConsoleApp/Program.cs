using CommandLine;
using ConsoleApp;
using ConsoleApp.Managers;
using ConsoleApp.Models;


Parser.Default.ParseArguments<ListOptions, AddOptions, DeleteOptions>(args)
        .WithParsed<ListOptions>(HandleListCommand)
        .WithParsed<AddOptions>(HandleAddCommand)
        .WithParsed<DeleteOptions>(HandleDeleteCommand);

/// <summary>
/// Handles EventManager output to commandline
/// </summary>
static void HandleListCommand(ListOptions options)
{
    var results = EventManager.GetEvents(options);

    foreach (var item in results)
    {
        Console.WriteLine(item);
    }
}

/// <summary>
/// Handles EventManager output to commandline
/// </summary>
static void HandleAddCommand(AddOptions options)
{
    Event result = EventManager.AddEvent(options);

    Console.WriteLine(result);
}

/// <summary>
/// Handles EventManager output to commandline
/// </summary>
static void HandleDeleteCommand(DeleteOptions options)
{
    var results = EventManager.DeleteEvents(options);

    foreach (Event item in results)
    {
        Console.WriteLine(item);
    }
}