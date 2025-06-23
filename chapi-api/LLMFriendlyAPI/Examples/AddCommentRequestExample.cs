using Swashbuckle.AspNetCore.Filters;
using LLMFriendlyAPI.DTOs;

namespace LLMFriendlyAPI.Examples
{
    public class AddCommentRequestExample : IExamplesProvider<AddCommentRequest>
    {
        public AddCommentRequest GetExamples()
        {
            return new AddCommentRequest
            {
                Content = "Please update this task to high priority. Client is waiting."
            };
        }
    }
}
