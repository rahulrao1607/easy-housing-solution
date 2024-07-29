using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace EasyHousingSolutionMVC.Models;

public partial class Cart
{
    [DisplayName("Cart Id")]
    public int CartId { get; set; }
	[DisplayName("Buyer Id")]
	public int BuyerId { get; set; }
	[DisplayName("Property Id")]
	public int PropertyId { get; set; }

    //public virtual Buyer Buyer { get; set; } = null!;

    //public virtual Property Property { get; set; } = null!;
}
