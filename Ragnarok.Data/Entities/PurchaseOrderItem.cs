namespace Ragnarok.Data.Entities
{
    public class PurchaseOrderItem
    {
        public virtual long Id { get; set; }
        public virtual int Quantity { get; set; }
        public virtual decimal Price { get; set; }
        public virtual long ProductId { get; set; }
        public virtual Product Product { get; set; }
        public virtual long PurchaseOrderId { get; set; }
        public virtual PurchaseOrder PurchaseOrder { get; set; }
    }
}