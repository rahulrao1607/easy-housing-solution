using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EasyHousingSolutionWebAPI.Models;
using EasyHousingSolutionWebAPI.Repository.IRepository;

namespace EasyHousingSolutionWebAPI.Models
{
    public class BuyerRepository : IBuyerRepository<Buyer>
    {

        private readonly EasyHousingDbContext dbConn;

        public BuyerRepository()
        {
            dbConn = new EasyHousingDbContext();
        }

        // Get all buyers List
        public List<Buyer> GetAll()
        {
            return dbConn.Buyers.ToList();
        }

        //Get buyer list by ID
        public Buyer GetById(int id)
        {
            Buyer bObj = dbConn.Buyers.FirstOrDefault(c => c.BuyerId == id);
            return bObj;
            // return dbConn.Buyers.FirstOrDefault(b => b.BuyerId == id);
        }


        // to add buyer 
        public bool Add(Buyer buyer)
        {
            dbConn.Buyers.Add(buyer);
            dbConn.SaveChanges();
            return true;
        }


        // to update the buyer result
        public bool Update(int id, Buyer buyer)
        {
            //bool flag = false;
            if (id != buyer.BuyerId)
            {
                return false;
            }
            else
            {
                Buyer bObj = dbConn.Buyers.Find(id);
                bObj.FirstName = buyer.FirstName;
                bObj.LastName = buyer.LastName;
                bObj.PhoneNo = buyer.PhoneNo;
                bObj.DateofBirth = buyer.DateofBirth;
                bObj.EmailId = buyer.EmailId;

                dbConn.SaveChanges();
                return true;
            }
            return false;
        }

        // for deleting the buyer 
        public bool Delete(int id)
        {
            //bool flag = false;
            var existingBuyer = GetById(id);
            if (existingBuyer != null)
            {
                dbConn.Buyers.Remove(existingBuyer);
                dbConn.SaveChanges();
                return true;
            }

            return false; // Return false if buyer with given id is not found
        }

        // for saving the changes 
        //public bool Save()
        //{
        //    return dbConn.SaveChanges() >= 0;
        //}


        public bool AddCart(int propId, int buyId)
        {
            var cartItem = new Cart { PropertyId = propId, BuyerId = buyId };
            dbConn.Carts.Add(cartItem);
            dbConn.SaveChanges();
            return true;
        }

        public bool DeleteFromCart(int buyerId, int propId)
        {
            var result = from itemCarts in dbConn.Carts
                         where itemCarts.BuyerId == buyerId && itemCarts.PropertyId == propId
                         select itemCarts;

            dbConn.Carts.RemoveRange(result);
            dbConn.SaveChanges();
            return true;


        }

        public bool IsPropertyExists(int propId, int buyerId)
        {
            bool value = dbConn.Carts.Any(property => property.PropertyId == propId && property.BuyerId == buyerId);
            return value;
        }


        public List<CartPropertyDTO> ViewProperties(int buyerId)
        {
            var result = from itemCarts in dbConn.Carts
                         join itemProperty in dbConn.Properties
                         on itemCarts.PropertyId equals itemProperty.PropertyId
                         where itemCarts.BuyerId == buyerId
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

            return result.ToList();
        }

		public async Task<Buyer>? GetBuyerByUserNameAsync(string userName)
		{
			Buyer? buyer = await dbConn.Buyers.FirstOrDefaultAsync(u => u.UserName == userName);
			if (buyer == null)
			{
				return null;
			}
			return buyer;
		}
	}
}