namespace gym.Domain.Entities;

public sealed class Subscription 
{
    public string Type { get; }
    public DateTime ExpirationDate { get; }

    public Subscription(string type, int months) 
    {
        Type = type;
        ExpirationDate = DateTime.Now.AddMonths(months);
    }

    public bool IsActive() => DateTime.Now <= ExpirationDate;
}