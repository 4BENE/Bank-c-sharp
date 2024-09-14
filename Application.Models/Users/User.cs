namespace Application.Models.Users;

public class User
{
    public User(string id, string password, int money)
    {
        Id = id;
        Password = password;
        Money = money;
    }

    public string Id { get; private set; }

    public string Password { get; private set; }

    public int Money { get; set; }
}
