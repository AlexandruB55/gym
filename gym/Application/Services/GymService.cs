using gym.Domain.Entities;
using gym.Domain.Aggregates;

namespace gym.Application.Services;

public class GymService 
{
    private readonly Gym _gym = new();
    public IReadOnlyCollection<GymClass> Classes => _gym.Classes;

    public void CreateClass(string type, int cap, string trainer) => 
        _gym.AddClass(new GymClass(type, cap, trainer));

    public bool MakeReservation(Guid classId, Client client) => 
        _gym.ReserveClass(classId, client);
}
