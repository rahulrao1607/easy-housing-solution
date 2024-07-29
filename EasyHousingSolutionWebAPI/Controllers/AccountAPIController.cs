using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EasyHousingSolutionWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Azure;

namespace EasyHousingSolutionWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountAPIController : ControllerBase
    {
        private readonly UserManager<MyIdentityUser> userManager;
        private readonly SignInManager<MyIdentityUser> loginManager;
        private readonly RoleManager<MyIdentityRole> roleManager;
        private readonly IConfiguration _configuration;

        MyIdentityDbContext context = new MyIdentityDbContext();

        EasyHousingDbContext contextEHS = new EasyHousingDbContext();

        public UserManager<MyIdentityUser> UserManager { get; private set; }

        public AccountAPIController(UserManager<MyIdentityUser> userManager,
           SignInManager<MyIdentityUser> loginManager,
           RoleManager<MyIdentityRole> roleManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.loginManager = loginManager;
            this.roleManager = roleManager;
            _configuration = configuration;

        }


        [HttpPost]
        [Route("register-Buyer")]
        public async Task<IActionResult> RegisterBuyer(RegisterViewModel obj)
        {

            var buyerExists = await userManager.FindByNameAsync(obj.UserName);
            if (buyerExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Models.Response { Status = "Error", Message = "Buyer already exists!" });

            if (ModelState.IsValid)
            {
                MyIdentityUser user = new MyIdentityUser();
                user.UserName = obj.UserName;
                user.SecurityStamp = Guid.NewGuid().ToString();
                user.Email = obj.Email;

                IdentityResult result = userManager.CreateAsync(user, obj.Password).Result;

                if (result.Succeeded)
                {
                    if (!roleManager.RoleExistsAsync("Buyer").Result)
                    {
                        MyIdentityRole role = new MyIdentityRole();
                        role.Name = "Buyer";
                        //Creating Role Here
                        IdentityResult roleResult = roleManager.CreateAsync(role).Result;
                        if (!roleResult.Succeeded)
                        {
                            ModelState.AddModelError("", "Role creation failed! Please check user details and try again.");
                            return StatusCode(500, ModelState);

                        }
                    }

                }
                //Adding User To Role
                if (roleManager.RoleExistsAsync("Buyer").Result)
                {
                    userManager.AddToRoleAsync(user, "Buyer");
                }
            }
            User userObj = new User();
            userObj.UserName = obj.UserName;
            userObj.Email = obj.Email;
            userObj.Password = obj.Password;
            userObj.UserType = "Buyer";
            contextEHS.Users.Add(userObj);
            contextEHS.SaveChanges();
            return Ok("Buyer created successfully!");
        }

        [HttpPost]
        [Route("register-Seller")]
        public async Task<IActionResult> RegisterSeller(RegisterViewModel obj)
        {
            var sellerExists = await userManager.FindByNameAsync(obj.UserName);
            if (sellerExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Models.Response { Status = "Error", Message = "Seller already exists!" });

            if (ModelState.IsValid)
            {
                MyIdentityUser user = new MyIdentityUser();
                user.UserName = obj.UserName;
                user.Email = obj.Email;


                IdentityResult result = userManager.CreateAsync(user, obj.Password).Result;

                if (result.Succeeded)
                {
                    if (!roleManager.RoleExistsAsync("Seller").Result)
                    {
                        MyIdentityRole role = new MyIdentityRole();
                        role.Name = "Seller";
                        IdentityResult roleResult = roleManager.CreateAsync(role).Result;
                        if (!roleResult.Succeeded)
                        {
                            ModelState.AddModelError("", "Role creation failed! Please check user details and try again.");
                            return StatusCode(500, ModelState);

                        }
                    }

                }
                //Adding User To Role
                if (roleManager.RoleExistsAsync("Seller").Result)
                {
                    userManager.AddToRoleAsync(user, "Seller");
                }
            }
            User userObj = new User();
            userObj.UserName = obj.UserName;
            userObj.Email = obj.Email;
            userObj.Password = obj.Password;
            userObj.UserType = "Seller";
            contextEHS.Users.Add(userObj);
            contextEHS.SaveChanges();
            return Ok("Seller created successfully!");
        }

        [HttpPost]
        [Route("register-Admin")]
        public async Task<IActionResult> RegisterAdmin(RegisterViewModel obj)
        {
            var sellerExists = await userManager.FindByNameAsync(obj.UserName);
            if (sellerExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Models.Response { Status = "Error", Message = "Admin already exists!" });

            if (ModelState.IsValid)
            {
                MyIdentityUser user = new MyIdentityUser();
                user.UserName = obj.UserName;
                user.Email = obj.Email;


                IdentityResult result = userManager.CreateAsync(user, obj.Password).Result;

                if (result.Succeeded)
                {
                    if (!roleManager.RoleExistsAsync("Admin").Result)
                    {
                        MyIdentityRole role = new MyIdentityRole();
                        role.Name = "Admin";
                        IdentityResult roleResult = roleManager.CreateAsync(role).Result;
                        if (!roleResult.Succeeded)
                        {
                            ModelState.AddModelError("", "Role creation failed! Please check user details and try again.");
                            return StatusCode(500, ModelState);

                        }
                    }

                }
                //Adding User To Role
                if (roleManager.RoleExistsAsync("Admin").Result)
                {
                    userManager.AddToRoleAsync(user, "Admin");
                }
            }
            User userObj = new User();
            userObj.UserName = obj.UserName;
            userObj.Email = obj.Email;
            userObj.Password = obj.Password;
            userObj.UserType = "Admin";
            contextEHS.Users.Add(userObj);
            contextEHS.SaveChanges();
            return Ok("Admin created successfully!");
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(LoginViewModel obj)
        {
            if (ModelState.IsValid)
            {
                var result = loginManager.PasswordSignInAsync(obj.UserName, obj.Password,false, false).Result;
                var role = GetUserRole(obj.UserName);



                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,obj.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddMinutes(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );



                if (result.Succeeded)
                {

                    //Annonymous Object
                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo,
                        username = obj.UserName,
                        Role = role.Result
                    });
                }
            }
            return Unauthorized();
        }

        [HttpGet]
        public async Task<string> GetUserRole(string userName)
        {
            var user = await userManager.FindByNameAsync(userName);
            var role = "";
            if (userManager.IsInRoleAsync(user, "Buyer").Result)
            {
                role = "Buyer";
            }
            else if (userManager.IsInRoleAsync(user, "Seller").Result)
            {
                role = "Seller";
            }
            else
            {
                role = "Admin";
            }

            return role;
        }

        [HttpPost]
        public IActionResult LogOff()
        {
            loginManager.SignOutAsync().Wait();
            return RedirectToAction("Login", "AccountAPI");
        }
    }
}
