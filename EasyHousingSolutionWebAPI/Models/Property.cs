using System;
using System.Collections.Generic;

namespace EasyHousingSolutionWebAPI.Models;

public partial class Property
{
    public int PropertyId { get; set; }

    public string PropertyName { get; set; } = null!;

    public string PropertyType { get; set; } = null!;

    public string PropertyOption { get; set; } = null!;

    public string? Description { get; set; }

    public string Address { get; set; } = null!;

    public decimal PriceRange { get; set; }

    public decimal InitialDeposit { get; set; }

    public string Landmark { get; set; } = null!;

    public bool IsActive { get; set; }

    public int SellerId { get; set; }

    public int StateId { get; set; }

    public int CityId { get; set; }

    //public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    //public virtual ICollection<Image> Images { get; set; } = new List<Image>();

    //public virtual Seller Seller { get; set; } = null!;
}

