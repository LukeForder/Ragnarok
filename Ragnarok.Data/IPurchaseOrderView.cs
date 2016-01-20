using Ragnarok.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ragnarok.Data
{
    public interface IPurchaseOrderView
    {
        IQueryable<PurchaseOrderView> PurchaseOrders { get; }

        Task<PurchaseOrderView> GetAsync(long id);
    }
}
