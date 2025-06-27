using Data;
using Models;

namespace Chapi.Repositories
{
    public class StatusLogRepository : IStatusLogRepository
    {
        private readonly AppDbContext _context;
        public StatusLogRepository(AppDbContext context) => _context = context;

        public TaskStatusLog AddStatusLog(int taskId, string newStatus, string reason)
        {
            var log = new TaskStatusLog
            {
                TaskId = taskId,
                Status = newStatus,
                Reason = reason
            };
            _context.TaskStatusLogs.Add(log);
            _context.SaveChanges();
            return log;
        }

        public IEnumerable<TaskStatusLog> GetLogs(int taskId) =>
            _context.TaskStatusLogs.Where(l => l.TaskId == taskId).ToList();
    }
}
