using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Codelisk.GeneratorAttributes.WebAttributes.Database;
using Microsoft.EntityFrameworkCore;

namespace Framework.Restservice.Database
{
    [BaseContext]
    public partial class BaseDbContext : IdentityDbContext<UserDto, IdentityRole<Guid>, Guid>
    {
        public BaseDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
