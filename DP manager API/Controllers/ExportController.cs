using DP_manager_API.Data;
using DP_manager_API.Models;
using GraphQL.AspNet.Attributes;
using GraphQL.AspNet.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DP_manager_API.Controllers;

public class ExportController(AppDbContext dbContext) : GraphController
{
    [QueryRoot("export")]
    public string Export(ExportTable table, string fileName)
    {
        dbContext.Database.ExecuteSqlRaw($"COPY \"{ExportTableNames.Get(table)}\" TO '{fileName}' WITH (DELIMITER ';', FORMAT CSV, HEADER);");

        return fileName;
    }
}
