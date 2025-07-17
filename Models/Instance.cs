
public class Instance
{
    public class HistoryEntry
{
    public string ActionId { get; set; } = default!;
    public DateTime Timestamp { get; set; }
}
    public string Id { get; set; } = Guid.NewGuid().ToString();//G-u-id like uuid in javascript
    public string DefinitionId { get; set; } = default!;
    public string CurrentState { get; set; } = default!;
    public List<HistoryEntry> History { get; set; } = new();//ordered pairs
}