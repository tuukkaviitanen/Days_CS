using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Models
{
    /// <summary>
    /// Included all more than once used constants in Options
    /// </summary>
    static class Constants
    {
        internal const string Category = "category";
        internal const string Categories = "categories";
        internal const string Description = "description";
        internal const string Queries = "Queries";
        internal const string Date = "date";
        internal const string Today = "today";
        internal const string AfterDate = "after-date";
        internal const string BeforeDate = "before-date";
        internal const string NoCategory = "no-category";
        internal const string Exclude = "exclude";
        internal const string All = "all";
        internal const string DryRun = "dry-run";
    }

    /// <summary>
    /// Includes all default filters for Events
    /// </summary>
    public interface IEventFilterOptions
    {
        public string? Category { get; set; }
        public string? Categories { get; set; }
        public string? Description { get; set; }
        public DateOnly? Date { get; set; }
        public bool IsToday { get; set; }
        public DateOnly? AfterDate { get; set; }
        public DateOnly? BeforeDate { get; set; }
        public bool NoCategory { get; set; }
        public bool IsExcluded { get; set; }
    }
    /// <summary>
    /// Extends IEventFilterOptions by adding filters needed for detele operation
    /// </summary>
    public interface IEventDeleteOptions : IEventFilterOptions
    {
        bool DryRun { get; set; }
        bool DeleteAllMatchingEvents { get; set; } 
    }

    /// <summary>
    /// Implements IEventFilterOptions and assigns Filters as CLI Options
    /// </summary>
    [Verb("list",HelpText = "List events to console")]
    public class ListOptions : IEventFilterOptions
    {
        [Option(Constants.Category)]
        public string? Category { get; set; }


        [Option(Constants.Categories)]
        public string? Categories { get; set; }


        [Option(Constants.Description)]
        public string? Description { get; set; }


        [Option(Constants.Date)]
        public DateOnly? Date { get; set; }


        [Option(Constants.Today)]
        public bool IsToday { get; set; }


        [Option(Constants.AfterDate)]
        public DateOnly? AfterDate { get; set; }


        [Option(Constants.BeforeDate)]
        public DateOnly? BeforeDate { get; set; }


        [Option(Constants.NoCategory)]
        public bool NoCategory { get; set; }


        [Option(Constants.Exclude)]
        public bool IsExcluded { get; set; }
    }

    /// <summary>
    /// Implements IEventDeleteOptions and assigns Filters as CLI Options. 
    /// Also sets all queries as one group. 
    /// This way one of them is always required.
    /// </summary>
    [Verb("delete", HelpText = "Delete event(s)")]
    public class DeleteOptions : IEventDeleteOptions
    {
        [Option(Constants.Category, Group = Constants.Queries)]
        public string? Category { get; set; }


        [Option(Constants.Description, Group = Constants.Queries)]
        public string? Description { get; set; }


        [Option(Constants.Date, Group = Constants.Queries)]
        public DateOnly? Date { get; set; }


        [Option(Constants.Today, Group = Constants.Queries)]
        public bool IsToday { get; set; }


        [Option(Constants.AfterDate, Group = Constants.Queries)]
        public DateOnly? AfterDate { get; set; }


        [Option(Constants.BeforeDate, Group = Constants.Queries)]
        public DateOnly? BeforeDate { get; set; }


        [Option(Constants.NoCategory, Group = Constants.Queries)]
        public bool NoCategory { get; set; }


        [Option(Constants.Exclude, Group = Constants.Queries)]
        public bool IsExcluded { get; set; }


        [Option(Constants.All, Group = Constants.Queries)]
        public bool DeleteAllMatchingEvents { get; set; }


        [Option(Constants.DryRun)]
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
