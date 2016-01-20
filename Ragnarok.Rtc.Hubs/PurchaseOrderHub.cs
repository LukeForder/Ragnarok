using Microsoft.AspNet.SignalR;
using Ragnarok.Data;
using Ragnarok.Rtc.Clients.Interfaces.Purchasing.Commands;
using System.Linq;
using System.Data.Entity;
using Ragnarok.Rtc.Clients.Interfaces.Purchasing.Events;
using System.Threading.Tasks;

namespace Ragnarok.Rtc.Hubs
{
    public class PurchaseOrderHub : Hub
    {
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;
        private readonly IPurchaseOrderView _purchaseOrderView;

        public PurchaseOrderHub(
            IPurchaseOrderRepository purchaseOrderRepository,
            IPurchaseOrderView purchaseOrderView)
        {
            _purchaseOrderView = purchaseOrderView;
            _purchaseOrderRepository = purchaseOrderRepository;
        }

        public async Task CreateAsync(CreatePurchaseOrderCommand command)
        {
            try
            {
                var purchaseOrder = new Data.Entities.PurchaseOrder
                {
                    Items =
                  command
                      .Products
                      .Select(
                          x => new Data.Entities.PurchaseOrderItem
                          {
                              ProductId = x.ProductId,
                              Price = x.Price,
                              Quantity = x.Quantity
                          })
                      .ToList(),
                    Reference = command.OrderReference
                };

                 var purchaseOrderId = await _purchaseOrderRepository.AddPurchaseAsync(purchaseOrder);

                var view = await _purchaseOrderView.GetAsync(purchaseOrderId);

                Clients
                    .All
                    .OrderCreatedEvent(
                        new OrderCreatedEvent
                        {
                            CorrelationId = command.CorrelationId,
                            CreatedAt = view.CreatedAt,
                            Id = view.Id,
                            Items =
                                view
                                .Items
                                .Select(
                                    x =>
                                        new OrderCreatedEvent.PurchaseOrderItem
                                        {
                                            Id = x.Id,
                                            Description = x.Description,
                                            Price = x.Price,
                                            ProductId = x.ProductId,
                                            Quantity = x.Quantity
                                        })
                                .ToList(),
                            OrderReference = view.OrderReference
                        });
            }
            catch (System.Exception ex)
            {

                throw;
            }

        }
    }
}
    