using System.Collections.Generic;
using EasyHousingSolutionWebAPI.Models;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
namespace EasyHousingSolutionWebAPI.Repository.IRepository

{

    public interface IPropertyRepository

    {
        bool Add(Property obj);

        bool Update(int id, Property obj);

        bool Delete(int id);

        List<Property> GetAllProperty();

        Property GetById(int id);

        bool Save();

        IEnumerable<Property> GetPropertyBuyOption(string BuyOption);

        IEnumerable<Property> GetPropertySellerId(string SellerName);
        IEnumerable<Property> GetPropertyIsActive(bool IsActive);

        IEnumerable<Property> GetPropertyByPriceHighToLow();
        IEnumerable<Property> GetPropertyByPriceLowToHigh();

        Property UpdatePropertyIsActive(int propertyId);

        bool VerifyProperty(int id);

        IEnumerable<Property> GetPropertySellerIdIsActive(int SellerId, bool IsActive);
    }

}