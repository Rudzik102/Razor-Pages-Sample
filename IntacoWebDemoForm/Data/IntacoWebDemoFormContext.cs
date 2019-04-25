using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace IntacoWebDemoForm.Models
{
    public class IntacoWebDemoFormContext : DbContext
    {
        public IntacoWebDemoFormContext (DbContextOptions<IntacoWebDemoFormContext> options)
            : base(options)
        {
        }

        public DbSet<IntacoWebDemoForm.Models.Person> Person { get; set; }
    }
}
