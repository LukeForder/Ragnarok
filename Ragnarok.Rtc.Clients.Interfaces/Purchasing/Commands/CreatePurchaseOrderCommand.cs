using System;
using System.Collections;
using System.Collections.Generic;

namespace Ragnarok.Rtc.Clients.Interfaces.Purchasing.Commands
{
    public class CreatePurchaseOrderCommand : Command
    {
        public string OrderReference { get; set; }

        public IEnumerable<Product> Products { get; set; }

        public class Product
        {
            public long ProductId { get; set; }

            public int Quantity { get; set; }

            public decimal Price { get; set; }
        }
    }
}