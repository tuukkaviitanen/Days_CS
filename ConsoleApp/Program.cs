
using ConsoleApp;
using ConsoleApp.Models;
using Mono.Options;


var command = args.FirstOrDefault();

var arguments = new Arguments();

var options = new OptionSet()
{
    {
        "--today", "Get Events happening today", (c) => arguments.Date = DateOnly.FromDateTime(DateTime.Now)
    },
    {
        "--categories", "Get Events by category", c => arguments.Categories = c.Split(',').ToList()
    },
    {
        "--description", "Get Events by category", d => arguments.Descriptions = d.Split(',').ToList()
    },
    {
        "--after-date", "Get Events after this date", a => arguments.AfterDate = DateOnly.Parse(a)
    },
    {
        "--before-date", "Get Events before this date", b => arguments.BeforeDate = DateOnly.Parse(b)
    },
    {
        "--date", "Get Events on this date", d => arguments.Date = DateOnly.Parse(d)
    },
    {
        "--no-category", "Get Events with no category", n => arguments.Categories = null
    },
    {
        "--exclude", "Paramters are excluded", e => arguments.IsExcluded = true
    },
};
options.Parse(args);


switch (command)
{
    case "list":
        {
            var events = EventManager.GetEvents(arguments);
            foreach(var e in events)
            { 
                Console.WriteLine(e); 
            }
            break;
        }
    default:
        throw new ArgumentException("invalid command");
        
}






