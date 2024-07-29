using System;

using System.Security.Policy;

using System.Text;

using EasyHousingSolutionMVC.Models;

using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
namespace EasyHousingSolutionMVC.Controllers
{

    public class SellerController : Controller

    {

        // GET: SellerMVCController

        string baseURL = "https://localhost:7119/api/SellersAPI/";   //backend url


        public async Task<ActionResult> Index()

        {

            List<Seller> sellers = new List<Seller>();

            HttpClient client = new HttpClient();

            var response = await client.GetAsync(baseURL);

            string apiResponse = await response.Content.ReadAsStringAsync();

			sellers = JsonConvert.DeserializeObject<List<Seller>>(apiResponse);   //convert data from JSON to list


            return View(sellers);

        }

        // GET: Details/5
        [Route("/Details/{id}")]
        public async Task<ActionResult> Details(int id)

        {

            Seller mySellers = new Seller();

            HttpClient client = new HttpClient();

            var response = await client.GetAsync(baseURL + id);

            string apiResponse = await response.Content.ReadAsStringAsync();

            mySellers = JsonConvert.DeserializeObject<Seller>(apiResponse);

            //convert data from JSON to list


            return View(mySellers);

        }


		// GET : Seller details using Seller UserName

		public async void Details(string userName)

		{

			Seller mySellers = new Seller();

			HttpClient client = new HttpClient();

			var response = await client.GetAsync(baseURL + userName);

			string apiResponse = await response.Content.ReadAsStringAsync();

			mySellers = JsonConvert.DeserializeObject<Seller>(apiResponse);

			//convert data from JSON to list

			HttpContext.Session.SetInt32("SellerId",mySellers.SellerId);

		}


		

		// GET: Create

		public ActionResult Create()

        {

            return View();

        }

        // POST: Create

        [HttpPost]

        [ValidateAntiForgeryToken]

        public async Task<ActionResult> Create(Seller bObj)

        {

            try

            {

                if (ModelState.IsValid)

                {

                    using (var httpClient = new HttpClient())

                    {

                        StringContent content = new StringContent(JsonConvert.SerializeObject(bObj), Encoding.UTF8, "application/json");

                        using (var response = await httpClient.PostAsync(baseURL, content))

                        {

                        }

                    }

                }

                return RedirectToAction("Login","Auth");

            }

            catch

            {

                return View();

            }

        }


        public async Task<ActionResult> Edit(int id)

        {

            Seller bObj = new Seller();

            using (var httpClient = new HttpClient())

            {

                using (var response = await httpClient.GetAsync(baseURL + id.ToString()))

                {

                    string apiResponse = await response.Content.ReadAsStringAsync();

                    bObj = JsonConvert.DeserializeObject<Seller>(apiResponse);

                }

            }

            return View(bObj);

        }

        // POST: Edit/5

        [HttpPost]

        [ValidateAntiForgeryToken]

        public async Task<ActionResult> Edit(int id, Seller bObj)

        {

            var client = new HttpClient();

            client.BaseAddress = new Uri(baseURL);

            bObj.SellerId = id;

            string data = JsonConvert.SerializeObject(bObj);

            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PutAsync(client.BaseAddress + id.ToString(), content).Result;

            if (response.IsSuccessStatusCode)

            {

                return RedirectToAction("Index");

            }

            return View();

        }


        // GET: Delete/5
        public async Task<ActionResult> Delete(int id)

        {

            Seller tObj = new Seller();

            using (var httpClient = new HttpClient())

            {

                using (var response = await httpClient.GetAsync(baseURL + id.ToString()))

                {

                    string apiResponse = await response.Content.ReadAsStringAsync();

                    tObj = JsonConvert.DeserializeObject<Seller>(apiResponse);

                }

            }

            return View(tObj);

        }


        // POST: SellerMVCController/Delete/5

        [HttpPost]

        [ValidateAntiForgeryToken]

        public async Task<ActionResult> Delete(int id, Seller tObj)

        {

            using (var httpClient = new HttpClient())

            {

                using (var response = await httpClient.DeleteAsync(baseURL + id))

                {

                    string apiResponse = await response.Content.ReadAsStringAsync();

                }

            }

            return RedirectToAction("Index");

        }


    }

}