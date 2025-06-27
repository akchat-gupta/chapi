namespace Repositories;

public interface ITaskRepository
{
    IEnumerable<Models.Task> GetAll();
    Models.Task? GetById(int id);
    Models.Task Create(Models.Task task);
    bool Update(Models.Task task);
    bool Delete(int id);
    IEnumerable<Models.Task> Search(string? status, string? assignedTo, DateTime? dueBefore);
}
