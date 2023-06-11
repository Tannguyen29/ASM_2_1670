using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ASM_2_1670.Areas.Admin.Models;

namespace ASM_2_1670.Data
{
    public class ASM_2_1670Context : DbContext
    {
        public ASM_2_1670Context (DbContextOptions<ASM_2_1670Context> options)
            : base(options)
        {
        }

        public DbSet<ASM_2_1670.Areas.Admin.Models.User> User { get; set; } = default!;

        public DbSet<ASM_2_1670.Areas.Admin.Models.Brand> Brand { get; set; } = default!;

        public DbSet<ASM_2_1670.Areas.Admin.Models.Category> Category { get; set; } = default!;

        public DbSet<ASM_2_1670.Areas.Admin.Models.Product> Product { get; set; } = default!;
    }
}
