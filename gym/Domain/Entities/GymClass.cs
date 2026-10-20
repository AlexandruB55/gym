namespace gym.Domain.Entities;

public sealed class GymClass 
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Type { get; set; }
    public int Capacity { get; set; }
    public string TrainerName { get; set; } // Cerința: asocierea antrenorilor
    public List<Guid> ParticipantIds { get; } = new();

    public GymClass(string type, int capacity, string trainerName) 
    {
        Type = type;
        Capacity = capacity;
        TrainerName = trainerName;
    }

    public bool Reserve(Guid clientId) 
    {
        if (ParticipantIds.Count >= Capacity) return false;
        if (!ParticipantIds.Contains(clientId)) ParticipantIds.Add(clientId);
        return true;
    }
}