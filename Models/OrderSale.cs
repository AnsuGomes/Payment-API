using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Payment_API.Enums;

namespace Payment_API.Models
{
    public class OrderSale
    {
        public int Id { get; set; }
        public StatusTransitions Status { get; set; }
        public decimal Amount { get; set; }
        public DateTime CurrentDate { get; set; }
        public List<Product> Products { get; set; }

        public int SellerId { get; set; }
        public Seller Seller { get; set; }
    }
}