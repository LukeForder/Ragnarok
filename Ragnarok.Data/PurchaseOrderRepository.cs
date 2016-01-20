using Ragnarok.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ragnarok.Data
{
    public class PurchaseOrderRepository : DbContext, IPurchaseOrderRepository, IPurchaseOrderView
    {
        public IQueryable<PurchaseOrderView> PurchaseOrders
        {
            get
            {
                return
                    Set<PurchaseOrder>()
                        .Select(purchaseOrder => new PurchaseOrderView
                        {
                            Id = purchaseOrder.Id,
                            Items =
                                    purchaseOrder
                                    .Items
                                    .Select(
                                        item =>
                                        new PurchaseOrderItemView
                                        {
                                            Id = item.Id,
                                            Description = item.Product.Description,
                                            Price = item.Price,
                                            ProductId = item.ProductId,
                                            Quantity = item.Quantity
                                        }),
                            OrderReference = purchaseOrder.Reference
                        })
                        .AsQueryable();
            }
        }

        public PurchaseOrderRepository(string connectionStringOrName)
            : base(connectionStringOrName)
        {
        }

        public PurchaseOrderRepository()
        {
        }

        public async Task<long> AddProductAsync(Product product)
        {
            Set<Product>().Add(product);
            await SaveChangesAsync();
            return product.Id;
        }
        
        public async Task<long> AddPurchaseAsync(PurchaseOrder purchaseOrder)
        {
            try
            {
                Set<PurchaseOrderItem>().AddRange(purchaseOrder.Items);
                Set<PurchaseOrder>().Add(purchaseOrder);
                await SaveChangesAsync();
                return purchaseOrder.Id;
            }
            catch (Exception wz)
            {

                throw;
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PurchaseOrder>();
            modelBuilder.Entity<Product>();
            modelBuilder.Entity<PurchaseOrderItem>();
            
            
        }

        public Task<PurchaseOrderView> GetAsync(long id)
        {
            return
                Set<PurchaseOrder>()
                    .Select(purchaseOrder => new PurchaseOrderView
                    {
                        Id = purchaseOrder.Id,
                        Items =
                                purchaseOrder
                                .Items
                                .Select(
                                    item =>
                                    new PurchaseOrderItemView
                                    {
                                        Id = item.Id,
                                        Description = item.Product.Description,
                                        Price = item.Price,
                                        ProductId = item.ProductId,
                                        Quantity = item.Quantity
                                    }),
                        OrderReference = purchaseOrder.Reference
                    })
                    .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
