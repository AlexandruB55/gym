using gym.Domain.ValueObjects;

namespace gym.Domain.Entities;

public sealed class Admin : User 
{
    public Admin(string name, Email email) 
        : base(name, email) 
    {
        
    }
}