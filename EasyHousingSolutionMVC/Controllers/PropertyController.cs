using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EasyHousingSolutionMVC.Models;
using Newtonsoft.Json;
using System.Numerics;
using System.Linq;
using System.Net.Http;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;


namespace EasyHousingSolutionMVC.Controllers
{
    public class PropertyController : Controller
    {
        string url = "https://localhost:7119/api/PropertiesAPI/";

        // GET: PropertyController

        public async Task<ActionResult> Index()

        {

            List<Property> propList = new List<Property>();

            HttpClient client = new HttpClient();

            var response = await client.GetAsync(url);

            string apiResponse = await response.Content.ReadAsStringAsync();

            //convert data from JSON to list

            propList = JsonConvert.DeserializeObject<List<Property>>(apiResponse);

            ViewBag.Message = HttpContext.Session.GetString("Role");

            return View(propList);

        }

        // GET: Properties for Admin
		public async Task<ActionResult> AdminProperty()

		{

			List<Property> propList = new List<Property>();

			HttpClient client = new HttpClient();

			var response = await client.GetAsync(url);

			string apiResponse = await response.Content.ReadAsStringAsync();

			//convert data from JSON to list

			propList = JsonConvert.DeserializeObject<List<Property>>(apiResponse);

			return View(propList);

		}

        // Admin Property Details
		public async Task<ActionResult> PropertyDetail(int id)

		{

			Property myProperty = new Property();

			HttpClient client = new HttpClient();

			var response = await client.GetAsync(url + id);

			string apiResponse = await response.Content.ReadAsStringAsync();

			myProperty = JsonConvert.DeserializeObject<Property>(apiResponse);

			ViewBag.Message = "Admin";

			return View(myProperty);

		}

        public async Task<ActionResult> GetPropertiesByType(string propertyType)

        {

            try

            {

                using (var client = new HttpClient())

                {

                    // Assuming the search API endpoint is different from the details endpoint

                    var response = await client.GetAsync($"{url}BuyOption?BuyOption={propertyType}");

                    response.EnsureSuccessStatusCode(); // Ensure successful response, otherwise, an exception will be thrown.

                    string apiResponse = await response.Content.ReadAsStringAsync();

                    // Assuming the API response is a list of PropertyViewModel objects

                    var searchResults = JsonConvert.DeserializeObject<List<Property>>(apiResponse);

                    return View("Index", searchResults); // Pass searchResults to the Search view

                }

            }

            catch (HttpRequestException ex)

            {

                // Log or handle the exception

                return StatusCode(500, $"Error retrieving data: {ex.Message}");

            }

            catch (JsonException ex)

            {

                // Log or handle the exception

                return StatusCode(500, $"Error deserializing JSON: {ex.Message}");

            }

        }


        // GET: PropertyController/Details/5

        //
        public async Task<ActionResult> Search(string propertyName)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    // Encode the property name for use in the URL
                    string encodedPropertyName = Uri.EscapeDataString(propertyName);

                    // Assuming the search API endpoint is different from the details endpoint
                    var response = await client.GetAsync(url + "?propertyName=" + encodedPropertyName);

                    response.EnsureSuccessStatusCode(); // Ensure successful response, otherwise, an exception will be thrown.

                    string apiResponse = await response.Content.ReadAsStringAsync();

                    // Assuming the API response is a list of Property objects
                    var searchResults = JsonConvert.DeserializeObject<List<Property>>(apiResponse);

