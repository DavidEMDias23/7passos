using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SetepassosPRJ.Models
{
    public class HiScoresDbContext : DbContext
    {
        public DbSet<HiScores> Scores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connection = @"Server=(localdb)\mssqllocaldb;Database=CaptainMangiDB; Trusted_Connection=True;";
            optionsBuilder.UseSqlServer(connection);
        }

    }
}
