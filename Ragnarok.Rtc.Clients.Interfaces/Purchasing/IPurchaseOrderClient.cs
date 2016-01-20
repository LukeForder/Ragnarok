using Ragnarok.Rtc.Clients.Interfaces.Purchasing.Commands;
using Ragnarok.Rtc.Clients.Interfaces.Purchasing.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ragnarok.Rtc.Clients.Interfaces.Purchasing
{
    public interface IPurchaseOrderClient
    {
        Task CreateAsync(CreatePurchaseOrderCommand command);

        IObservable<OrderCreatedEvent> OrderCreatedEvents { get; }
    }
}
