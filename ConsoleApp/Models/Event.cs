using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Models;
public class Event
{
    public Event(string? category, string description, DateOnly timeStamp)
    {
        Category = category;
        Description = description;
        TimeStamp = timeStamp;
    }
    public string? Category { get; set; }   
    public string Description { get; set; }
    public DateOnly TimeStamp { get; set; }

    public override string ToString()
    {
        return $"{Category}:{Description}:{TimeStamp:yyyy-MM-dd}";
    }
}
