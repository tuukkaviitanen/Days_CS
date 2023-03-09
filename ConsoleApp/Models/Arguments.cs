using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Models
{
    internal class Arguments
    {
        public Arguments()
        {
            Categories = new List<string>();
            IsExcluded = false;
        }
        public List<string>? Categories { get; set; }
        public List<string>? Descriptions { get; set; }
        public DateOnly? BeforeDate { get; set; }
        public DateOnly? AfterDate { get; set; }
        public DateOnly? Date { get; set; }
        public bool IsExcluded { get; set; }
    }
}
