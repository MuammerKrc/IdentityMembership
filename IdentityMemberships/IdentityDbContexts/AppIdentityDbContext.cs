using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityStructureModel.IdentityModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityStructureModel.IdentityDbContexts
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser, AppRole, long>
    {
        public AppIdentityDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
