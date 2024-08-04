using DP_manager_API.Configuration;
using DP_manager_API.Data;
using GraphQL.AspNet.Configuration;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

DotEnv.Load(".env");

if (!DotEnv.HasVariable("CONNECTION"))
    throw new NullReferenceException("No connection string found.");

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddGraphQL();
builder.Services.AddPostgresDatabase(DotEnv.GetVariable("CONNECTION"));

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseGraphQL();
app.MapControllers();

if (DotEnv.HasVariable("IMPORT"))
    using (var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        new XlsxImport(dbInitializer).ImportStock(DotEnv.GetVariable("IMPORT"), DotEnv.GetVariable("IMPORTTARGET"));
    }

app.Run();
