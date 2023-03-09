using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Models
{
    internal class Options
    {
        [Option("categories")]
        public string? Categories { get; set; }

        [Option("categories")]
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
}
