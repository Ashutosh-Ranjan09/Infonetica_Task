namespace Engine.Models;

public class Definition
{
    public string Id { get; set; } = default!;
    public List<State> States { get; set; } = new();
    public List<Action> Actions { get; set; } = new();
}