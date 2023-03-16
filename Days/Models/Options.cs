using CommandLine;

namespace Days.Models
{
    /// <summary>
    /// Includes all more-than-once-used constants in Options
    /// </summary>
    static class Constants
    {
        internal const string Category = "category";
        internal const string CategoryHelp = "Filter all Events in selected category. (Case INSENSITIVE)";

        internal const string Categories = "categories";
        internal const string CategoriesHelp = "Filter all Events in selected categories. Seperated by comma ',' (Case INSENSITIVE) ";

        internal const string Description = "description";
        internal const string DescriptionHelp = "Filter all Events matching description (even partial match) (Case INSENSITIVE)";

        internal const string Queries = "Queries";
        internal const string QueriesHelp = "Queries";

        internal const string Date = "date";
        internal const string DateHelp = "Filter all Events in selected date";

        internal const string Today = "today";
        internal const string TodayHelp = "Filter all Events happening today";

        internal const string AfterDate = "after-date";
        internal const string AfterDateHelp = "Filter all Events after the selected date";

        internal const string BeforeDate = "before-date";
        internal const string BeforeDateHelp = "Filter all Events prior the selected date";

        internal const string NoCategory = "no-category";
        internal const string NoCategoryHelp = "Filter all Events with no category";

        internal const string Exclude = "exclude";
        internal const string ExcludeHelp = "Reverse all used filters";

        internal const string All = "all";
        internal const string AllHelp = "Delete ALL Events";

        internal const string DryRun = "dry-run";
        internal const string DryRunHelp = "Only displays Events that would be deleted with used filters";
    }

    /// <summary>
    /// Includes all default filters for Events
    /// </summary>
    public interface IEventFilterOptions
    {
        /// <summary>
        /// Filters Events by single category
        /// </summary>
        public string? Category { get; set; }

        /// <summary>
        /// Filters Events by multiple categories. Seperated by comma ","
        /// </summary>
        public string? Categories { get; set; }

        /// <summary>
        /// Filters Events by all Events that contain this description/part of description
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Filters Events by date
        /// </summary>
        public DateOnly? Date { get; set; }

        /// <summary>
        /// Filters Events happening today
        /// </summary>
        public bool IsToday { get; set; }

        /// <summary>
        /// Filters Events happening after this date
        /// </summary>
        public DateOnly? AfterDate { get; set; }

        /// <summary>
        /// Filters Events happening before this date
        /// </summary>
        public DateOnly? BeforeDate { get; set; }

        /// <summary>
        /// Filters Events with no category
        /// </summary>
        public bool NoCategory { get; set; }

        /// <summary>
        /// Filters are reversed
        /// </summary>
        public bool IsExcluded { get; set; }
    }
    /// <summary>
    /// Extends IEventFilterOptions by adding filters needed for detele operation
    /// </summary>
    public interface IEventDeleteOptions : IEventFilterOptions
    {
        /// <summary>
        /// Delete command won't actually delete the shown Events
        /// </summary>
        bool DryRun { get; set; }

        /// <summary>
        /// All stored Event will be deleted
        /// </summary>
        bool DeleteAllEvents { get; set; } 
    }

    /// <summary>
    /// Implements IEventFilterOptions and assigns Filters as CLI Options
    /// </summary>
    [Verb("list",HelpText = "List events to console")]
    public class ListOptions : IEventFilterOptions
    {
        [Option(Constants.Category, HelpText = Constants.CategoryHelp)]
        public string? Category { get; set; }


        [Option(Constants.Categories, HelpText = Constants.CategoriesHelp)]
        public string? Categories { get; set; }


        [Option(Constants.Description, HelpText = Constants.DescriptionHelp)]
        public string? Description { get; set; }


        [Option(Constants.Date, HelpText = Constants.DateHelp)]
        public DateOnly? Date { get; set; }


        [Option(Constants.Today, HelpText = Constants.TodayHelp)]
        public bool IsToday { get; set; }


        [Option(Constants.AfterDate, HelpText = Constants.AfterDateHelp)]
        public DateOnly? AfterDate { get; set; }


        [Option(Constants.BeforeDate, HelpText = Constants.BeforeDateHelp)]
        public DateOnly? BeforeDate { get; set; }


        [Option(Constants.NoCategory, HelpText = Constants.NoCategoryHelp)]
        public bool NoCategory { get; set; }


        [Option(Constants.Exclude, HelpText = Constants.ExcludeHelp)]
        public bool IsExcluded { get; set; }
    }

    /// <summary>
    /// Implements IEventDeleteOptions and assigns Filters as CLI Options. 
    /// Also sets all queries as one group. 
    /// This way one of them is always required.
    /// </summary>
    [Verb("delete", HelpText = "Delete event(s). Displays all deleted/would-be-deleted events")]
    public class DeleteOptions : IEventDeleteOptions
    {
        [Option(Constants.Category, HelpText = Constants.CategoryHelp, Group = Constants.Queries)] // Option needs to be set seperately from ListOptions because of GROUP paramter
        public string? Category { get; set; }


        [Option(Constants.Description, HelpText = Constants.DescriptionHelp, Group = Constants.Queries)]
        public string? Description { get; set; }


        [Option(Constants.Date, HelpText = Constants.DateHelp, Group = Constants.Queries)]
        public DateOnly? Date { get; set; }


        [Option(Constants.Today, HelpText = Constants.TodayHelp, Group = Constants.Queries)]
        public bool IsToday { get; set; }


        [Option(Constants.AfterDate, HelpText = Constants.AfterDateHelp, Group = Constants.Queries)]
        public DateOnly? AfterDate { get; set; }


        [Option(Constants.BeforeDate, HelpText = Constants.BeforeDateHelp, Group = Constants.Queries)]
        public DateOnly? BeforeDate { get; set; }


        [Option(Constants.NoCategory, HelpText = Constants.NoCategoryHelp, Group = Constants.Queries)]
        public bool NoCategory { get; set; }


        [Option(Constants.Exclude, HelpText = Constants.ExcludeHelp, Group = Constants.Queries)]
        public bool IsExcluded { get; set; }


        [Option(Constants.All, HelpText = Constants.AllHelp, Group = Constants.Queries)]
        public bool DeleteAllEvents { get; set; }


        [Option(Constants.DryRun, HelpText = Constants.DryRunHelp)]
        public bool DryRun { get; set; }


        // NOT IMPLENTED TO CLI
        public string? Categories { get; set; }
    }

    /// <summary>
    /// Includes all needed optins for adding Events.
    /// Implements IEvent that holds all needed properties for an event.
    /// Can be converted to Event
    /// </summary>
    [Verb("add", HelpText = "Add event")]
    public class AddOptions : IEvent
    {
        [Option(Constants.Category)]
        public string? Category { get; set; }

        [Option(Constants.Description, Required = true)]
        public string Description { get; set; }

        [Option(Constants.Date)]
        public DateOnly Date { get; set; }

        /// <summary>
        /// Sets default values for AddOptions
        /// </summary>
        public AddOptions()
        {
            Date = DateOnly.FromDateTime(DateTime.Now);
            Description = string.Empty;
        }
    }

    
}
