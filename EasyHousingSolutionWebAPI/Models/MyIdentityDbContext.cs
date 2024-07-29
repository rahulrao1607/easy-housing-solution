using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyHousingSolutionWebAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EasyHousingSolutionWebAPI.Models
{
    public class MyIdentityDbContext : IdentityDbContext<MyIdentityUser, MyIdentityRole, string>
    {
        public MyIdentityDbContext()
        {

        }
        public MyIdentityDbContext(DbContextOptions<MyIdentityDbContext> options) : base(options)
        {
            //nothing here
        }

    }
}
