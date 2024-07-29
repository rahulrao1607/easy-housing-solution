using EasyHousingSolutionWebAPI.Models;

namespace EasyHousingSolutionWebAPI.Repository.IRepository
{
    public interface ICartRepository
    {


        IEnumerable<CartPropertyDTO> GetItemsByBuyerID(int buyId);

        //bool DeleteAllCartItems();

        Cart ShowDetails(int cartId);
        //CartPropertyDTO ShowDetailsProp(int propId);

        bool EmptyCart(int buyerId);

        bool DeleteProperty(int cartId);

        bool AddToCart(int propId, int BuyerId);

        bool IsCartEmpty();

        bool IsPropertyExists(int propId, int buyerId);






    }
}