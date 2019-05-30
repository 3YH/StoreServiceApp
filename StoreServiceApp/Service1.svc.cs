using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace StoreServiceApp
{
    public class Service1 : IService1
    {
        //User actions
        public IEnumerable<User> GetUsers()
        {
            List<User> users = new List<User>();
            StoreContext sc = new StoreContext();
            users = sc.Users.ToList();
            return users;
        }
        public User FindUserById(int id)
        {
            List<User> users = new List<User>();
            StoreContext sc = new StoreContext();
            users = sc.Users.ToList();
            User founduser = (from user in users
                              where user.Userid == id
                              select user).First();
            return founduser;
        }
        public User FindUserByUserame(string username)
        {
            List<User> users = new List<User>();
            StoreContext sc = new StoreContext();
            users = sc.Users.ToList();
            User founduser = (from user in users
                              where user.Username.Equals(username)
                              select user).First();
            return founduser;
        }
        public string InsertUser(string username)
        {
            List<User> users = new List<User>();
            StoreContext sc = new StoreContext();
            users = sc.Users.ToList();
            bool taken = (from user in users
                          where user.Username == username
                          select user).Any();

            if (!taken)
            {
                char[] name = username.ToArray();
                Array.Reverse(name);
                string pw = new string(name);

                User newUser = new User
                {
                    Username = username,
                    Password = pw,
                    Saldo = 15
                };

                sc.Users.Add(newUser);
                sc.SaveChanges();
                return "Your password is:  " + pw;
            }
            else
            {
                return "Username taken.";
            }

        }

        public int Login(string username, string password)
        {
            int login = 0;
            List<User> users = new List<User>();
            StoreContext sc = new StoreContext();
            users = sc.Users.ToList();

            bool finduser = (from user in users
                             where user.Username == username && user.Password == password
                             select user).Any();
            if (finduser)
            {
                login = (from user in users
                         where user.Username == username && user.Password == password
                         select user.Userid).First();
            }
            return login;
        }

        public void UpdateUser(User uobj)
        {
            StoreContext sc = new StoreContext();
            var query = (from user in sc.Users
                         where user.Userid == uobj.Userid
                         select user).First();
            query.Username = uobj.Username;
            query.Password = uobj.Password;
            query.Saldo = uobj.Saldo;
            sc.SaveChanges();
        }
        public void DeleteUser(int userid)
        {
            StoreContext sc = new StoreContext();
            var query = (from user in sc.Users
                         where user.Userid == userid
                         select user).First();
            sc.Users.Remove(query);
            sc.SaveChanges();
        }

        //Product actions
        public IEnumerable<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            StoreContext sc = new StoreContext();
            products = sc.Products.ToList();
            return products;
        }
        public Product FindProductById(int id)
        {
            List<Product> products = new List<Product>();
            StoreContext sc = new StoreContext();
            products = sc.Products.ToList();
            Product foundproduct = (from product in products
                                    where product.Productid == id
                                    select product).First();
            return foundproduct;
        }
        public Product FindProductByName(string name)
        {
            List<Product> products = new List<Product>();
            StoreContext sc = new StoreContext();
            products = sc.Products.ToList();
            Product foundproduct = (from product in products
                                    where product.Name == name
                                    select product).First();
            return foundproduct;
        }
        public void InsertProduct(Product pobj)
        {
            StoreContext sc = new StoreContext();
            sc.Products.Add(pobj);
            sc.SaveChanges();
        }
        public void UpdateProduct(Product pobj)
        {
            StoreContext sc = new StoreContext();
            var query = (from product in sc.Products
                         where product.Productid == pobj.Productid
                         select product).First();
            query.Name = pobj.Name;
            query.Price = pobj.Price;
            query.Stock = pobj.Stock;
            sc.SaveChanges();
        }
        public void DeleteProduct(int Productid)
        {
            StoreContext sc = new StoreContext();
            var query = (from product in sc.Products
                         where product.Productid == Productid
                         select product).First();
            sc.Products.Remove(query);
            sc.SaveChanges();
        }
        public IEnumerable<Inventory> GetInventory()
        {
            List<Inventory> inventory = new List<Inventory>();
            StoreContext sc = new StoreContext();
            inventory = sc.Inventory.ToList();
            return inventory;
        }
        public void InsertInventory(Inventory iobj)
        {
            StoreContext sc = new StoreContext();
            sc.Inventory.Add(iobj);
            sc.SaveChanges();
        }

        //public void InsertInventory(int Userid)
        //{
        //    StoreContext sc = new StoreContext();
        //    Inventory newInventory = new Inventory
        //    {
        //        Userid = Userid
        //    };
        //    sc.Inventory.Add(newInventory);

        //    sc.SaveChanges();
        //}

        public List<Product> FindInventoryProducts(int id)
        {
            StoreContext sc = new StoreContext();
            List<Inventory> inventory = new List<Inventory>();
            //IEnumerable<Product> products = GetProducts();
            List<Product> products = new List<Product>();
            products = sc.Products.ToList();
            inventory = sc.Inventory.ToList();
            List<Product> InventoryProducts = new List<Product>();
            foreach (Inventory inv in inventory)
            {
                if (inv.Userid == id)
                {
                    Product foundproducts = (from product in products
                                             where product.Productid == inv.Productid
                                             select product).First();
                    InventoryProducts.Add(
                            new Product
                            {
                                Stock = inv.Amount,
                                Productid = foundproducts.Productid,
                                Name = foundproducts.Name,
                                Price = foundproducts.Price
                            });
                }
            }
            return InventoryProducts;
        }
        public Inventory FindInventory(int productid, int userid)
        {
            List<Inventory> Inventory = new List<Inventory>();
            StoreContext sc = new StoreContext();
            Inventory = sc.Inventory.ToList();
            Inventory foundinventory = (from item in Inventory
                                        where item.Productid == productid && item.Userid == userid
                                        select item).First();
            return foundinventory;
        }

        public void UpdateAmount(int Productid, int Userid, int amount)
        {
            StoreContext sc = new StoreContext();
            var query = (from inventory in sc.Inventory
                         where inventory.Userid == Userid && inventory.Productid == Productid
                         select inventory).First();
            query.Amount = amount;
            sc.SaveChanges();
        }

        public string BuyProduct(int Userid, int Productid)
        {
            StoreContext sc = new StoreContext();
            Product currentProduct = FindProductById(Productid);
            User currentUser = FindUserById(Userid);
            List<Inventory> inventories = new List<Inventory>();
            List<User> users = new List<User>();
            List<Product> products = new List<Product>();
            users = sc.Users.ToList();
            products = sc.Products.ToList();
            inventories = sc.Inventory.ToList();

            if (currentProduct.Stock >= 0)
            {
                if (currentUser.Saldo - currentProduct.Price >= 0)
                {
                    bool productbought = (from product in inventories
                                          where product.Productid == Productid && product.Userid == Userid
                                          select product).Any();
                    int newSaldo = currentUser.Saldo -= currentProduct.Price;
                    User updatedSaldo = new User
                    {
                        Userid = Userid,
                        Username = currentUser.Username,
                        Password = currentUser.Password,
                        Saldo = newSaldo
                    };
                    if (productbought)
                    {
                        Inventory currentInventory = FindInventory(Productid, Userid);
                        var newAmount = currentInventory.Amount + 1;
                        UpdateAmount(Productid, Userid, newAmount);
                        UpdateUser(updatedSaldo);
                        return currentProduct.Name +  " added to inventory.";
                    }
                    else
                    {
                        Inventory NewEntry = new Inventory
                        {
                            //Inventoryid = currentInventory.Inventoryid,
                            Productid = Productid,
                            Userid = Userid,
                            Amount = 1
                    };
                        sc.Inventory.Add(NewEntry);
                        UpdateUser(updatedSaldo);
                        sc.SaveChanges();
                        return currentProduct.Name + " added to inventory.";
                    }
                    
                }
                else
                {
                    return "Not enough money in your account.";
                }
            }
            else
            {
                return "This product is out of stock";
            }
        }
    }
}
