using LLMFriendlyAPI.DTOs;
using LLMFriendlyAPI.Models;
using LLMFriendlyAPI.Repositories;

namespace LLMFriendlyAPI.Services
{
    public class TaskService(ITaskRepository taskRepo, ICommentRepository commentRepo, IStatusLogRepository statusRepo) : ITaskService
    {
        private readonly ITaskRepository _taskRepo = taskRepo;
        private readonly ICommentRepository _commentRepo = commentRepo;
        private readonly IStatusLogRepository _statusRepo = statusRepo;

        public IEnumerable<TaskDto> GetAll() =>
            _taskRepo.GetAll().Select(MapToDto);

        public TaskDto? GetById(int id)
        {
            var task = _taskRepo.GetById(id);
            return task == null ? null : MapToDto(task);
        }

        public TaskDto Create(CreateTaskRequest request)
        {
            var task = new Models.Task
            {
                Title = request.Title,
                Description = request.Description,
                DueDate = request.DueDate
            };
            return MapToDto(_taskRepo.Create(task));
        }

        public bool Update(int id, TaskDto dto)
        {
            var task = _taskRepo.GetById(id);
            if (task == null) return false;

            task.Title = dto.Title;
            task.Description = dto.Description;
            task.DueDate = dto.DueDate;
            task.IsCompleted = dto.IsCompleted;

            return _taskRepo.Update(task);
        }

        public bool Delete(int id) => _taskRepo.Delete(id);

        public TaskComment AddComment(int taskId, AddCommentRequest request) =>
            _commentRepo.AddComment(taskId, request.Content);

        public bool UpdateStatus(int taskId, TaskStatusUpdateRequest request)
        {
            var task = _taskRepo.GetById(taskId);
            if (task == null) return false;

            task.IsCompleted = request.NewStatus == Enums.TaskStatus.Completed;
            _statusRepo.AddStatusLog(taskId, request.NewStatus.ToString(), request.Reason);
            return _taskRepo.Update(task);
        }

        public IEnumerable<Models.Task> Search(string? status, string? assignedTo, DateTime? dueBefore) =>
            _taskRepo.Search(status, assignedTo, dueBefore);

        private TaskDto MapToDto(Models.Task task) => new()
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            DueDate = task.DueDate,
            IsCompleted = task.IsCompleted
        };
    }

}
