using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    public class EFDbContext: DbContext
    {
        public DbSet<Book> Books { get; set; }

        public EFDbContext(DbContextOptions<EFDbContext> options)
            : base(options)
        {
        }

    }
}
