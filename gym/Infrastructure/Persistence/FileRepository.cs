using System.Text.Json;

namespace Infrastructure.Persistence;

public class FileRepository<T>
{
    private readonly string _file;

    public FileRepository(string file)
    {
        _file = file;
    }

    public void Save(List<T> data)
    {
        var json = JsonSerializer.Serialize(data, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        File.WriteAllText(_file, json);
    }

    public List<T> Load()
    {
        if (!File.Exists(_file))
            return new List<T>();

        var json = File.ReadAllText(_file);
        return JsonSerializer.Deserialize<List<T>>(json) ?? new();
    }
}