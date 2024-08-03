using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;
using DP_manager_API.Entities;

namespace DP_manager_API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Plant> Articles { get; set; }
    }

}
