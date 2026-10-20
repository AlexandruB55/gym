using gym.Domain.ValueObjects;

namespace gym.Domain.Entities;

public sealed class Client : User 
{
    public List<Subscription> Subscriptions { get; } = new();
    
    public Client(string name, Email email) : base(name, email) { }
    
    public bool HasActiveSubscription() => Subscriptions.Any(s => s.IsActive());

    public void AddSubscription(Subscription subscription) => Subscriptions.Add(subscription);
}