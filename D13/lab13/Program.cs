

public class Program
{
    public static int Main()
    {
        Order o = new Order() { 
            Id = 1,
            Date = DateTime.Now,
            Customer = new Customer()
            {
                Id =1,
                Name= "Mina"
            },
            Items =new List<OrderItem>()
            {
                new OrderItem()
                {
                    Id=1,
                    Qty = 1,
                    Price = 1,
                    Item =new Item()
                    {
                        Id =1,
                        Name="Phone",
                        Description="djhswdhasdnas"
                    }
                },
                new OrderItem()
                {
                    Id=2,
                    Qty = 1,
                    Price = 1,
                    Item =new Item()
                    {
                        Id =1,
                        Name="Phone",
                        Description="djhswdhasdnas"
                    }
                }
            }
        
        };

        
        Inventory i = new Inventory();
        o.OnAdded += i.Dicress;


        Invois invois = new Invois();
        o.OnAdded += invois.Isuue;

        o.Add();
        return 0;
    }
}