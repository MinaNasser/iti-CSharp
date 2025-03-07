using Data.Providers;
using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Manegers
{
    public class OrderManger : BaseManger<Order>
    {
        private readonly OrderProvider _orderProvider;

        public OrderManger(OrderProvider orderProvider)
        {
            _orderProvider = orderProvider ?? throw new ArgumentNullException(nameof(orderProvider));
        }

        public override void Add(Order item)
        {
            string itemsJson = JsonConvert.SerializeObject(item.Items.Select(i => new
            {
                item_id = i.Item.Id,
                qty = i.Qty,
                price = i.Price
            }));

            _orderProvider.AddOrderWithInventory(item.CustomerID, itemsJson);
        }

        public override void Update(Order item) => _orderProvider.UpdateOrder(item.Id, item.Price, item.Amount, item.Status);
        public override Order GetById(int id) => _orderProvider.GetOrderByID(id).FirstOrDefault();
        public override IList<Order> GetAll() => _orderProvider.GetAllOrders();
        public override void Remove(Order item) => _orderProvider.Remove(item);
        public override void Clear() => _orderProvider.Clear();
        public int AddAndReturnId(Order order)
        {
            return _orderProvider.AddOrderAndReturnId(order);
        }



    }
}
