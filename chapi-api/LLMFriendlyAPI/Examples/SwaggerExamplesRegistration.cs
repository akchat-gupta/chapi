using Swashbuckle.AspNetCore.Filters;
using LLMFriendlyAPI.DTOs;

namespace LLMFriendlyAPI.Examples
{
    public static class SwaggerExamplesRegistration
    {
        public static void RegisterSwaggerExamples(this IServiceCollection services)
        {
            services.AddSwaggerExamplesFromAssemblies(
                typeof(CreateTaskRequestExample).Assembly
            );

            services.AddTransient<IExamplesProvider<CreateTaskRequest>, CreateTaskRequestExample>();
            services.AddTransient<IExamplesProvider<TaskDto>, TaskDtoExample>();
            services.AddTransient<IExamplesProvider<AddCommentRequest>, AddCommentRequestExample>();
            services.AddTransient<IExamplesProvider<TaskStatusUpdateRequest>, TaskStatusUpdateRequestExample>();
        }
    }
}
