using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using EasyHousingSolutionMVC.Models;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;

namespace EasyHousingSolutionMVC.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        ///<summary>
        ///This is A View Logic Which Returns Login Page
        ///</summary>
        public IActionResult Login()
        {
            //Checking If User is Already Logged In
            //If Customer is Logged in, Go To Profile Page
            if (HttpContext.Session.GetString("UserName") != null && HttpContext.Session.GetString("Role") == "Buyer")
            {
                return RedirectToAction("Index", "Property");
            }
            //If Seller is Logged in, Go To Seller Page
            else if (HttpContext.Session.GetString("UserName") != null && HttpContext.Session.GetString("Role") == "Seller")
            {
                return RedirectToAction("Index", "Seller");
            }
            //If Admin is Logged in, Go To Admin Page
            else if (HttpContext.Session.GetString("UserName") != null && HttpContext.Session.GetString("Role") == "Admin")
            {
                return RedirectToAction("Index", "Admin");
            }
            //Else Show Login Page
            else
            {
                return View();
            }
        }

        ///<summary>
        ///This is an Action Result Method <c>Login</c>.
        ///It Takes Login Model as Input From UI
        ///If LoggedIn, It Creates Role and CustomerId Sessions
        ///</summary>
        [HttpPost]
        public ActionResult Login(Login model)
        {
            //Checking the state of model passed as parameter.
            if (ModelState.IsValid)
            {

                //Validating the user, whether the user is valid or not.
                var isValidUser = IsValidUser(model).Result;

                //If user is valid & present in database, we are redirecting it to Welcome page.
                if (isValidUser != null)
                {
                    HttpContext.Session.SetString("UserName", isValidUser.UserName);
                    HttpContext.Session.SetString("FullName", isValidUser.Token);
                    
                    HttpContext.Session.SetString("Role", isValidUser.Role);
                    
                    ViewBag.Message = isValidUser.Role;

					return HttpContext.Session.GetString("Role") switch
                    {
                        "Buyer" => RedirectToAction("Index", "Property"),
                        "Seller" => RedirectToAction("GetPropertySellerId", "Property"), //  condition for Seller role
                        "Admin" => RedirectToAction("Index", "Admin"), //  condition for Admin role
                        _ => RedirectToAction("Login", "Auth") // Default case
                    };

                }
                else
                {
                    //If the username and password combination is not present in DB then error message is shown.
                    ModelState.AddModelError(string.Empty, "Wrong Username and password combination !");
                    return View();
                }
            }
            else
            {
                //If model state is not valid, the model with error message is returned to the View.
                return View(model);
            }
        }

        ///<summary>
        ///This is an Async Method <c>IsValidUser</c>.
        ///It Takes Login Model as Input and Returns Object of Profile.
        ///This Method Makes API Request to Post Login Credentials and Get Customer Details.
        ///It Returns Null If Status Code of API Request is Not Successful.
        ///<param name="model">Login</param>
        ///</summary>
        public async Task<LoginGet> IsValidUser(Login model)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7119/api/");
                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string tmp = JsonConvert.SerializeObject(model).ToString();
                var content = new StringContent(tmp, Encoding.UTF8, "application/json");
                HttpResponseMessage Res = await client.PostAsync("AccountAPI/login", content);
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var apiResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list
                    var resValue = JsonConvert.DeserializeObject<LoginGet>(apiResponse);
                    HttpContext.Session.SetString("Token", resValue.Token);
                    return resValue;
                }
                else
                    return null;
            }
        }

        ///<summary>
        ///This is A View Logic Which Returns Buyer Signup Page
        ///</summary>
        public IActionResult SignupBuyer()
        {
            //Checking If User is Already Logged In
            //If Customer is Logged in, Go To Profile Page
            if (HttpContext.Session.GetString("UserName") != null && HttpContext.Session.GetString("Role") == "Buyer")
            {
                return RedirectToAction("Index", "Buyer");
            }
            else
            {
                return View();
            }
        }


        ///<summary>
        ///This is A View Logic Which Returns Seller Signup Page
        ///</summary>
        public IActionResult SignupSeller()
        {
            //Checking If User is Already Logged In
            //If Seller is Logged in, Go To Admin Page
            if (HttpContext.Session.GetString("UserName") != null && HttpContext.Session.GetString("Role") == "Seller")
            {
                return RedirectToAction("Index", "Seller");
            }
            else
            {
                return View();
            }
        }

        ///<summary>
        ///This is an Action Result Method <c>Signup</c>.
        ///It Takes Signup Model as Input From UI
        ///If Created, It Shows Login Page
        ///</summary>
        [HttpPost]
        public IActionResult SignupBuyer(SignUp model)
        {
            // model.Role = "Buyer";          

            if (ModelState.IsValid)
            {

                //Validating the user, whether the user is valid or not.
                var hasValidData = HasValidDataForBuyer(model).Result;

                //If user is valid & present in database, we are redirecting it to Welcome page.
                if (hasValidData != null)
                {
                    //return View("Login");
                    //return RedirectToAction("Login", "Auth");
                    HttpContext.Session.SetString("BuyerUserName", model.UserName);
                    HttpContext.Session.SetString("BuyerEmail", model.Email);
                    return RedirectToAction("Create", "Buyer");
				}
                else
                {
                    //If User Already Exist in DB then error message is shown.
                    ModelState.AddModelError(string.Empty, "Email Already Present in Database !");
                    return View();
                }
            }
            else
            {
                //If model state is not valid, the model with error message is returned to the View.
                return View(model);
            }
        }

        ///<summary>
        ///This is an Action Result Method <c>Signup</c>.
        ///It Takes Signup Model as Input From UI
        ///If Created, It Shows Login Page
        ///</summary>
        [HttpPost]
        public IActionResult SignupSeller(SignUp model)
        {
            // model.Role = "Seller";          

            if (ModelState.IsValid)
            {

                //Validating the user, whether the user is valid or not.
                var hasValidData = HasValidDataForSeller(model).Result;

                //If user is valid & present in database, we are redirecting it to Welcome page.
                if (hasValidData != null)
                {
					//return View("Login");

					return RedirectToAction("Create", "Seller");
				}
                else
                {
                    //If User Already Exist in DB then error message is shown.
                    ModelState.AddModelError(string.Empty, "Email Already Present in Database !");
                    return View();
                }
            }
            else
            {
                //If model state is not valid, the model with error message is returned to the View.
                return View(model);
            }
        }

        ///<summary>
        ///This is an Async Method <c>HadValidData</c>.
        ///It Takes Signup Model as Input and Returns Object of Profile.
        ///This Method Makes API Request to Post Signup Credentials and Get Customer Details.
        ///It Returns Null If Status Code of API Request is Not Successful.
        ///<param name="model">Signup</param>
        ///</summary>
        public async Task<string> HasValidDataForBuyer(SignUp model)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7119/api/");
                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string tmp = JsonConvert.SerializeObject(model).ToString();
                var content = new StringContent(tmp, Encoding.UTF8, "application/json");
                HttpResponseMessage Res = await client.PostAsync("AccountAPI/register-Buyer", content);
                if (Res.IsSuccessStatusCode)
                {
                    
                    //Storing the response details recieved from web api
                    var apiResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list
                    string resp = JsonConvert.DeserializeObject<string>(apiResponse);
                    return resp;
                }
                else
                    return null;
            }
        }

        ///<summary>
        ///This is an Async Method <c>HadValidData</c>.
        ///It Takes Signup Model as Input and Returns Object of Profile.
        ///This Method Makes API Request to Post Signup Credentials and Get Customer Details.
        ///It Returns Null If Status Code of API Request is Not Successful.
        ///<param name="model">Signup</param>
        ///</summary>
        public async Task<string> HasValidDataForSeller(SignUp model)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7119/api/");
                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string tmp = JsonConvert.SerializeObject(model).ToString();
                var content = new StringContent(tmp, Encoding.UTF8, "application/json");
                HttpResponseMessage Res = await client.PostAsync("AccountAPI/register-Seller", content);
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var apiResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list
                    string resp = JsonConvert.DeserializeObject<string>(apiResponse);
                    return resp;
                }
                else
                    return null;
            }
        }

        ///<summary>
        ///This is Logout Logic
        ///It Clears All Value from Session
        ///</summary>
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return View("Login");
        }
    }
}
