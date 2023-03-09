using ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal static class EventManager
    {
        public static List<Event> GetEvents(Arguments arguments)
        {
            var events = new List<Event>
            {
                new Event("Birthday", "Tuukka's birthday", new DateOnly(2000,05,23)),
                new Event("Birthday", "Random birthday", new DateOnly(2000,05,01)),
                new Event(null, "Random birthday", new DateOnly(2000,05,01)),
            };

            if(arguments.Categories is null)
            {
                events = events.Where(x => x.Category is null || x.Category == string.Empty).ToList();
            }
            else if(arguments.Categories.Count != 0)
            {
                events = events.Where(x => x.Category is not null && arguments.Categories.Contains(x.Category)).ToList();
            }

            if(arguments.BeforeDate.HasValue)
                events = events.Where(x => x.TimeStamp <  arguments.BeforeDate.Value).ToList();

            if (arguments.AfterDate.HasValue)
                events = events.Where(x => x.TimeStamp > arguments.AfterDate.Value).ToList();

            if (arguments.Date.HasValue)
                events = events.Where(x => x.TimeStamp == arguments.Date.Value).ToList();



            return events;
        }


    }
}
