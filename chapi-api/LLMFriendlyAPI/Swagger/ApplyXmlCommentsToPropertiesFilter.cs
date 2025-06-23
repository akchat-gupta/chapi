using System.Xml.XPath;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace LLMFriendlyAPI.Swagger
{
    public class ApplyXmlCommentsToPropertiesFilter : ISchemaFilter
    {
        private readonly XPathNavigator _navigator;

        public ApplyXmlCommentsToPropertiesFilter(string xmlPath)
        {
            var xmlDoc = new XPathDocument(xmlPath);
            _navigator = xmlDoc.CreateNavigator();
        }

        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (schema?.Properties == null || context?.Type == null)
                return;

            foreach (var property in context.Type.GetProperties())
            {
                var memberName = $"P:{context.Type.FullName}.{property.Name}";
                var node = _navigator.SelectSingleNode($"/doc/members/member[@name='{memberName}']/summary");
                if (node != null && schema.Properties.ContainsKey(ToCamelCase(property.Name)))
                {
                    var description = CleanSummary(node.InnerXml);
                    schema.Properties[ToCamelCase(property.Name)].Description = description;
                }
            }
        }

        private static string ToCamelCase(string name)
        {
            if (string.IsNullOrEmpty(name) || !char.IsUpper(name[0]))
                return name;

            return char.ToLowerInvariant(name[0]) + name[1..];
        }

        private static string CleanSummary(string xml)
        {
            return xml?.Trim()
                      .Replace("\r", "")
                      .Replace("\n", "")
                      .Replace("  ", " ");
        }
    }

}
