using System;
using System.Collections.Generic;
using supportDesk.Models;
using Microsoft.EntityFrameworkCore;

namespace supportDesk.Models;

public partial class Agent
{
    public int Id { get; set; }

    public Guid? Uid { get; set; }

    public string Name { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Mail { get; set; }

    public string? Designation { get; set; }
}


public static class AgentEndpoints
{
	public static void MapAgentEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Agent");

        group.MapGet("/", async (SupportdeskdbContext db) =>
        {
            return await db.Agents.ToListAsync();
        })
        .WithName("GetAllAgents")
        .Produces<List<Agent>>(StatusCodes.Status200OK);

        group.MapGet("/{id}", async  (int id, SupportdeskdbContext db) =>
        {
            return await db.Agents.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Agent model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetAgentById")
        .Produces<Agent>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        group.MapPut("/{id}", async  (int id, Agent agent, SupportdeskdbContext db) =>
        {
            var affected = await db.Agents
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.Id, agent.Id)
                  .SetProperty(m => m.Uid, agent.Uid)
                  .SetProperty(m => m.Name, agent.Name)
                  .SetProperty(m => m.Phone, agent.Phone)
                  .SetProperty(m => m.Mail, agent.Mail)
                  .SetProperty(m => m.Designation, agent.Designation)
                  );
            return affected == 1 ? Results.Ok() : Results.NotFound();
        })
        .WithName("UpdateAgent")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        group.MapPost("/", async (Agent agent, SupportdeskdbContext db) =>
        {
            db.Agents.Add(agent);
            await db.SaveChangesAsync();
            return Results.Created($"/api/Agent/{agent.Id}",agent);
        })
        .WithName("CreateAgent")
        .Produces<Agent>(StatusCodes.Status201Created);

        group.MapDelete("/{id}", async  (int id, SupportdeskdbContext db) =>
        {
            var affected = await db.Agents
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? Results.Ok() : Results.NotFound();
        })
        .WithName("DeleteAgent")
        .Produces<Agent>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}