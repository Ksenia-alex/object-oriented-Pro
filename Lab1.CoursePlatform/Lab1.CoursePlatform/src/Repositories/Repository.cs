using Lab1.CoursePlatform.Interfaces;
using System.Collections.Concurrent;
using Lab1.CoursePlatform.Models.Persons;

namespace Lab1.CoursePlatform.Repositories;

public class Repository<T>(Func<T, string> getIdFunc) : InterfaceRepository<T>
    where T : class
{
    protected readonly ConcurrentDictionary<string, T> _concurrentDictionary = new();

    private readonly Func<T, string> _getId = getIdFunc ?? throw new ArgumentNullException(nameof(getIdFunc));

    public void Add(T entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        string id = _getId(entity);
        _concurrentDictionary.TryAdd(id, entity);
    }

    public void Delete(string? id)
    {
        if (string.IsNullOrWhiteSpace(id)) return;
        _concurrentDictionary.TryRemove(id, out _);
    }

    public void Update(T entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        string id = _getId(entity);
        _concurrentDictionary[id] = entity;
    }

    public T? GetById(string id)
    {
        if (string.IsNullOrEmpty(id)) return null;
        
        _concurrentDictionary.TryGetValue(id, out var entity);
        return entity;
    }

    public IEnumerable<T> GetAll()
    {
        return _concurrentDictionary.Values.ToList();
    }
}
