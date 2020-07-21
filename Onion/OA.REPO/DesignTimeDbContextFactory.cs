using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OA.REPO
{
    public class DesignTimeDbContextFactory  : IDesignTimeDbContextFactory<ApplicationDBContext>
    {
        public ApplicationDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDBContext>();
            optionsBuilder.UseSqlServer("Data Source=.;Database=EscrowData;Trusted_Connection=True;MultipleActiveResultSets=true");

            return new ApplicationDBContext(optionsBuilder.Options);
        }
    }
}
