using CsvHelper.Configuration.Attributes;

namespace Days.Models;
/// <summary>
/// Holds all required properties for Events
/// </summary>
public interface IEvent
{
    public DateOnly Date { get; set; }
    public string? Category { get; set; }
    public string Description { get; set; }
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
    /// Way to create Event out of any class that implements IEvent.
    /// </summary>
    /// <param name="iEvent">Any class that implemets IEvent</param>
    public Event(IEvent iEvent)
    {
        Category = iEvent.Category;
        Description = iEvent.Description;
        Date = iEvent.Date;
    }

    // Name attributes are for CsvHelper to write lowercase headers
    [Name("date"), Format("yyyy-MM-dd")]
    public DateOnly Date { get; set; }

    [Name("category")]
    public string? Category { get; set; }

    [Name("description")]
    public string Description { get; set; }

    

    public override string ToString()
    {
        var categoryString = !string.IsNullOrEmpty(Category) ? $"({Category})" : string.Empty;
        return $"{Date:yyyy-MM-dd}: {Description} {categoryString}";
    }
}
