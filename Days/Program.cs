using CommandLine;
using Days.Managers;
using Days.Models;


Parser.Default.ParseArguments<ListOptions, AddOptions, DeleteOptions>(args) // Parses and runs verbs and options from commandline args
        .WithParsed<ListOptions>(HandleListCommand)
        .WithParsed<AddOptions>(HandleAddCommand)
        .WithParsed<DeleteOptions>(HandleDeleteCommand);

/// <summary>
/// Handles EventManager output to commandline
/// </summary>
static void HandleListCommand(ListOptions options)
{
    try
    {
        var results = EventManager.GetEvents(options);

        foreach (var item in results)
        {
            Console.WriteLine(item);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}

/// <summary>
/// Handles EventManager output to commandline
/// </summary>
static void HandleAddCommand(AddOptions options)
{
    try
    {
        Event result = EventManager.AddEvent(options);

        Console.WriteLine(result);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
    
}

/// <summary>
/// Handles EventManager output to commandline
/// </summary>
static void HandleDeleteCommand(DeleteOptions options)
{
    try
    {
        var results = EventManager.DeleteEvents(options);

        foreach (Event item in results)
        {
            Console.WriteLine(item);
        }
    }
    catch(Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
    
}