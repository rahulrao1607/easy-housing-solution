using EasyHousingSolutionWebAPI.Models;
using EasyHousingSolutionWebAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;

namespace EasyHousingSolutionWebAPI.Repository
{
	public class SellerRepository : ISellerRepository
	{
		private readonly EasyHousingDbContext _dataContext;

		public SellerRepository(EasyHousingDbContext dataContext)
		{
			_dataContext = dataContext;
		}

		public async Task<List<Seller>>? AddSellerAsync(Seller sp)
		{
			_dataContext.Sellers.Add(sp);
			await _dataContext.SaveChangesAsync();

			return await _dataContext.Sellers.ToListAsync();
		}

		public async Task<List<Seller>>? DeleteSellerAsync(int id)
		{
			Seller? sp = await _dataContext.Sellers.FindAsync(id);
			if (sp == null)
			{
				return null;
			}
			_dataContext.Sellers.Remove(sp);
			await _dataContext.SaveChangesAsync();

			return await _dataContext.Sellers.ToListAsync();
		}

		public async Task<List<Seller>> GetSellerAsync()
		{
			return await _dataContext.Sellers.ToListAsync();
		}

		public async Task<Seller>? GetSellerByIDAsync(int id)
		{
			Seller? sp = await _dataContext.Sellers.FindAsync(id);
			if (sp == null)
			{
				return null;
			}
			return sp;
		}


		public async Task<Seller>? GetSellerByUserNameAsync(string userName)
		{
			Seller? sp = await _dataContext.Sellers.FirstOrDefaultAsync(u => u.UserName == userName);
			if (sp == null)
			{
				return null;
			}
			return sp;
		}

		public async Task<List<Seller>>? UpdateSellerAsync(int id, Seller sellers)
		{
			Seller? sp = _dataContext.Sellers.Find(id);
			if (sp == null)
			{
				return null;
			}

			sp.UserName = sellers.UserName;
			sp.FirstName = sellers.FirstName;
			sp.LastName = sellers.LastName;
			sp.DateofBirth = sellers.DateofBirth;
			sp.PhoneNo = sellers.PhoneNo;
			sp.Address = sellers.Address;
			sp.SellerId = sellers.SellerId;
			sp.CityId = sellers.CityId;
			sp.EmailId = sellers.EmailId;

			await _dataContext.SaveChangesAsync();
			return await _dataContext.Sellers.ToListAsync();
		}
	}
}

