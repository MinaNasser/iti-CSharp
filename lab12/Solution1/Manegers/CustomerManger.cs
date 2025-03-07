using Data.Providers;
using Models;

namespace Manegers
{
    public class CustomerManger : BaseManger<Customer>
    {
        private readonly CustomerProvider _customerProvider;

        public CustomerManger(CustomerProvider customerProvider)
        {
            _customerProvider = customerProvider ?? throw new ArgumentNullException(nameof(customerProvider));
        }

        public override void Add(Customer item) => _customerProvider.Add(item);
        public override void Update(Customer item) => _customerProvider.UpdateCustomer(item.Id, item.Name, item.Email, item.Phone);
        public override Customer GetById(int id) => _customerProvider.GetCustomersByID(id).FirstOrDefault();
        public override IList<Customer> GetAll() => _customerProvider.GetAllCustomers();
        public override void Remove(Customer item) => _customerProvider.Remove(item);
        public override void Clear() => _customerProvider.Clear();
    }
}
