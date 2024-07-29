using EasyHousingSolutionWebAPI.Models;

namespace EasyHousingSolutionWebAPI.Repository.IRepository
{
	public interface ISellerRepository
	{
		Task<List<Seller>> GetSellerAsync();
		Task<Seller>? GetSellerByIDAsync(int id);

		Task<Seller>? GetSellerByUserNameAsync(string userName);
		Task<List<Seller>>? AddSellerAsync(Seller sp);

		Task<List<Seller>>? UpdateSellerAsync(int id, Seller sp);
		Task<List<Seller>>? DeleteSellerAsync(int id);
	}
}