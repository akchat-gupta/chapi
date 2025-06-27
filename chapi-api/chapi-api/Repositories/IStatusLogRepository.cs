using Models;

namespace Chapi.Repositories
{
    public interface IStatusLogRepository
    {
        TaskStatusLog AddStatusLog(int taskId, string newStatus, string reason);
        IEnumerable<TaskStatusLog> GetLogs(int taskId);
    }
}
