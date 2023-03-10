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

        static List<Event> Events = new()
        {
            new Event("Birthday", "Tuukka's birthday", new DateOnly(2000, 05, 23)),
            new Event("Birthday", "Random birthday", new DateOnly(2000, 05, 01)),
            new Event(null, "Random birthday", new DateOnly(2000, 05, 01)),
        };


        /// <summary>
        /// Gets events from .csv file and filters them
        /// </summary>
        /// <param name="options">Option for the filter</param>
        /// <returns></returns>
        public static List<Event> GetEvents(ListOptions options)
        {
            return QueryEvents(Events, options);
        }

        public static Event AddEvent(AddOptions options)
        {
            var newEvent = new Event(options.Category, options.Description, options.Date);
            Events.Add(newEvent);
            return newEvent;
        }

        public static List<Event> DeleteEvents(DeleteOptions options)
        {
            var toBeDeletedEvents = QueryEvents(Events, options);

            if(options.DryRun)
            {
                return toBeDeletedEvents;
            }
            else
            {
                foreach(var toBeDeletedEvent in toBeDeletedEvents)
                {
                    Events.Remove(toBeDeletedEvent);
                }
                return toBeDeletedEvents;
            }
        }


        static List<Event> QueryEvents(List<Event> events, QueryOptions options)
        {
            IEnumerable<Event> filteredEvents = events;

            if (options.NoCategory)
            {
                filteredEvents = filteredEvents.Where(x => x.Category is null || x.Category == string.Empty);
            }

            if (options.IsToday)
            {
                filteredEvents = filteredEvents.Where(x => x.TimeStamp == DateOnly.FromDateTime(DateTime.Now));
            }

            if (options.Categories is not null)
            {
                filteredEvents = filteredEvents.Where(x => x.Category is not null && options.Categories.Split(",").Contains(x.Category));
            }

            if (options.Descriptions is not null)
            {
                filteredEvents = filteredEvents.Where(x => x.Description is not null && options.Descriptions.Split(",").Contains(x.Description));
            }

            if (options.BeforeDate.HasValue)
            {
                filteredEvents = filteredEvents.Where(x => x.TimeStamp < options.BeforeDate.Value);
            }

            if (options.AfterDate.HasValue)
            {
                filteredEvents = filteredEvents.Where(x => x.TimeStamp > options.AfterDate.Value);
            }

            if (options.Date.HasValue)
            {
                filteredEvents = filteredEvents.Where(x => x.TimeStamp == options.Date.Value);
            }


            return (options.IsExcluded) ? events.Except(filteredEvents).ToList() : filteredEvents.ToList();

        }


    }
}
