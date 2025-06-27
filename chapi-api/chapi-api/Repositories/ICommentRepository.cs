using Models;

namespace Chapi.Repositories
{
    public interface ICommentRepository
    {
        TaskComment AddComment(int taskId, string content);
        IEnumerable<TaskComment> GetComments(int taskId);
    }
}
