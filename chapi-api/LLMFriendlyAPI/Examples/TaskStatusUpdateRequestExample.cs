using Swashbuckle.AspNetCore.Filters;
using LLMFriendlyAPI.DTOs;

namespace LLMFriendlyAPI.Examples
{
    public class TaskStatusUpdateRequestExample : IExamplesProvider<TaskStatusUpdateRequest>
    {
        public TaskStatusUpdateRequest GetExamples()
        {
            return new TaskStatusUpdateRequest
            {
                NewStatus = Enums.TaskStatus.Completed,
                Reason = "QA approved and merged to main branch."
            };
        }
    }
}
