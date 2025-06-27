using DTOs;
using Models;

namespace Services;

public interface ITaskService
{
    IEnumerable<TaskDto> GetAll();
    TaskDto? GetById(int id);
    TaskDto Create(CreateTaskRequest request);
    bool Update(int id, TaskDto dto);
    bool Delete(int id);
    TaskComment AddComment(int taskId, AddCommentRequest request);
    bool UpdateStatus(int taskId, TaskStatusUpdateRequest request);
    IEnumerable<Models.Task> Search(string? status, string? assignedTo, DateTime? dueBefore);
}
