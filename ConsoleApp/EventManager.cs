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
        public static List<Event> GetEvents(Options options)
        {
            var events = new List<Event>
            {
                new Event("Birthday", "Tuukka's birthday", new DateOnly(2000,05,23)),
                new Event("Birthday", "Random birthday", new DateOnly(2000,05,01)),
                new Event(null, "Random birthday", new DateOnly(2000,05,01)),
            };

            if(options.NoCategory)
                events = events
                    .Where(x => (!options.IsExcluded) ? x.Category is null || x.Category == string.Empty : !(x.Category is null || x.Category == string.Empty))
                    .ToList();

            if(options.IsToday)
                events = events
                    .Where(x => (!options.IsExcluded) ? x.TimeStamp == DateOnly.FromDateTime(DateTime.Today) : x.TimeStamp != DateOnly.FromDateTime(DateTime.Today))
                    .ToList();

            if (options.Categories is not null)
                events = events
                    .Where(x => x.Category is not null && (!options.IsExcluded) ? options.Categories.Split(",").Contains(x.Category) : !options.Categories.Split(",").Contains(x.Category) )
                    .ToList();

            if (options.Descriptions is not null)
                events = events
                    .Where(x => x.Description is not null && options.Descriptions.Split(",").Contains(x.Description))
                    .ToList();

            if (options.BeforeDate.HasValue)
                events = events
                    .Where(x => (!options.IsExcluded) ? x.TimeStamp < options.BeforeDate.Value : x.TimeStamp >= options.BeforeDate.Value)
                    .ToList();

            if (options.AfterDate.HasValue)
                events = events
                    .Where(x => (!options.IsExcluded) ? x.TimeStamp > options.AfterDate.Value : x.TimeStamp <= options.AfterDate.Value)
                    .ToList();

            if (options.Date.HasValue)
                events = events
                    .Where(x => (!options.IsExcluded) ? x.TimeStamp == options.Date.Value : x.TimeStamp != options.Date.Value)
                    .ToList();



            return events;
        }


    }
}
