using Data.Connections;
using Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace Data.Providers
{
    public class InventoryProvider
    {
        private readonly SQLConnectionHelper _dbHelper;

        public InventoryProvider(SQLConnectionHelper dbHelper)
        {
            _dbHelper = dbHelper ?? throw new ArgumentNullException(nameof(dbHelper));
        }

        public int AddNewItem(string name, string description)
        {
            object result = _dbHelper.ExecuteScalar("sp_add_item", cmd =>
            {
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@description", description);
            });

            if (result == null || result == DBNull.Value)
            {
                Console.WriteLine(" Error: No item ID returned from database.");
                return 0;
            }

            return Convert.ToInt32(result);
        }



        public void AddItemToInventory(int itemId, int quantity)
        {
            _dbHelper.ExecuteNonQuery("sp_add_to_inventory", cmd =>
            {
                cmd.Parameters.AddWithValue("@item_id", itemId);
                cmd.Parameters.AddWithValue("@quantity", quantity);
            });
        }


        public Item GetItemById(int itemId)
        {
            DataTable dt = _dbHelper.ExecuteQuery("sp_get_item_by_id", cmd =>
            {
                cmd.Parameters.AddWithValue("@item_id", itemId);
            });

            if (dt.Rows.Count == 0) return null;

            DataRow row = dt.Rows[0];

            return new Item
            {
                Id = Convert.ToInt32(row["item_id"]),
                Name = row["name"].ToString(),
                Description = row["description"].ToString()
            };
        }


        public void UpdateInventoryQuantity(int itemId, int newQuantity)
        {
            _dbHelper.ExecuteNonQuery("sp_update_inventory_quantity", cmd =>
            {
                cmd.Parameters.AddWithValue("@item_id", itemId);
                cmd.Parameters.AddWithValue("@quantity", newQuantity);
            });
        }


        public bool DecreaseItems(int itemId, int quantity)
        {
            int result = _dbHelper.ExecuteNonQuery("sp_decrease_inventory", cmd =>
            {
                cmd.Parameters.AddWithValue("@item_id", itemId);
                cmd.Parameters.AddWithValue("@quantity", quantity);
            });

            Console.WriteLine(result > 0 ? $"✅ Updated inventory for Item ID {itemId}, Decreased by {quantity}"
                                         : "⚠️ Inventory update failed!");

            return result > 0;
        }


        public void DeleteItemFromInventory(int itemId)
        {
            _dbHelper.ExecuteNonQuery("sp_delete_item_from_inventory", cmd =>
            {
                cmd.Parameters.AddWithValue("@item_id", itemId);
            });
        }


        public List<ItemStock> GetStock()
        {
            DataTable dt = _dbHelper.ExecuteQuery("sp_get_inventory");
            List<ItemStock> stock = new List<ItemStock>();

            foreach (DataRow row in dt.Rows)
            {
                stock.Add(new ItemStock
                {
                    Item = new Item
                    {
                        Id = Convert.ToInt32(row["item_id"]),
                        Name = row["name"].ToString(),
                        Description = row["description"].ToString()
                    },
                    Quantity = Convert.ToInt32(row["quantity"])
                });
            }

            return stock;
        }
    }
}
