using Ragnarok.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ragnarok.Data.Mappings
{
    public class PurchaseOrderItemMapping : EntityTypeConfiguration<PurchaseOrderItem>
    {
        public PurchaseOrderItemMapping()
        {
            HasRequired(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .WillCascadeOnDelete();
            
            HasRequired(x => x.PurchaseOrder)
                .WithMany()
                .HasForeignKey(x => x.PurchaseOrderId)
                .WillCascadeOnDelete();
        }
    }
}
