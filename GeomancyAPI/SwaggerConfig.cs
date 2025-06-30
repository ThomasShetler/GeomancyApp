using System.Web.Http;
using Swashbuckle.Application;
using Swashbuckle.Swagger;
using System.Web.Http.Description;
using System.Web;

[assembly: PreApplicationStartMethod(typeof(GeomancyAPI.SwaggerConfig), "Register")]

namespace GeomancyAPI
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "Geomancy API");
                    c.DescribeAllEnumsAsStrings();
                    c.IncludeXmlComments(string.Format(@"{0}\bin\GeomancyAPI.xml", System.AppDomain.CurrentDomain.BaseDirectory));
                    
                    // Add API documentation
                    c.DocumentFilter<AddRequiredHeaderParameter>();
                })
                .EnableSwaggerUi(c =>
                {
                    c.DocumentTitle("Geomancy API Documentation");
                    c.DocExpansion(DocExpansion.List);
                });
        }
    }

    public class AddRequiredHeaderParameter : IDocumentFilter
    {
        public void Apply(Swashbuckle.Swagger.SwaggerDocument swaggerDoc, SchemaRegistry schemaRegistry, IApiExplorer apiExplorer)
        {
            // Add any additional documentation here if needed
        }
    }
} 