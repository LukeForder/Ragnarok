using System;
using System.Collections.Generic;

namespace Ragnarok.Rtc.Clients.Interfaces.Purchasing.Events
{
    public class OrderCreatedEvent : Event
    {
        public long Id { get; set; }

        public string OrderReference { get; set; }

        public DateTime CreatedAt { get; set; }

        public IEnumerable<PurchaseOrderItem> Items { get; set; }

        public class PurchaseOrderItem
        {
            public long Id { get; set; }

            public long ProductId { get; set; }

            public string Description { get; set; }

            public int Quantity { get; set; }

            public decimal Price { get; set; }
        }
    }
}