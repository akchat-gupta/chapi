using LLMFriendlyAPI.Data;

namespace LLMFriendlyAPI.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Models.Task> GetAll() => _context.Tasks.ToList();

        public Models.Task? GetById(int id) => _context.Tasks.Find(id);

        public Models.Task Create(Models.Task task)
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();
            return task;
        }

        public bool Update(Models.Task task)
        {
            _context.Tasks.Update(task);
            return _context.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task == null) return false;

            _context.Tasks.Remove(task);
            return _context.SaveChanges() > 0;
        }

        public IEnumerable<Models.Task> Search(string? status, string? assignedTo, DateTime? dueBefore)
        {
            var query = _context.Tasks.AsQueryable();

            if (!string.IsNullOrEmpty(status))
            {
                bool isCompleted = status.ToLower() == "completed";
                query = query.Where(t => t.IsCompleted == isCompleted);
            }

            if (dueBefore.HasValue)
            {
                query = query.Where(t => t.DueDate < dueBefore.Value);
            }

            // `assignedTo` placeholder for future use
            return query.ToList();
        }
    }
}
