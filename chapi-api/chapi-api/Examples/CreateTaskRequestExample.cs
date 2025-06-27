using Swashbuckle.AspNetCore.Filters;
using DTOs;

namespace Examples;

public class CreateTaskRequestExample : IExamplesProvider<CreateTaskRequest>
{
    public CreateTaskRequest GetExamples()
    {
        return new CreateTaskRequest
        {
            Title = "Submit Report",
            Description = "Compile and submit the Q2 performance report to management.",
            DueDate = DateTime.UtcNow.AddDays(7)
        };
    }
}
