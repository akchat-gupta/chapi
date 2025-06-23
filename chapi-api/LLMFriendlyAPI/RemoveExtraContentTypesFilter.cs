using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace LLMFriendlyAPI
{
    public class RemoveExtraContentTypesFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            foreach (var content in operation.RequestBody?.Content?.ToList() ?? Enumerable.Empty<KeyValuePair<string, OpenApiMediaType>>())
            {
                if (content.Key != "application/json")
                {
                    operation.RequestBody.Content.Remove(content.Key);
                }
            }
        }
    }
}
