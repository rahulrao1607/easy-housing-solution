using EasyHousingSolutionWebAPI.Models;
using EasyHousingSolutionWebAPI.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EasyHousingSolutionWebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SellersAPIController : ControllerBase
	{
		public readonly ISellerRepository _sellerService;
		public SellersAPIController(ISellerRepository sellerService)
		{
			this._sellerService = sellerService;
		}

		[HttpGet]
		public async Task<ActionResult<List<Seller>>> GetSellerAsync()
		{
			return Ok(await _sellerService.GetSellerAsync());
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Seller>> GetSellerByIDAsync(int id)
		{
			Seller? sellers = await _sellerService.GetSellerByIDAsync(id);
			if (sellers == null)
			{
				return NotFound("No Seller with this id ");
			}
			return Ok(sellers);
		}


		// Get seller by username 

		[HttpGet]
		[Route("GetSellerByUserName/{userName}")]
		public async Task<ActionResult<Seller>> GetSellerByUserNameAsync(string userName)
		{
			Seller? sellers = await _sellerService.GetSellerByUserNameAsync(userName);
			if (sellers == null)
			{
				return NotFound("No Seller with this userName ");
			}
			return Ok(sellers);
		}


		[HttpPost]
		public async Task<ActionResult<List<Seller>>> AddSellerAsync(Seller sp)
		{
			return Ok(await _sellerService.AddSellerAsync(sp));
		}
		[HttpPut("{id}")]
		public async Task<ActionResult<List<Seller>>> UpdateSellerAsync(int id, Seller seller)
		{
			var sp = await _sellerService.UpdateSellerAsync(id, seller);
			if (sp == null)
			{
				return NotFound("No Seller with this id");
			}
			return Ok(sp);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<List<Seller>>> DeleteSellerAsync(int id)
		{
			var sp = await _sellerService.DeleteSellerAsync(id);
			if (sp == null)
			{
				return NotFound("No seller with this id");
			}
			return Ok(sp);

		}


	}
}