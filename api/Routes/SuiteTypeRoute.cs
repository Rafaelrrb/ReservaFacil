using Reserva.Facil.Context;
using SuiteType.Request;
using SuiteType.Model;
using Microsoft.EntityFrameworkCore; // Add this line if SuiteTypeModel is in SuiteType.Models namespace

namespace SuiteType.Route;

public static class SuiteTypeRoute {
  public static void SuiteTypeRoutes(this WebApplication app){
    var route = app.MapGroup("type");

    route.MapPost("", async (SuiteTypeRequest req, ReservaFacilContext context) => { 
      var suiteType = new SuiteTypeModel(req.description, req.price);
      await context.AddAsync(suiteType);
      await context.SaveChangesAsync();
      return Results.Ok();
    });

    route.MapGet("", async (ReservaFacilContext context) => {
      var suiteType = await context.SuiteTypes.ToListAsync();
      return Results.Ok(suiteType);
    });

  }
}
