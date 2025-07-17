namespace Engine.Models;

public class Instance
{
    public string Id { get; set; } = Guid.NewGuid().ToString();//G-u-id like uuid in javascript
    public string DefinitionId { get; set; } = default!;
    public string CurrentState { get; set; } = default!;
    public List<(string ActionId, DateTime Timestamp)> History { get; set; } = new();//ordered pairs
}