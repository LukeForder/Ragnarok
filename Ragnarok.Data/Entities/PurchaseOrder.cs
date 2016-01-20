using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ragnarok.Data.Entities
{
    public class PurchaseOrder
    {
        public virtual long Id { get; set; }
        public virtual string Reference { get; set; }
        public virtual ICollection<PurchaseOrderItem> Items { get; set; }
    }

}
