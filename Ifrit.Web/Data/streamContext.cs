using Ifrit.Web.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ifrit.Web.Data
{
    public partial class streamresultContext : DbContext
    {
        public virtual DbSet<IotData> iotdata { get; set; }

        public streamresultContext(DbContextOptions<streamresultContext> options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        // Unable to generate entity type for table 'dbo.results'. Please see the warning messages.
    }
}
