using EasyHousingSolutionWebAPI.Models;
using EasyHousingSolutionWebAPI.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EasyHousingSolutionWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuyersAPIController : ControllerBase
    {
        BuyerRepository buyerRepository = null;
        public BuyersAPIController()
        {
            buyerRepository = new BuyerRepository();
        }


        // GET: api/<BuyersAPIController>
        [HttpGet]
        public List<Buyer> Get()
        {
            return buyerRepository.GetAll();
        }


        // GET api/<BuyersAPIController>/5
        [HttpGet]
        [Route("GetDetails/{id}")]
        public Buyer Get(int id)
        {
            return buyerRepository.GetById(id);
        }

        [HttpGet]
        [Route("GetCart/{buyerId}")]
        public async Task<IActionResult> GetCartProperties(int buyerId)
        {
            var carts = buyerRepository.ViewProperties(buyerId);
            return Ok(carts);
        }
        // POST api/<BuyersAPIController>
        [HttpPost]
        public ActionResult Post([FromBody] Buyer buyer)
        {
            buyerRepository.Add(buyer);
            return Ok(buyer);
        }

        // PUT api/<BuyersAPIController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Buyer value)
        {
            buyerRepository.Update(id, value);
            return Ok();
        }

        // DELETE api/<BuyersAPIController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            buyerRepository.Delete(id);
            return Ok();
        }


        [HttpPost]
        [Route("AddToCart/{propId}/{buyerId}")]
        public ActionResult AddToCart(int propId, int buyerId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                if (buyerRepository.IsPropertyExists(propId, buyerId))
                {

                    return BadRequest("Property already exists into the cart");
                }
                else
                {
                    buyerRepository.AddCart(propId, buyerId);
                    return Ok("Product Added");
                }

            }
        }

        [HttpDelete]
        [Route("RemoveProperty/{buyerId}/{propId}")]
        public async Task<IActionResult> DeleteProperty(int buyerId, int propId)
        {
            if (buyerRepository.DeleteFromCart(buyerId, propId))
            {
                return Ok("Property remove from the cart");
            }
            return BadRequest();
        }

		// Get Buyer by username 

		[HttpGet]
		[Route("GetBuyerByUserName/{userName}")]
		public async Task<ActionResult<Seller>> GetBuyerByUserNameAsync(string userName)
		{
			Buyer? buyer = await buyerRepository.GetBuyerByUserNameAsync(userName);
			if (buyer == null)
			{
				return NotFound("No Seller with this userName ");
			}
			return Ok(buyer);
		}

	}
}