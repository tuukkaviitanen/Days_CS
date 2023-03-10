using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Models
{
    public class QueryOptions
    {
        [Option("categories")]
        public string? Categories { get; set; }

        [Option("descriptions")]
        public string? Descriptions { get; set; }

        [Option("date")]
        public DateOnly? Date { get; set; }

        [Option("today")]
        public bool IsToday { get; set; }

        [Option("after-date")]
        public DateOnly? AfterDate { get; set; }

        [Option("before-date")]
        public DateOnly? BeforeDate { get; set; }

        [Option("no-category")]
        public bool NoCategory { get; set; }

        [Option("exclude")]
        public bool IsExcluded { get; set; }
    }

    [Verb("list",HelpText = "List events to console")]
    public class ListOptions : QueryOptions
    {
        
    }

    [Verb("delete", HelpText = "Delete event(s)")]
    public class DeleteOptions : QueryOptions
    {
        [Option("dry-run")]
        public bool DryRun { get; set; }

        [Option("all")]
        public bool All { get; set; }
    }

    [Verb("add", HelpText = "Add event")]
    public class AddOptions
    {
        [Option("category")]
        public string? Category { get; set; }

        [Option("description", Required = true)]
        public string Description { get; set; }

        [Option("date")]
        public DateOnly Date { get; set; }

        public AddOptions()
        {
            Date = DateOnly.FromDateTime(DateTime.Now);
            Description = string.Empty;
        }
    }

    
}
