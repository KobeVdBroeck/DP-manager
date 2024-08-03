using DP_manager_API.Configuration;
using DP_manager_API.Data;
using GraphQL.AspNet.Configuration;

var builder = WebApplication.CreateBuilder(args);

DotEnv.Load(".env");

if (!DotEnv.HasVariable("CONNECTION"))
    throw new NullReferenceException("No connection string found.");

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddGraphQL();
builder.Services.AddPostgresDatabase(Environment.GetEnvironmentVariable("CONNECTION"));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.UseGraphQL();
app.MapControllers();

if (args.Contains("import"))
    XlsxImport.ImportCurrentStock("");

app.Run();
