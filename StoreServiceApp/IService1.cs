using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace StoreServiceApp
{
    [ServiceContract]
    public interface IService1
    {
        //User actions
        [OperationContract]
        IEnumerable<User> GetUsers();
        [OperationContract]
        User FindUserById(int id);
        [OperationContract]
        User FindUserByUserame(string username);
        [OperationContract]
        string InsertUser(string username);
        [OperationContract]
        int Login(string username, string password);
        [OperationContract]
        void UpdateUser(User uobj);
        [OperationContract]
        void DeleteUser(int userid);

        //Product actions
        [OperationContract]
        IEnumerable<Product> GetProducts();
        [OperationContract]
        Product FindProductById(int id);
        [OperationContract]
        Product FindProductByName(string name);
        [OperationContract]
        void InsertProduct(Product pobj);
        [OperationContract]
        void UpdateProduct(Product pobj);
        [OperationContract]
        void DeleteProduct(int Productid);
        [OperationContract]
        IEnumerable<Inventory> GetInventory();
        [OperationContract]
        void InsertInventory(Inventory iobj);
        [OperationContract]
        List<Product> FindInventoryProducts(int id);
        [OperationContract]
        Inventory FindInventory(int productid, int userid);
       [OperationContract]
        string BuyProduct(int Userid, int Productid);
    }
    [DataContract]
    public class User
    {
        [DataMember]
        [Required]
        [Key]
        public int Userid { get; set; }
        [DataMember]
        [Required]
        public string Username { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public int Saldo { get; set; }
    }
    public class Product
    {
        [DataMember]
        [Required]
        [Key]
        public int Productid { get; set; }
        [DataMember]
        [Required]
        public string Name { get; set; }
        [DataMember]
        [Required]
        public int Price { get; set; }
        [DataMember]
        [Required]
        public int Stock { get; set; }
    }
    public class Inventory
    {
        [DataMember]
        [Required]
        [Key]
        public int Inventoryid { get; set; }
        [DataMember]
        [Required]
        public int Productid { get; set; }
        [DataMember]
        [Required]
        public int Userid { get; set; }
        [DataMember]
        public int Amount { get; set; }
    }
}
