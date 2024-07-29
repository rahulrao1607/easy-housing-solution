using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EasyHousingSolutionMVC.Models;
using Newtonsoft.Json;
using System.Numerics;
using System.Text;
using System.Reflection;
namespace EasyHousingSolutionMVC.Controllers
{
    public class BuyerController : Controller
    {
        string url = "https://localhost:7119/api/BuyersAPI/";
        // GET: BuyerController
        public async Task<ActionResult> Index()
        {
            List<Buyer> buyList = new List<Buyer>();
            HttpClient client = new HttpClient();

            var response = await client.GetAsync(url);
            string apiResponse = await response.Content.ReadAsStringAsync();

            //convert data from JSON to list 
            buyList = JsonConvert.DeserializeObject<List<Buyer>>(apiResponse);
            return View(buyList);
        }


        // GET: BuyerController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Buyer myBuyer = new Buyer();
            HttpClient client = new HttpClient();

            var response = await client.GetAsync(url + "GetDetails/" + id);
            string apiResponse = await response.Content.ReadAsStringAsync();

            myBuyer = JsonConvert.DeserializeObject<Buyer>(apiResponse);


            return View(myBuyer);

        }

        // GET: BuyerController/Create
        public ActionResult Create()
        {
            ViewBag.BuyerUserName = HttpContext.Session.GetString("BuyerUserName");
            ViewBag.BuyerEmail = HttpContext.Session.GetString("BuyerEmail");
            HttpContext.Session.Remove("BuyerUserName");
            HttpContext.Session.Remove("BuyerEmail");

            return View();
        }

        // POST: BuyerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Buyer bObj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var httpClient = new HttpClient())
                    {
                        StringContent content = new StringContent(JsonConvert.SerializeObject(bObj), Encoding.UTF8, "application/json");
                        using (var response = await httpClient.PostAsync(url, content))
                        {

                        }
                    }
                }

                return RedirectToAction("Login","Auth"); //// Change heree 
            }
            catch
            {
                return View();
            }
        }

        // GET: BuyerController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Buyer bObj = new Buyer();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(url + id.ToString()))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    bObj = JsonConvert.DeserializeObject<Buyer>(apiResponse);
                }
            }

            return View(bObj);
        }

        // POST: BuyerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Buyer bObj)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(url);
            bObj.BuyerId = id;
            string data = JsonConvert.SerializeObject(bObj);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync(client.BaseAddress + id.ToString(), content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: BuyerController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Buyer bObj = new Buyer();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync(url + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    bObj = JsonConvert.DeserializeObject<Buyer>(apiResponse);
                }
            }

			return RedirectToAction("Index");
		}

        // POST: BuyerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync(url + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<ActionResult> AddCart(int propId, int buyerId)
        {

			Buyer myBuyer = new Buyer();

			HttpClient client = new HttpClient();

			var userName = HttpContext.Session.GetString("UserName");

			var responsee = await client.GetAsync(url + "GetBuyerByUserName/" + userName);

			string apiResponse = await responsee.Content.ReadAsStringAsync();

			myBuyer = JsonConvert.DeserializeObject<Buyer>(apiResponse);

			HttpContext.Session.SetInt32("buyerUserId",myBuyer.BuyerId);

			buyerId = myBuyer.BuyerId;
			using (client)
            {
                var cartItem = new Cart { PropertyId = propId, BuyerId = buyerId };
                StringContent content = new StringContent(JsonConvert.SerializeObject(cartItem), Encoding.UTF8, "application/json");

                using (var response = client.PostAsync("https://localhost:7119/api/BuyersAPI/AddToCart/" + propId.ToString() + '/' + buyerId.ToString(), content))

                {
                    response.Wait();

                    var result = response.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "Buyer";
                        //Redirect to view cart action to display the updated cart
                        return RedirectToAction("Index", "Cart", new { id = buyerId });
                        //<a asp-controller="Product" asp-action="GetProductsByCategoryFromAPI" asp-route-id="@item.CategoryId">show Product</a>
                    }
                }

                return View("Error");

            }
        }

       

    }

}