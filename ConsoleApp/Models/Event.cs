using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ConsoleApp.Models.Event;

namespace ConsoleApp.Models;
/// <summary>
/// Holds all required properties for Events
/// </summary>
public interface IEvent
{
    public string? Category { get; set; }
    public string Description { get; set; }
    public DateOnly Date { get; set; }
}

/// <summary>
/// Implements IEvent
/// </summary>
public class Event : IEvent
{
    public Event(string? category, string description, DateOnly date) 
    {
        Category = category;
        Description = description;
        Date = date;
    }

    /// <summary>
    /// Way to create Event out of any class that implements IEvent
    /// </summary>
    /// <param name="iEvent">Any class that implemets IEvent</param>
    public Event(IEvent iEvent)
    {
        Category = iEvent.Category;
        Description = iEvent.Description;
        Date = iEvent.Date;
    }
    public string? Category { get; set; }   
    public string Description { get; set; }
    public DateOnly Date { get; set; }

    public override string ToString()
    {
        return $"{Category}:{Description}:{Date:yyyy-MM-dd}";
    }
}
