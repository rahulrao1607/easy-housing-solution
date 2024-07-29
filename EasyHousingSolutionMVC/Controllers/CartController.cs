using System.Diagnostics.CodeAnalysis;
using System.Security.Policy;
using System.Text;
using EasyHousingSolutionMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;

namespace EasyHousingSolutionMVC.Controllers
{
    public class CartController : Controller
    {

        Uri baseAddress = new Uri("https://localhost:7119/api");
        private readonly HttpClient _httpClient;

        public CartController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAddress;
        }


        // GET: CartController
        [HttpGet]
        public ActionResult Index(int id)
        {
			var buyerUserId = HttpContext.Session.GetInt32("buyerUserId");
			List<CartPropertyDTO> cartList = new List<CartPropertyDTO>(); // creating list to store data

			HttpResponseMessage response = _httpClient.GetAsync("https://localhost:7119/api/BuyersAPI/GetCart/" + buyerUserId).Result;  // getting Http response

			ViewBag.Message = "Buyer";

			if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                cartList = JsonConvert.DeserializeObject<List<CartPropertyDTO>>(data);
                //Converting JSON to List
            }
            return View(cartList);


        }

        // GET: CartController/Details/5
        public ActionResult Details(int id)
        {
            Cart cObj = new Cart();
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/CartsAPI/ShowDetails?cartId=" + id).Result;  // getting Http response

            if (response.IsSuccessStatusCode)
            {
				ViewBag.Message = "Buyer";
				string data = response.Content.ReadAsStringAsync().Result;
                cObj = JsonConvert.DeserializeObject<Cart>(data);
                //Converting JSON to List
            }
            return View(cObj);

        }

        // GET: CartController/Create
        public ActionResult Create()
        {
            return View();
        }


        // POST: CartController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int propId, int buyerId)
        {
            try
            {
                Cart cObj = new Cart { PropertyId = propId, BuyerId = buyerId };
                string data = JsonConvert.SerializeObject(cObj);  // serializing the object to Json

                StringContent content = new StringContent(data, Encoding.UTF8, "application/json"); // Passing the data in the form JSON //string content

                HttpResponseMessage response = _httpClient.PostAsync(_httpClient.BaseAddress + "/CartsAPI/PostCart?propId=" + propId + "&buyerId=" + buyerId, null).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Property Added.";
                    return RedirectToAction(nameof(Index));// return this to Index field
                }
                else
                {
                    throw new Exception();
                }

            }// hadeling any unhandled exception
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message; // Displaying the Exception message

                return View();
            }
            return View();
        }

        // GET: CartController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CartController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CartController/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                Cart mObj = new Cart();

                HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress +
                    "/CartsAPI/ShowDetails?cartId=" + id).Result;

                if (response.IsSuccessStatusCode)
                {
					ViewBag.Message = "Buyer";
					string data = response.Content.ReadAsStringAsync().Result;
                    mObj = JsonConvert.DeserializeObject<Cart>(data);


                }
                return View(mObj);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message.Split('\n');
                return View();
            }

        }

        // POST: CartController/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                HttpResponseMessage response = _httpClient.DeleteAsync("https://localhost:7119/api/CartsAPI/DeleteProperty/" + id.ToString()).Result;

                if (response.IsSuccessStatusCode)
                {
					ViewBag.Message = "Buyer";
					TempData["successMessage"] = "Property Deleted";
                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message.Split('\n');
                return View();
            }


        }
    }
}
