using Ragnarok.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ragnarok.Data
{
    public interface IPurchaseOrderRepository
    {
        Task<long> AddProductAsync(Product product);

        Task<long> AddPurchaseAsync(PurchaseOrder purchaseOrder);
    }
}
