var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.GeomancyWebUI>("geomancywebui");

builder.Build().Run();