                    return View(searchResults);
                }
            }
            catch (HttpRequestException ex)
            {
                // Log or handle the exception
                return StatusCode(500, $"Error retrieving data: {ex.Message}");
            }
            catch (JsonException ex)
            {
                // Log or handle the exception
                return StatusCode(500, $"Error deserializing JSON: {ex.Message}");
            }
        }

        //-------------------------------bysellerid and isactive
        [Route("/SellerActiveProperty")]
        public async Task<ActionResult> GetPropertySellerIdIsActive(bool isActive)
        {
            try
            {
                using (var client = new HttpClient())
                {
					var selId = HttpContext.Session.GetInt32("SellerId");
					// Assuming the search API endpoint is different from the details endpoint
					var response = await client.GetAsync($"{url}?sellerId={selId}");

                    response.EnsureSuccessStatusCode(); // Ensure successful response, otherwise, an exception will be thrown.

                    string apiResponse = await response.Content.ReadAsStringAsync();

                    // Assuming the API response is a list of Property objects
                    var allProperties = JsonConvert.DeserializeObject<List<Property>>(apiResponse);

                    // Filter properties based on isActive status
                    var filteredProperties = allProperties.Where(p => p.IsActive == isActive).ToList();
                    ViewBag.Message = "Seller";

					return View("Index", filteredProperties); // Pass filteredProperties to the Search view
                }
            }
            catch (HttpRequestException ex)
            {
                // Log or handle the exception
                return StatusCode(500, $"Error retrieving data: {ex.Message}");
            }
            catch (JsonException ex)
            {
                // Log or handle the exception
                return StatusCode(500, $"Error deserializing JSON: {ex.Message}");
            }
        }

        //------------------------------ isactive ----------------------

        public async Task<ActionResult> GetActiveProperties(bool isActive)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    // Assuming the search API endpoint is different from the details endpoint
                    var response = await client.GetAsync(url + "IsActive?IsActive=" + isActive);

                    response.EnsureSuccessStatusCode(); // Ensure successful response, otherwise, an exception will be thrown.

                    string apiResponse = await response.Content.ReadAsStringAsync();

                    // Assuming the API response is a list of Property objects
                    var searchResults = JsonConvert.DeserializeObject<List<Property>>(apiResponse);

                    return View("AdminProperty", searchResults); // Pass searchResults to the Search view
                }
            }
            catch (HttpRequestException ex)
            {
                // Log or handle the exception
                return StatusCode(500, $"Error retrieving data: {ex.Message}");
            }
            catch (JsonException ex)
            {
                // Log or handle the exception
                return StatusCode(500, $"Error deserializing JSON: {ex.Message}");
            }
        }

        //------------------------------------------

        [Route("/HighToLow")]
        public async Task<ActionResult> GetPropertyByPriceHighToLow()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    // Assuming the search API endpoint is different from the details endpoint
                    var response = await client.GetAsync("https://localhost:7119/api/PropertiesAPI/PriceHighToLow");

                    response.EnsureSuccessStatusCode(); // Ensure successful response, otherwise, an exception will be thrown.

                    string apiResponse = await response.Content.ReadAsStringAsync();

                    // Assuming the API response is a list of Property objects
                    var searchResults = JsonConvert.DeserializeObject<List<Property>>(apiResponse);

                    return View("Index", searchResults); // Pass searchResults to the Search view
                }
            }
            catch (HttpRequestException ex)
            {
                // Log or handle the exception
                return StatusCode(500, $"Error retrieving data: {ex.Message}");
            }
            catch (JsonException ex)
            {
                // Log or handle the exception
                return StatusCode(500, $"Error deserializing JSON: {ex.Message}");
            }
        }

        //-------------------------------by high to low
        [Route("/LowToHigh")]
        public async Task<ActionResult> GetPropertyByPriceLowToHigh()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    // Assuming the search API endpoint is different from the details endpoint
                    var response = await client.GetAsync("https://localhost:7119/api/PropertiesAPI/PriceLowToHigh");

                    response.EnsureSuccessStatusCode(); // Ensure successful response, otherwise, an exception will be thrown.

                    string apiResponse = await response.Content.ReadAsStringAsync();

                    // Assuming the API response is a list of Property objects
                    var searchResults = JsonConvert.DeserializeObject<List<Property>>(apiResponse);

                    return View("Index", searchResults); // Pass searchResults to the Search view
                }
            }
            catch (HttpRequestException ex)
            {
                // Log or handle the exception
                return StatusCode(500, $"Error retrieving data: {ex.Message}");
            }
            catch (JsonException ex)
            {
                // Log or handle the exception
                return StatusCode(500, $"Error deserializing JSON: {ex.Message}");
            }
        }


        //---------------------------------------------------------------------------------------------
        // GET: Admins/VerifyProperty/5
        public async Task<ActionResult> VerifyProperty(int id)
        {
            Property property = new Property();

            HttpClient client = new HttpClient();

            var response = await client.GetAsync(url + id);

            string apiResponse = await response.Content.ReadAsStringAsync();

            property = JsonConvert.DeserializeObject<Property>(apiResponse);


            return View(property);
        }


        public async Task<ActionResult> Details(int id)

        {

            Property myProperty = new Property();

            HttpClient client = new HttpClient();

            var response = await client.GetAsync(url + id);

            string apiResponse = await response.Content.ReadAsStringAsync();

            myProperty = JsonConvert.DeserializeObject<Property>(apiResponse);

			ViewBag.Message = "Property";

			return View(myProperty);

        }

        [Route("/SellerDetails")]
        public async Task<ActionResult> SellerDetails(int id)

        {

            Property myProperty = new Property();

            HttpClient client = new HttpClient();

            var response = await client.GetAsync(url + id);

            string apiResponse = await response.Content.ReadAsStringAsync();

            myProperty = JsonConvert.DeserializeObject<Property>(apiResponse);

			ViewBag.Message = "Seller";

			return View(myProperty);

        }
        // GET: PropertyController/Create

        public ActionResult Create()

        {

            return View();

        }

        // POST: PropertyController/Create

        [HttpPost]

        [ValidateAntiForgeryToken]

        public async Task<ActionResult> Create(Property bObj)

        {

			Seller mySellers = new Seller();

			HttpClient client = new HttpClient();

			var userName = HttpContext.Session.GetString("UserName");

			var response = await client.GetAsync("https://localhost:7119/api/SellersAPI/GetSellerByUserName/" + userName);

			string apiResponse = await response.Content.ReadAsStringAsync();

			mySellers = JsonConvert.DeserializeObject<Seller>(apiResponse);

			//convert data from JSON to list

			HttpContext.Session.SetInt32("SellerId", mySellers.SellerId);

			try

            {

                if (ModelState.IsValid)

                {

                    using (var httpClient = new HttpClient())

                    {
                        var id = HttpContext.Session.GetInt32("SellerId");
                        bObj.SellerId = (int)id;
                        StringContent content = new StringContent(JsonConvert.SerializeObject(bObj), Encoding.UTF8, "application/json");

                        using (var respons = await httpClient.PostAsync(url, content))

                        {

                        }

                    }

                }

                return RedirectToAction("GetPropertySellerId", "Property");

            }

            catch

            {

                return View();

            }

        }

        // GET: PropertyController/Edit/5

        public async Task<ActionResult> Edit(int id)

        {

            Property bObj = new Property();

            using (var httpClient = new HttpClient())

            {

                using (var response = await httpClient.GetAsync(url + id.ToString()))

                {

                    string apiResponse = await response.Content.ReadAsStringAsync();

                    bObj = JsonConvert.DeserializeObject<Property>(apiResponse);

                }

            }

            return View(bObj);

        }

        // POST: PropertyController/Edit/5

        [HttpPost]

        [ValidateAntiForgeryToken]

        public async Task<ActionResult> Edit(int id, Property bObj)

        {

            var client = new HttpClient();

            client.BaseAddress = new Uri(url);

            bObj.PropertyId = id;

            string data = JsonConvert.SerializeObject(bObj);

            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PutAsync(client.BaseAddress + id.ToString(), content).Result;

            if (response.IsSuccessStatusCode)

            {

                return RedirectToAction("Index");

            }

            return View();

        }


        public async Task<ActionResult> SellerPropertyEdit(int id)

        {

            Property bObj = new Property();

            using (var httpClient = new HttpClient())

            {

                using (var response = await httpClient.GetAsync(url + id.ToString()))

                {

                    string apiResponse = await response.Content.ReadAsStringAsync();

                    bObj = JsonConvert.DeserializeObject<Property>(apiResponse);

                }

            }

            return View(bObj);

        }

        // POST: PropertyController/Edit/5

        [HttpPost]

        [ValidateAntiForgeryToken]

        public async Task<ActionResult> SellerPropertyEdit(int id, Property bObj)

        {

            var client = new HttpClient();

            client.BaseAddress = new Uri(url);

            bObj.PropertyId = id;

            string data = JsonConvert.SerializeObject(bObj);

            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PutAsync(client.BaseAddress + id.ToString(), content).Result;

			ViewBag.Message = "Seller";

			if (response.IsSuccessStatusCode)

            {

                return RedirectToAction("GetPropertySellerId", "Property");

            }

            return View();

        }

        public async Task<ActionResult> EditStatus(int id)

        {

            Property bObj = new Property();

            using (var httpClient = new HttpClient())

            {

                using (var response = await httpClient.GetAsync(url + id.ToString()))

                {

                    string apiResponse = await response.Content.ReadAsStringAsync();

                    bObj = JsonConvert.DeserializeObject<Property>(apiResponse);

                }

            }

            return View(bObj);

        }

        // POST: PropertyController/Edit/5

        [HttpPost]
       // [Route("/UpdateStatus")]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> EditStatus(int id, Property bObj)

        {

            var client = new HttpClient();

            client.BaseAddress = new Uri(url);

            bObj.PropertyId = id;

            string data = JsonConvert.SerializeObject(bObj);

            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PutAsync(client.BaseAddress + id.ToString(), content).Result;

            if (response.IsSuccessStatusCode)

            {

                return RedirectToAction("AdminProperty");

            }

            return View();

        }

        // GET: PropertyController/Delete/5

        public async Task<ActionResult> Delete(int id)

        {

            Property bObj = new Property();

            using (var httpClient = new HttpClient())

            {

                using (var response = await httpClient.GetAsync(url + id.ToString()))

                {

                    string apiResponse = await response.Content.ReadAsStringAsync();

                    bObj = JsonConvert.DeserializeObject<Property>(apiResponse);

                }

            }

            return View(bObj);

        }

        // POST: PropertyController/Delete/5

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




        public async Task<ActionResult> SellerPropertyDelete(int id)

        {

            Property bObj = new Property();

            using (var httpClient = new HttpClient())

            {

                using (var response = await httpClient.GetAsync(url + id.ToString()))

                {

                    string apiResponse = await response.Content.ReadAsStringAsync();

                    bObj = JsonConvert.DeserializeObject<Property>(apiResponse);

                }

            }

            return View(bObj);

        }

        // POST: PropertyController/Delete/5

        [HttpPost]

        [ValidateAntiForgeryToken]

        public async Task<ActionResult> SellerPropertyDelete(int id, IFormCollection collection)

        {

            using (var httpClient = new HttpClient())

            {

                using (var response = await httpClient.DeleteAsync(url + id))

                {

                    string apiResponse = await response.Content.ReadAsStringAsync();

                }

            }
			ViewBag.Message = "Seller";
			return RedirectToAction("GetPropertySellerId", "Property");

        }

        //// GET: PropertyController
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //// GET: PropertyController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: PropertyController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: PropertyController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: PropertyController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: PropertyController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: PropertyController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: PropertyController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        [Route("/SellerProperties")]
		public async Task<ActionResult> GetPropertySellerId()

		{


			try

			{

				using (var client = new HttpClient())

				{
					var sellername = HttpContext.Session.GetString("UserName");

					// Assuming the search API endpoint is different from the details endpoint

					var response = await client.GetAsync($"https://localhost:7119/api/PropertiesAPI/SellerId?name={sellername}");

					

					response.EnsureSuccessStatusCode(); // Ensure successful response, otherwise, an exception will be thrown.

					string apiResponse = await response.Content.ReadAsStringAsync();

					// Assuming the API response is a list of PropertyViewModel objects

					var searchResults = JsonConvert.DeserializeObject<List<Property>>(apiResponse);

                    foreach(var record in searchResults)
                    {
						HttpContext.Session.SetInt32("SellerId", record.SellerId);
                        break;
					}

					

					ViewBag.Message = "Seller";
					return View("SellerIndex", searchResults); // Pass searchResults to the Search view

				}

			}

			catch (HttpRequestException ex)

			{

				// Log or handle the exception

				return StatusCode(500, $"Error retrieving data: {ex.Message}");

			}

			catch (JsonException ex)

			{

				// Log or handle the exception

				return StatusCode(500, $"Error deserializing JSON: {ex.Message}");

			}

		}
	}
}
