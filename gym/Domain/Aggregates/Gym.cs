using gym.Domain.Entities;

namespace gym.Domain.Aggregates;

public class Gym 
{
    private readonly List<GymClass> _classes = new();
    public IReadOnlyCollection<GymClass> Classes => _classes;

    public void AddClass(GymClass gymClass) => _classes.Add(gymClass);

    public bool ReserveClass(Guid classId, Client client) 
    {
        var cls = _classes.FirstOrDefault(c => c.Id == classId);
        if (cls == null) return false;
        return cls.Reserve(client.Id); 
    }
}