using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EasyHousingSolutionWebAPI.Models;
using EasyHousingSolutionWebAPI.Repository.IRepository;
using Newtonsoft.Json.Linq;


namespace EasyHousingSolutionWebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CartsAPIController : ControllerBase
    {
        private ICartRepository cartRepo;


        /// <summary>
        /// Constructor for CartsAPI controller
        /// </summary>
        /// <param name="customerRepository">ICustomerRepository: Interface object of the customer repository</param>
        /// <param name="mapper">IMapper: Interface object for mapper</param>
        public CartsAPIController(ICartRepository cartRepository)
        {
            cartRepo = cartRepository;

        }


        // GET: api/CartsAPI
        [HttpGet]
        public ActionResult<ICollection<CartPropertyDTO>> GetCarts(int buyerId)
        {
            var carts = cartRepo.GetItemsByBuyerID(buyerId);
            return Ok(carts);
        }

        // GET: api/CartsAPI/5
        //[HttpGet("{id}")]
        //public ActionResult<ICollection<Cart>> GetCart(int id)
        //{
        //    var cart =  cartRepo.GetItemsByBuyerID(id);

        //    if (cart == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(cart);

        //}

        //// PUT: api/CartsAPI/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutCart(int id, Cart cart)
        //{
        //    if (id != cart.CartId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(cart).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CartExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/CartsAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostCart(int propId, int buyerId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                if (cartRepo.IsPropertyExists(propId, buyerId))
                {

                    return BadRequest("Property already exists into the cart");
                }
                else
                {
                    cartRepo.AddToCart(propId, buyerId);
                    return Ok("Product Added");
                }

            }
        }


        [HttpGet]
        public async Task<IActionResult> ShowDetails(int cartId)
        {
            var cart = cartRepo.ShowDetails(cartId);
            return Ok(cart);
        }

        //[HttpGet]
        //public async Task<IActionResult> ShowDetailsProperty(int propId)
        //{
        //	var cart = cartRepo.ShowDetailsProp(propId);
        //	return Ok(cart);
        //}



        //[HttpGet("BuyerId")]
        //public IEnumerable<Cart> GetPropertyBuyerId(int BuyerId)
        //{
        //    var result = cartRepo.GetItemsByBuyerID(BuyerId);

        //    return result;

        //}


        // DELETE: api/CartsAPI/5

        [HttpDelete("{buyerId}")]
        public async Task<IActionResult> DeleteAllCart(int buyerId)
        {
            if (cartRepo.EmptyCart(buyerId))
            {
                return Ok("Cart is Empty");
            }
            return BadRequest();
        }


        [HttpDelete("{cartId}")]
        public async Task<IActionResult> DeleteProperty(int cartId)
        {
            if (cartRepo.DeleteProperty(cartId))
            {
                return Ok("Property remove from the cart");
            }
            return BadRequest();
        }

        //[HttpDelete]
        //public async Task<IActionResult> EmptyCart()
        //{
        //    cartRepo.DeleteAllCartItems();
        //    return Ok();
        //}

        private bool CartExists()
        {

            if (cartRepo.IsCartEmpty())
            {
                return false;
            }
            return true;
        }
    }
}