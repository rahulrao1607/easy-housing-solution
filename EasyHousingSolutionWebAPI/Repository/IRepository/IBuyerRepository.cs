using EasyHousingSolutionWebAPI.Models;

namespace EasyHousingSolutionWebAPI.Repository.IRepository
{
    public interface IBuyerRepository<T>
    {
        bool Add(T obj);
        bool Update(int id, T obj);

        bool Delete(int id);

        List<T> GetAll();

        T GetById(int id);

		Task<Buyer>? GetBuyerByUserNameAsync(string userName);

		bool AddCart(int propId, int buyerId);

        bool DeleteFromCart(int buyerId, int propId);

        bool IsPropertyExists(int propId, int buyerId);

        List<CartPropertyDTO> ViewProperties(int buyerId);
        // bool Save();
    }
}