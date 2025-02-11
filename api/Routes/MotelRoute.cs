using Reserva.Facil.Context;
using Microsoft.EntityFrameworkCore;
using Motel.Request;
using Motel.Model;
using Suite.Request;
using Suite.Model;

namespace Motel.Route;

public static class MotelRoute {
  public static void MotelRoutes(this WebApplication app){
    var route = app.MapGroup("motel");

    route.MapPost("", async (MotelRequest req, ReservaFacilContext context) => { 
      var motel = new MotelModel(req.Name, req.Address, req.Phone);
      await context.AddAsync(motel);
      await context.SaveChangesAsync();
      return Results.Ok();
    });

    route.MapGet("", async (ReservaFacilContext context) => {
      var motels = await context.Motels.Include(m => m.Suites).ToListAsync();
    
      return Results.Ok(motels);
    });

    route.MapPut("/{id:guid}", async (Guid id, MotelRequest req, ReservaFacilContext context) => {
      var motel = await context.Motels.FirstOrDefaultAsync(m => m.Id == id);

      if(motel == null) 
        return Results.NotFound();

      motel.ChangePhone(req.Phone);

      await context.SaveChangesAsync();
      return Results.Ok(motel);
    });

    route.MapDelete("/{id:guid}", async (Guid id, ReservaFacilContext context) => {
      var motel = await context.Motels.FirstOrDefaultAsync(m => m.Id == id);

      if(motel == null) 
        return Results.NotFound();

      context.Remove(motel);
      await context.SaveChangesAsync();
      return Results.Ok();
    });

    // rota para criar suites
    route.MapPost("/{id:guid}/suites/{typeid:guid}", async (Guid id, Guid typeid, SuiteRequest req, ReservaFacilContext context) => {
      var motel = await context.Motels.Include(m => m.Suites).FirstOrDefaultAsync(m => m.Id == id);

      if(motel == null) 
      return Results.NotFound();

      var suiteType = await context.SuiteTypes.FirstOrDefaultAsync(st => st.Id == typeid);

      if(suiteType == null) 
      return Results.NotFound();

      var suite = new SuiteModel(req.numero, id, typeid, req.status);
      
      await context.AddAsync(suite);
      await context.SaveChangesAsync();
      return Results.Ok(suite);
    });
  }
}
