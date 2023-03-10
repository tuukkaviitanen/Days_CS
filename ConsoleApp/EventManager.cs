using ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("ConsoleApp.Tests")]
namespace ConsoleApp
{
    public static class EventManager
    {
        /// <summary>
        /// Gets events from .csv file and filters them
        /// </summary>
        /// <param name="options">Option for the filter</param>
        /// <returns></returns>
        public static List<Event> GetEvents(Options options)
        {
            var events = new List<Event>
            {
                new Event("Birthday", "Tuukka's birthday", new DateOnly(2000,05,23)),
                new Event("Birthday", "Random birthday", new DateOnly(2000,05,01)),
                new Event(null, "Random birthday", new DateOnly(2000,05,01)),
            };

            IEnumerable<Event> filteredEvents = events;

            if (options.NoCategory)
                filteredEvents = filteredEvents.Where(x => x.Category is null || x.Category == string.Empty);

            if (options.IsToday)
                filteredEvents = filteredEvents.Where(x => x.TimeStamp == DateOnly.FromDateTime(DateTime.Today));

            if (options.Categories is not null)
                filteredEvents = filteredEvents.Where(x => x.Category is not null && options.Categories.Split(",").Contains(x.Category));

            if (options.Descriptions is not null)
                filteredEvents = filteredEvents.Where(x => x.Description is not null && options.Descriptions.Split(",").Contains(x.Description));

            if (options.BeforeDate.HasValue)
                filteredEvents = filteredEvents.Where(x => x.TimeStamp < options.BeforeDate.Value);

            if (options.AfterDate.HasValue)
                filteredEvents = filteredEvents.Where(x => x.TimeStamp > options.AfterDate.Value);

            if (options.Date.HasValue)
                filteredEvents = filteredEvents.Where(x => x.TimeStamp == options.Date.Value);


            return (options.IsExcluded) ? events.Except(filteredEvents).ToList() : filteredEvents.ToList();
        }


    }
}
