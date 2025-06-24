namespace Orbital7.RapidApp;

public class RATableViewSegment<TItem>
{
    public string? HeaderText { get; init; }

    public IDictionary<int, TItem> Items { get; init; }

    public bool HasItems =>
        Items != null &&
        Items.Count > 0;

    public RATableViewSegment(
        ICollection<TItem> items)
    {
        var i = 0;

        Items = items.ToDictionary(
            x => ++i,
            x => x);
    }

    public RATableViewSegment(
        string headerText, 
        ICollection<TItem> items) :
        this(items)
    {
        HeaderText = headerText;
    }
}
