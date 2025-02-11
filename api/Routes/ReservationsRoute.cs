using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Reserva.Facil.Context;
using Reservation.Request;
using Reservation.Model;

namespace Reservation.Route;

public static class ReservationRoute
{
  public static void ReservationsRoutes(this WebApplication app)
  {
    var route = app.MapGroup("reservations");

    route.MapPost("/{idUser}/{idSuite}/{idMotel}", async (Guid idUser, Guid idSuite, Guid idMotel, ReservationRequest req, ReservaFacilContext context) =>
    {
      var reservation = new ReservationModel(idUser, idSuite, idMotel, req.DataEntrada, req.DataSaida, req.Status);
    
      context.Reservations.Add(reservation);
      await context.SaveChangesAsync();
    
      return Results.Ok(reservation);
    });

    route.MapGet("/{idMotel}", async (Guid idMotel, ReservaFacilContext context) =>
    {
      var query = context.Reservations
          .Include(r => r.User)
          .Include(r => r.Suite)
          .Where(r => r.IdMotel == idMotel); 

      var reservations = await query.ToListAsync();
      return Results.Ok(reservations);
    });

    route.MapGet("/{idMotel}/{date}", async (Guid idMotel, DateOnly date, ReservaFacilContext context) =>
    {
      var query = context.Reservations
          .Include(r => r.User)
          .Include(r => r.Suite)
          .Where(r => r.IdMotel == idMotel && DateOnly.FromDateTime(r.DataEntrada) == date);

      var reservations = await query.ToListAsync();
      return Results.Ok(reservations);
    });

    route.MapGet("/revenue/{idMotel}/{month}/{year}", async (Guid idMotel, int month, int year, ReservaFacilContext context) =>
    {

      var startDate = new DateTime(year, month, 1);
      var endDate = startDate.AddMonths(1).AddDays(-1);

      var query = context.Reservations
        .Include(r => r.Suite)
        .ThenInclude(s => s.SuiteType)
        .Where(r => r.IdMotel == idMotel &&
              r.DataSaida >= startDate && r.DataSaida <= endDate);

      var reservations = await query.ToListAsync();
      var revenue = reservations.Sum(r => r.Suite.SuiteType.Price);

      return Results.Ok(new { Revenue = revenue });
    });



    
  }
}
