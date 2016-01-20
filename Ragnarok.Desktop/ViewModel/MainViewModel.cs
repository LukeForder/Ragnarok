using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System;
using Ragnarok.Rtc.Clients.Interfaces.Purchasing;
using Ragnarok.Rtc.Clients.Interfaces.Purchasing.Commands;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reactive.Linq;
using Ragnarok.Rtc.Clients.Interfaces.Purchasing.Events;

namespace Ragnarok.Desktop.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly ObservableCollection<object> _orders;
        private readonly RelayCommand _createOrder;
        private readonly IPurchaseOrderClient _purchaseOrderClient;
        private readonly ConcurrentBag<Guid> _pendingOperations;
        private readonly IDisposable _newOrderEventStream;
        private bool _isCreating;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(
            IPurchaseOrderClient purchaseOrderClient)
        {
            _pendingOperations = new ConcurrentBag<Guid>();
            _purchaseOrderClient = purchaseOrderClient;
            _orders = new ObservableCollection<object>();
            _createOrder = new RelayCommand(OnCreateOrder);

            _newOrderEventStream =
                _purchaseOrderClient
                .OrderCreatedEvents
                .ObserveOnDispatcher()
                .Subscribe(OnNewPurchaseOrder);
        }

        private void OnNewPurchaseOrder(OrderCreatedEvent args)
        {
            Orders.Add(args);
        }

        private bool CanCreateOrder()
        {
            return !IsCreating;
        }

        private async void OnCreateOrder()
        {
            IsCreating = true;
            try
            {

                var operationId = Guid.NewGuid();
                var rng = new Random();

                await _purchaseOrderClient.CreateAsync(
                    new CreatePurchaseOrderCommand
                    {
                        CorrelationId = operationId,
                        OrderReference = $"ORD{rng.Next()}",
                        Products =
                            new List<CreatePurchaseOrderCommand.Product>
                            {
                            new CreatePurchaseOrderCommand.Product
                            {
                                Price = Convert.ToDecimal(Math.Round(rng.NextDouble() * 100, 10)),
                                ProductId = 1,
                                Quantity = rng.Next(1, 5)
                            }
                            }
                    });
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public ObservableCollection<object> Orders => _orders;

        public ICommand CreateOrder => _createOrder;

        public bool IsCreating
        {
            get
            {
                return _isCreating;
            }
            private set
            {
                _isCreating = value;
                this.RaisePropertyChanged();
                _createOrder.RaiseCanExecuteChanged();
            }
        }
    }

}