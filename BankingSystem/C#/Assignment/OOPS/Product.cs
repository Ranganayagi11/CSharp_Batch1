using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPS
{
    public class ProductList
    {
       private int Id { get; set; } =1;
       private string Name { get; set; } 
        public ProductList() { }
        public ProductList(int id,string name) 
        { 
            this.Id= id;
            this.Name = name;
        }
        
        public  void DisplayProduct()
        {
            Console.WriteLine($"Id: {Id},Product Name: {Name}");
        }
        
    }
}
