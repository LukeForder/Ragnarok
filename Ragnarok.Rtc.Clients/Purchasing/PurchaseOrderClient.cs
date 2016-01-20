using Ragnarok.Rtc.Clients.Interfaces.Purchasing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ragnarok.Rtc.Clients.Interfaces.Purchasing.Commands;
using Ragnarok.Rtc.Clients.Interfaces.Purchasing.Events;
using Microsoft.AspNet.SignalR.Client;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reactive;

namespace Ragnarok.Rtc.Clients.Purchasing
{
    public class PurchaseOrderClient : IPurchaseOrderClient
    {
        private readonly Func<HubConnection> _connectionFactory;
        private readonly Lazy<IHubProxy> _proxy;
        private readonly Lazy<IObservable<OrderCreatedEvent>> _orderCreatedEvents;

        public PurchaseOrderClient(
            Func<HubConnection> connectionFactory)
        {
            _connectionFactory = connectionFactory;

            _proxy = new Lazy<IHubProxy>(() =>
            {
                var connection = connectionFactory();

                var proxy = connection.CreateHubProxy("PurchaseOrderHub");

                connection.Start().Wait();
                
                return proxy;

            });

            _orderCreatedEvents = new Lazy<IObservable<OrderCreatedEvent>>(
                () => Observable.Create<OrderCreatedEvent>(
                    observer =>  
                        _proxy
                        .Value
                        .On<OrderCreatedEvent>(
                            nameof(OrderCreatedEvent), 
                            @event => observer.OnNext(@event))));
        }
        
        public IObservable<OrderCreatedEvent> OrderCreatedEvents
        {
            get
            {
                return _orderCreatedEvents.Value;
            }
        }
        
        public async Task CreateAsync(CreatePurchaseOrderCommand command)
        {
            await _proxy.Value.Invoke(nameof(CreateAsync), command);
        }
    }
}
