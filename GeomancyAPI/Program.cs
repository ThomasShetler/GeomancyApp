using System;
using System.Web.Http;
using System.Web.Http.SelfHost;
using Swashbuckle.Application;

namespace GeomancyAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            // Use higher port numbers that don't require admin privileges
            string[] ports = { "9000", "9001", "9002", "9003", "9004" };
            string selectedPort = null;
            HttpSelfHostServer server = null;

            foreach (string port in ports)
            {
                try
                {
                    var config = new HttpSelfHostConfiguration($"http://localhost:{port}");

                    // Configure Web API routes
                    config.MapHttpAttributeRoutes();

                    config.Routes.MapHttpRoute(
                        name: "DefaultApi",
                        routeTemplate: "api/{controller}/{id}",
                        defaults: new { id = RouteParameter.Optional }
                    );

                    // Remove XML formatter to ensure JSON is used by default
                    config.Formatters.Remove(config.Formatters.XmlFormatter);

                    // Configure JSON serialization
                    var jsonFormatter = config.Formatters.JsonFormatter;
                    if (jsonFormatter != null)
                    {
                        jsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                        jsonFormatter.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                    }

                    // Configure Swagger
                    config.EnableSwagger(c =>
                    {
                        c.SingleApiVersion("v1", "Geomancy API");
                        c.DescribeAllEnumsAsStrings();
                    })
                    .EnableSwaggerUi(c =>
                    {
                        c.DocumentTitle("Geomancy API Documentation");
                        c.DocExpansion(DocExpansion.List);
                    });

                    server = new HttpSelfHostServer(config);
                    server.OpenAsync().Wait();
                    selectedPort = port;
                    Console.WriteLine($"Successfully started on port {port}");
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to start on port {port}: {ex.Message}");
                    if (server != null)
                    {
                        server.Dispose();
                        server = null;
                    }
                }
            }

            if (server != null && selectedPort != null)
            {
                Console.WriteLine();
                Console.WriteLine("=== Geomancy API is Running ===");
                Console.WriteLine($"API Base URL: http://localhost:{selectedPort}");
                Console.WriteLine($"Swagger UI: http://localhost:{selectedPort}/swagger");
                Console.WriteLine($"Test Endpoint: http://localhost:{selectedPort}/api/geomancy/test");
                Console.WriteLine();
                Console.WriteLine("Press any key to stop the server and exit...");
                Console.ReadKey();
                server.CloseAsync().Wait();
                server.Dispose();
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Failed to start the API server on any available port.");
                Console.WriteLine("Possible solutions:");
                Console.WriteLine("1. Try running as Administrator");
                Console.WriteLine("2. Check if another application is using the ports");
                Console.WriteLine("3. Try manually specifying a port in the code");
                Console.WriteLine();
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
} 