using Days.Models;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace Days.Managers
{
    /// <summary>
    /// All Event operations are handled through this Manager
    /// </summary>
    public static class EventManager
    {
        /// <summary>
        /// Gets events from events.csv and filters them
        /// </summary>
        /// <param name="options">Option for the filter</param>
        /// <returns>Filtered Events</returns>
        public static List<Event> GetEvents(ListOptions options)
        {
            var allEvents = ReadEventsFromCSV();
            return QueryEvents(allEvents, options);
        }

        /// <summary>
        /// Adds an event to the Events collection
        /// </summary>
        /// <param name="options">AddOptions that implements IEvent. Can be converted to Event type</param>
        /// <returns></returns>
        public static Event AddEvent(AddOptions options)
        {
            string eventsFile = GetEventsFilePath();

            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                PrepareHeaderForMatch = args => args.Header.ToLower() // additional configuring needed to make headers case insensitive
            };

            Event newEvent = new Event(options);

            var allEvents = ReadEventsFromCSV();

            allEvents.Add(newEvent);

            WriteEventsToCSV(allEvents);

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

            var allEvents = ReadEventsFromCSV();

            var toBeDeletedEvents = (options.DeleteAllEvents) ? allEvents : QueryEvents(allEvents, options);

            if (options.DryRun)
            {
                return toBeDeletedEvents;
            }
            else
            {
                foreach (var toBeDeletedEvent in toBeDeletedEvents)
                {
                    allEvents.Remove(toBeDeletedEvent);
                }

                WriteEventsToCSV(allEvents);

                return toBeDeletedEvents;
            }
        }


        /// <summary>
        /// Queries the events with the given IEventFilterOptions
        /// </summary>
        /// <param name="events">All Events</param>
        /// <param name="options">Options/Filters</param>
        /// <returns></returns>
        static List<Event> QueryEvents(IEnumerable<Event> events, IEventFilterOptions options)
        {
            IEnumerable<Event> filteredEvents = events;

            if (options.Category is not null)
            {
                filteredEvents = filteredEvents.Where(x => x.Category?.ToLower() == options.Category.ToLower()); // Case insensitive
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
                filteredEvents = filteredEvents
                    .Where(x => x.Category is not null && options.Categories.ToLower().Split(",").Contains(x.Category.ToLower())); // Case insensitive
            }

            if (options.Description is not null)
            {
                filteredEvents = filteredEvents.Where(x => x.Description is not null && x.Description.ToLower().Contains(options.Description.ToLower())); // Case insensitive
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


        /// <summary>
        /// Creates and tests the filepath for Events.csv file
        /// </summary>
        /// <returns>Full path for Events.csv</returns>
        /// <exception cref="DirectoryNotFoundException">Throws this if one of the two directories doesn't exist</exception>
        /// <exception cref="FileNotFoundException">Throws this if Events.csv doesn't exist</exception>
        public static string GetEventsFilePath()
        {
            string userHomeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            if (!Directory.Exists(userHomeDirectory))
            {
                throw new DirectoryNotFoundException("Home directory couldn't be determined: " + userHomeDirectory);
            }

            string daysDirectory = Path.Combine(userHomeDirectory + "/.days");
            if (!Directory.Exists(daysDirectory))
            {
                throw new DirectoryNotFoundException(".days directory doesnt exist in the home directory: " + daysDirectory + " please create it!");
            }

            string eventsFile = Path.Combine(daysDirectory + "/events.csv");

            if (!File.Exists(eventsFile))
            {
                throw new FileNotFoundException("events.csv doesn't exist in the .days directory: " + eventsFile + " please create it!");
            }

            return eventsFile;
        }

        /// <summary>
        /// Reads all Events from Events.csv file
        /// </summary>
        /// <returns>All Events from Events.csv file</returns>
        private static List<Event> ReadEventsFromCSV()
        {
            string eventsFile = GetEventsFilePath();

            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                PrepareHeaderForMatch = args => args.Header.ToLower() // this additional configuring needed to make headers case insensitive
            };

            using var reader = new StreamReader(eventsFile);
            using var csvReader = new CsvReader(reader, csvConfig);

            return csvReader.GetRecords<Event>().OrderBy(x => x.Date).ToList();
        }

        /// <summary>
        /// Overwrites Events to Events.csv file
        /// </summary>
        /// <param name="eventsToWrite">Writes these events to CSV file</param>
        private static void WriteEventsToCSV(IEnumerable<Event> eventsToWrite)
        {
            string eventsFile = GetEventsFilePath();

            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                PrepareHeaderForMatch = args => args.Header.ToLower() // this additional configuring needed to make headers case insensitive
            };

            using var writer = new StreamWriter(eventsFile);
            using var csvWriter = new CsvWriter(writer, csvConfig);

            csvWriter.WriteRecords(eventsToWrite.OrderBy(x => x.Date));
        }

    }
}
