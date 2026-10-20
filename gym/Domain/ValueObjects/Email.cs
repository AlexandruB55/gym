namespace gym.Domain.ValueObjects;

public sealed record Email(string Value)
{
    public static Email Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || !value.Contains("@"))
            throw new ArgumentException("Email invalid");
        return new Email(value);
    }
    public override string ToString() => Value;
}