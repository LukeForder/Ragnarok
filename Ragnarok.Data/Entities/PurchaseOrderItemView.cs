namespace Ragnarok.Data.Entities
{
    public class PurchaseOrderItemView
    {
        public long Id { get; set; }

        public long ProductId { get; set; }
        
        public string Description { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}