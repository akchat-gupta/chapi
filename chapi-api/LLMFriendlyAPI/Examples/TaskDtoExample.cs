using Swashbuckle.AspNetCore.Filters;
using LLMFriendlyAPI.DTOs;

namespace LLMFriendlyAPI.Examples
{
    public class TaskDtoExample : IExamplesProvider<TaskDto>
    {
        public TaskDto GetExamples()
        {
            return new TaskDto
            {
                Id = 101,
                Title = "Fix login issue",
                Description = "Resolve the intermittent 500 error during user login.",
                DueDate = DateTime.UtcNow.AddDays(3),
                IsCompleted = false
            };
        }
    }
}
