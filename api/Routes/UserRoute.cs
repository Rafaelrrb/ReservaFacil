using Reserva.Facil.Context;
using User.Model;
using User.Request;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace User.Route;

public static class UserRoute {
  public static void UserRoutes(this WebApplication app){
    var route = app.MapGroup("user");

    
    route.MapPost("", async (UserRequest req, ReservaFacilContext context) => { 
      var user = new UserModel(req.Name, req.Email, req.PasswordHash);
      await context.AddAsync(user);
      await context.SaveChangesAsync();
      return Results.Ok();
    });

    
    route.MapPost("login", async (UserRequest req, ReservaFacilContext context) => {
      var user = await context.Users.FirstOrDefaultAsync(u => u.Email == req.Email);
      if (user == null || !user.VerifyPasswordHash(req.PasswordHash, user.PasswordHash, user.PasswordSalt))
            return Results.Json(new { access_token = "undefined" });

      var access_token = GenerateJwtToken(user);
      return Results.Ok(new { access_token });
    });

    route.MapGet("", async (ReservaFacilContext context) => {
      var users = await context.Users.ToListAsync();
      return Results.Ok(users);
    });

    
    route.MapPut("/{id:guid}", [Authorize] async (Guid id, UserRequest req, ReservaFacilContext context) => {
      var user = await context.Users.FirstOrDefaultAsync(u => u.Id == id);

      if(user == null) 
        return Results.NotFound();

      user.ChangeName(req.Name);
      user.ChangeEmail(req.Email);
      user.ChangePassword(req.PasswordHash);

      await context.SaveChangesAsync();
      return Results.Ok(user);
    });


    route.MapDelete("/{id:guid}", [Authorize] async (Guid id, ReservaFacilContext context) => {
      var user = await context.Users.FirstOrDefaultAsync(u => u.Id == id);

      if(user == null) 
        return Results.NotFound();

      context.Users.Remove(user);

      await context.SaveChangesAsync();
      return Results.Ok(user);
    });
  }

  private static string GenerateJwtToken(UserModel user)
  {
      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes("sua-chave-secreta");
      var tokenDescriptor = new SecurityTokenDescriptor
      {
          Subject = new ClaimsIdentity(new Claim[]
          {
          new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
          new Claim(ClaimTypes.Name, user.Name)
          }),
          Expires = DateTime.UtcNow.AddDays(7),
          SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };
      var token = tokenHandler.CreateToken(tokenDescriptor);
      return tokenHandler.WriteToken(token);
  }
}