using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace EasyHousingSolutionWebAPI.Models
{
    public class MyIdentityRole : IdentityRole
    {
        public const string Admin = "Admin";
        public const string Seller = "Seller";
        public const string Buyer = "Buyer";
    }
}
