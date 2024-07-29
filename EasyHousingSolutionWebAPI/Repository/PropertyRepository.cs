using System;

using System.Collections.Generic;

using System.Linq;

using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;

using EasyHousingSolutionWebAPI.Models;
using EasyHousingSolutionWebAPI.Repository.IRepository;

namespace EasyHousingSolutionWebAPI.Repository

{

    public class PropertyRepository : IPropertyRepository
    {

        private readonly EasyHousingDbContext dbConn;
        private IConfiguration configuration;

        public PropertyRepository(EasyHousingDbContext db, IConfiguration configuration)

        {

            dbConn = db;
            this.configuration = configuration;

        }

        // Get all Property List

        //  public List<Property> GetAllProperty
        public List<Property> GetAllProperty()
        {

            var item = dbConn.Properties.OrderBy(p => p.PropertyId).ToList();
            return item;
        }

        //Get Property list by ID

        public Property GetById(int id)

        {

            //  Property bObj = dbConn.Properties.Find(id);

            //  return bObj;

            return dbConn.Properties.FirstOrDefault(p => p.PropertyId == id);

        }

        public IEnumerable<Property> GetPropertyBuyOption(string BuyOption)
        {
            var qResult = from item in dbConn.Properties
                          where item.PropertyOption == BuyOption
                          select item;
            return qResult;

        }

        public IEnumerable<Property> GetPropertySellerId(string SellerName)
        {

            var sellerObj = from item in dbConn.Sellers
                            where item.UserName == SellerName
                            select item;
            var id = 0;
            foreach(Seller obj in sellerObj)
            {
                id = obj.SellerId;
            }

			var qResult = from item in dbConn.Properties
                          where item.SellerId == id
                          select item;
            return qResult;

        }

        public IEnumerable<Property> GetPropertySellerIdIsActive(int SellerId, bool IsActive)
        {
            var qResult = from item in dbConn.Properties
                          where item.SellerId == SellerId
&& item.IsActive == IsActive
                          select item;
            return qResult;

        }

        public IEnumerable<Property> GetPropertyIsActive(bool IsActive)
        {
            var qResult = from item in dbConn.Properties
                          where item.IsActive == IsActive
                          select item;
            return qResult;

        }


        public IEnumerable<Property> GetPropertyByPriceHighToLow()
        {
            var qResult = from item in dbConn.Properties
                          orderby item.PriceRange descending
                          select item;
            return qResult;
        }

        public IEnumerable<Property> GetPropertyByPriceLowToHigh()
        {

            var qResult = from item in dbConn.Properties
                          orderby item.PriceRange
                          select item;
            return qResult;
        }

        public Property UpdatePropertyIsActive(int propertyId)
        {
            var property = dbConn.Properties.FirstOrDefault(p => p.PropertyId == propertyId);

            if (property != null)
            {
                property.IsActive = true;
                dbConn.SaveChanges(); // Save changes to the database
            }

            return property;
        }


        public bool VerifyProperty(int id)
        {
            var property = dbConn.Properties.Find(id);
            if (property == null)
            {
                return false;
            }
            property.IsActive = true;
            dbConn.SaveChanges();

            return true;
        }
        // to add Property

        public bool Add(Property Property)

        {

            dbConn.Properties.Add(Property);

            //            dbConn.SaveChanges();
            //   dbConn.SaveChanges();//Save();
            return Save();
        }


        // to update the Property result

        public bool Update(int id, Property Property)

        {

            // bool flag = false;

            if (id != Property.PropertyId)

            {

                return false;

            }

            else

            {

                Property bObj = dbConn.Properties.Find(id);
                bObj.PropertyName = Property.PropertyName;
                bObj.PropertyType = Property.PropertyType;
                bObj.Address = Property.Address;
                bObj.PriceRange = Property.PriceRange;
                bObj.InitialDeposit = Property.InitialDeposit;
                bObj.Landmark = Property.Landmark;
                bObj.IsActive = Property.IsActive;
                bObj.Description = Property.Description;
                //bObj.FirstName = Property.FirstName;

                //bObj.LastName = Property.LastName;

                //bObj.PhoneNo = Property.PhoneNo;

                //bObj.DateofBirth = Property.DateofBirth;

                //bObj.EmailId = Property.EmailId;

                //  dbConn.SaveChanges();
                return Save();

            }

            return false;

        }

        // for deleting the Property

        public bool Delete(int id)

        {

            bool flag = false;

            var existingProperty = GetById(id);

            if (existingProperty != null)

            {

                dbConn.Properties.Remove(existingProperty);

                // dbConn.SaveChanges();

                return Save();

            }

            return flag; // Return false if Property with given id is not found

        }

        //  for saving the changes

        public bool Save()

        {

            return dbConn.SaveChanges() >= 0;
            

        }
    }

}