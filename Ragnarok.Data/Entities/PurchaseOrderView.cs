using System;
using System.Collections.Generic;

namespace Ragnarok.Data.Entities
{
    public class PurchaseOrderView
    {
        public long Id { get; set; }

        public string OrderReference { get; set; }

        public DateTime CreatedAt { get; set; }

        public IEnumerable<PurchaseOrderItemView> Items { get; set; }
    }
}