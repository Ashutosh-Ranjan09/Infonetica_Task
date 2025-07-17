using Engine.Models;
using Engine.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.MapPost("/definitions", (Definition def) =>
{
    try
    {
        EngineServices.AddDefinition(def);
        return Results.Ok();
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
});
app.MapGet("/definitions/{id}",(string id)=>
{
    return EngineServices.Definitions.TryGetValue(id, out var def) ? Results.Ok(def) : Results.NotFound();
});
app.MapPost("/instances/{defId}", (string defId) =>
{
    try
    {
        var inst = EngineServices.StartInstance(defId);
        return Results.Ok(inst);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
});
app.MapPost("/instances/{id}/actions/{actionId}", (string id, string actionId) =>
{
    try
    {
        var updated = EngineServices.ExecuteAction(id, actionId);
        return Results.Ok(updated);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

app.MapGet("/instances/{id}", (string id) =>
{
    return EngineServices.Instances.TryGetValue(id, out var inst) ? Results.Ok(inst) : Results.NotFound();
});

app.Run();