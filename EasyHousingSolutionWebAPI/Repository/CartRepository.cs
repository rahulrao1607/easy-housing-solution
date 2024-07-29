using System;
using EasyHousingSolutionWebAPI.Models;
using EasyHousingSolutionWebAPI.Repository.IRepository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace EasyHousingSolutionWebAPI.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly EasyHousingDbContext _db;
        private IConfiguration configuration;

        public CartRepository(EasyHousingDbContext context, IConfiguration configuration)
        {
            _db = context;
            this.configuration = configuration;

        }

        public ICollection<Cart> GetAllItems()
        {
            return _db.Carts.OrderBy(c => c.CartId).ToList();

        }

        public IEnumerable<CartPropertyDTO> GetItemsByBuyerID(int buyId)
        {

            var result = from itemCarts in _db.Carts
                         join itemProperty in _db.Properties
                         on itemCarts.PropertyId equals itemProperty.PropertyId
                         where itemCarts.BuyerId == buyId
                         select new CartPropertyDTO
                         {
                             CartId = itemCarts.CartId,
                             BuyerId = itemCarts.BuyerId,
                             PropertyId = itemCarts.PropertyId,
                             PropertyName = itemProperty.PropertyName,
                             PropertyType = itemProperty.PropertyType,
                             Description = itemProperty.Description,
                             Address = itemProperty.Address,
                             PriceRange = itemProperty.PriceRange,
                             InitialDeposit = itemProperty.InitialDeposit,
                             Landmark = itemProperty.Landmark,
                             IsActive = itemProperty.IsActive,
                             SellerId = itemProperty.SellerId
                         };

            return result;
        }

        public Cart ShowDetails(int cartId)
        {

			//var newObj = from itemCarts in _db.Carts
			//			 where itemCarts.CartId == cartId
			//			 select new Cart
			//			 {
			//				 CartId = itemCarts.CartId,
			//				 BuyerId = itemCarts.BuyerId,
			//				 PropertyId = itemCarts.PropertyId
			//			 };

			Cart cObj = new Cart();
            cObj = _db.Carts.FirstOrDefault(c => c.CartId == cartId);

            return cObj;
        }

        //public CartPropertyDTO ShowDetailsProp(int propId)
        //{
        //	CartPropertyDTO cObj = new CartPropertyDTO();
        //          cObj = _db.CartProperties.FirstOrDefault(id => id.PropertyId == propId);

        //	return cObj;
        //}



        //public bool DeleteAllCartItems()
        //{
        //    if(_db.Carts.IsNullOrEmpty())
        //    {
        //        throw new Exception("Can not delete elements in empty cart");

        //    }
        //    else
        //    {
        //        _db.Carts.ExecuteDelete();
        //        _db.SaveChanges();
        //        return true;
        //    }
        //}

        public bool EmptyCart(int buyId)
        {
            bool flag = false;
            var result = from itemCarts in _db.Carts
                         where itemCarts.BuyerId == buyId
                         select itemCarts;
            if (result != null)
            {
                _db.Carts.RemoveRange(result);
                _db.SaveChanges();
                flag = true;
            }
            return flag;

        }

        public bool DeleteProperty(int cartId)
        {
            var result = from itemCarts in _db.Carts
                         where itemCarts.CartId == cartId
                         select itemCarts;

            _db.Carts.RemoveRange(result);
            _db.SaveChanges();
            return true;

        }

        public bool AddToCart(int propId, int buyerId)
        {
            var cartItem = new Cart { PropertyId = propId, BuyerId = buyerId };
            _db.Carts.Add(cartItem);
            _db.SaveChanges();
            return true;
        }

        public bool IsCartEmpty()
        {
            if (_db.Carts.IsNullOrEmpty())
            {
                return true;

            }
            else
            {
                return false;
            }

        }

        public bool IsPropertyExists(int propId, int buyerId)
        {
            bool value = _db.Carts.Any(property => property.PropertyId == propId && property.BuyerId == buyerId);
            return value;
        }

        //public bool DeleteItemByBuyerIdPropertyId(int buyId, int propId)
        //{
        //    throw new NotImplementedException();
        //}
    }
}