using System.ComponentModel;
using System.Reflection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebAPI.Attributes;

public class DefaultValueSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        foreach (var prop in context.Type.GetProperties())
        {
            var defaultValueAttr = prop.GetCustomAttribute<DefaultValueAttribute>();
            if (defaultValueAttr != null && schema.Properties.ContainsKey(prop.Name))
            {
                schema.Properties[prop.Name].Default = new OpenApiString(defaultValueAttr.Value.ToString());
            }
        }
    }
}