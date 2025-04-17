namespace Core.Entities;

public class User
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Username { get; private set; }
    public string Password { get; private set; }

    public User(string username, string password)
    {
        Username = username;
        Password = password;
    }
}