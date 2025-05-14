namespace Domain;

public class User
{
    public User
    (
        string login,
        string password,
        string name,
        int gender,
        DateOnly? birthday,
        bool isAdmin,
        string createdBy
    )
    {
        Guid = Guid.NewGuid();
        Login = login;
        Password = password;
        Name = name;
        Gender = gender;
        Birthday = birthday;
        IsAdmin = isAdmin;
        CreatedOn = DateTime.UtcNow;
        CreatedBy = createdBy;
    }

    public Guid Guid { get; set; }
    public string? Login { get; set; }
    public string? Password { get; set; }
    public string? Name { get; set; }
    public int Gender { get; set; }
    public DateOnly? Birthday { get; set; }
    public bool IsAdmin { get; set; }
    public DateTime CreatedOn { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public string? ModifiedBy { get; set; }
    
    public bool IsRestored { get; set; }
    public DateTime? RestoredOn { get; set; }
    public string? RestoredBy { get; set; }
}