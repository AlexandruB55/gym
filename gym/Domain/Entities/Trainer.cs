using gym.Domain.ValueObjects;

namespace gym.Domain.Entities;

public sealed class Trainer : User 
{
    public string Specialization { get; set; }
    public Trainer(string name, Email email, string specialization) 
        : base(name, email) => Specialization = specialization;
}