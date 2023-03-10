using ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Managers
{
    /// <summary>
    /// All Event opertaions are handled through this Manager
    /// </summary>
    public static class EventManager
    {
        /// <summary>
        /// Temporary List for all events. 
        /// TODO: Should be collected from a CSV-file
        /// </summary>
        static List<Event> Events = new()
        {
            new Event("Birthday", "Tuukka's birthday", new DateOnly(2000, 05, 23)),
            new Event("Birthday", "Random birthday", new DateOnly(2000, 05, 01)),
            new Event(null, "Random birthday", new DateOnly(2000, 05, 01)),
        };


        /// <summary>
        /// Gets events from collection and filters them
        /// </summary>
        /// <param name="options">Option for the filter</param>
        /// <returns></returns>
        public static List<Event> GetEvents(ListOptions options)
        {
            return QueryEvents(Events, options);
        }

        /// <summary>
        /// Adds an event to the Events collection
        /// </summary>
        /// <param name="options">AddOptions that implements IEvent. Can be converted to Event type</param>
        /// <returns></returns>
        public static Event AddEvent(AddOptions options)
        {
            Event newEvent = new Event(options);
            Events.Add(newEvent);
            return newEvent;
        }

        /// <summary>
        /// Deletes all events that match the given options.
        /// EXCEPT when DryRun is enabled.
        /// Returns the deleted/would-be-deleted Events
        /// </summary>
        /// <param name="options">Filters for the delete operation</param>
        /// <returns></returns>
        public static List<Event> DeleteEvents(DeleteOptions options)
        {
            var toBeDeletedEvents = QueryEvents(Events, options);

            if (options.DryRun)
            {
                return toBeDeletedEvents;
            }
            else
            {
                foreach (var toBeDeletedEvent in toBeDeletedEvents)
                {
                    Events.Remove(toBeDeletedEvent);
                }
                return toBeDeletedEvents;
            }
        }


        /// <summary>
        /// Queries the events with the given IEventFilterOptions
        /// </summary>
        /// <param name="events">All Events</param>
        /// <param name="options">Options/Filters</param>
        /// <returns></returns>
        static List<Event> QueryEvents(List<Event> events, IEventFilterOptions options)
        {
            IEnumerable<Event> filteredEvents = events;

            if (options.Category is not null)
            {
                filteredEvents = filteredEvents.Where(x => x.Category == options.Category);
            }

            if (options.NoCategory)
            {
                filteredEvents = filteredEvents.Where(x => x.Category is null || x.Category == string.Empty);
            }

            if (options.IsToday)
            {
                filteredEvents = filteredEvents.Where(x => x.Date == DateOnly.FromDateTime(DateTime.Now));
            }

            if (options.Categories is not null)
            {
                filteredEvents = filteredEvents.Where(x => x.Category is not null && options.Categories.Split(",").Contains(x.Category));
            }

            if (options.Description is not null)
            {
                filteredEvents = filteredEvents.Where(x => x.Description is not null && x.Description.Contains(options.Description));
            }

            if (options.BeforeDate.HasValue)
            {
                filteredEvents = filteredEvents.Where(x => x.Date < options.BeforeDate.Value);
            }

            if (options.AfterDate.HasValue)
            {
                filteredEvents = filteredEvents.Where(x => x.Date > options.AfterDate.Value);
            }

            if (options.Date.HasValue)
            {
                filteredEvents = filteredEvents.Where(x => x.Date == options.Date.Value);
            }


            return options.IsExcluded ? events.Except(filteredEvents).ToList() : filteredEvents.ToList();

        }


    }
}
