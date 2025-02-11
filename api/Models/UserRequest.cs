namespace User.Request;

public record UserRequest(string Name, string Email, string PasswordHash);