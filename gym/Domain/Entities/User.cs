using gym.Domain.ValueObjects;

namespace gym.Domain.Entities;

public abstract class User 
{
    public Guid Id { get; init; } = Guid.NewGuid(); 
    public string Name { get; init; }
    public Email Email { get; init; }

    protected User(string name, Email email) 
    {
        Name = name;
        Email = email;
    }
}