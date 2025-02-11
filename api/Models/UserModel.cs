using System.ComponentModel.DataAnnotations;
using System.Text;
using Reservation.Model;

namespace User.Model;

public class UserModel{
  public UserModel() { }
  public UserModel(string name, string email, string password){
    Id = Guid.NewGuid();
    Name = name;
    Email = email;
    CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
    PasswordHash = passwordHash;
    PasswordSalt = passwordSalt;
  }
  public Guid Id { get; init;}
  [Required]
  public string Name { get; private set; }
  public void ChangeName(string name) => Name = name;

  [Required, EmailAddress]
  public string Email { get; private set; }
  public void ChangeEmail(string email) => Email = email;

  public byte[] PasswordHash { get; private set; }
  public byte[] PasswordSalt { get; private set; }

  public virtual ICollection<ReservationModel> Reservations { get; private set; }

public void ChangePassword(string password) {
        CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
        PasswordHash = passwordHash;
        PasswordSalt = passwordSalt;
}

private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
{
        using (var hmac = new System.Security.Cryptography.HMACSHA512())
        {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
}

public bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
{

        using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
        {
                byte[] computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(storedHash);
        }
}
}
