using System.Net;
using Engine.Models;
namespace Engine.Services;

public static class EngineServices
{
    public static Dictionary<string, Definition> Definitions = new();
    public static Dictionary<string, Instance> Instances = new();
    public static void AddDefinition(Definition def)
    {
        if (Definitions.ContainsKey(def.Id))
            throw new Exception("Duplicate definition Id.");
        if (def.States.Count(s => s.IsInitial) != 1)
            throw new Exception("Can't have multiple initial states");
        if (def.States.Any(s => string.IsNullOrWhiteSpace(s.Id)))
            throw new Exception("All states must have non-empty IDs");

        Definitions[def.Id] = def;
    }
    public static Instance StartInstance(string defId)
    {
        if (!Definitions.TryGetValue(defId, out var def))
            throw new Exception("Defintion not found.");
        var initial = def.States.FirstOrDefault(s => s.IsInitial && s.Enabled) ?? throw new Exception("NO enabled initial state.");
        var instance = new Instance { DefinitionId = defId, CurrentState = initial.Id };
        Instances[instance.Id] = instance;
        return instance;
    }
    public static Instance ExecuteAction(string instanceId, string actionId)
    {
        if (!Instances.TryGetValue(instanceId, out var inst))
            throw new Exception("Instance not found");
        var def = Definitions[inst.DefinitionId];
        var action = def.Actions.FirstOrDefault(a => a.Id == actionId) ?? throw new Exception("Action not found in workflow");
        if (!action.Enabled)
            throw new Exception("Action is disabled");
        if (!action.FromStates.Contains(inst.CurrentState))
            throw new Exception("Action not allowed from current state");
        var newState = def.States.FirstOrDefault(s => s.Id == action.ToState && s.Enabled) ?? throw new Exception("Target state not found or not enabled");
        if (def.States.First(s => s.Id == inst.CurrentState).IsFinal)
            throw new Exception("Cannot transition from a final state");

        inst.CurrentState = newState.Id;
        inst.History.Add((action.Id,DateTime.UtcNow));
        return inst;
    }

};