using Data;
using Models;

namespace Chapi.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext _context;
        public CommentRepository(AppDbContext context) => _context = context;

        public TaskComment AddComment(int taskId, string content)
        {
            var comment = new TaskComment { TaskId = taskId, Content = content };
            _context.TaskComments.Add(comment);
            _context.SaveChanges();
            return comment;
        }

        public IEnumerable<TaskComment> GetComments(int taskId) =>
            _context.TaskComments.Where(c => c.TaskId == taskId).ToList();
    }
}
