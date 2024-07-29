using System.Security.Policy;

namespace EasyHousingSolutionWebAPI.Models
{
    public class CartPropertyDTO
    {
        public int CartId { get; set; }

        public int BuyerId { get; set; }

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
    }
}

