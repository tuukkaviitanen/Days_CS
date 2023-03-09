
using CommandLine;
using ConsoleApp;
using ConsoleApp.Models;
using Mono.Options;


var command = args.FirstOrDefault();

var arguments = new Arguments();

await Parser.Default.ParseArguments<Options>(args).WithParsedAsync( async options =>
{
    switch (command)
    {
        case "list":
            {
                var events = EventManager.GetEvents(options);
                foreach (var e in events)
                {
                    Console.WriteLine(e);
                }
                break;
            }
        default:
            throw new ArgumentException("invalid command");

    }
});










