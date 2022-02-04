using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Audio;
using Windows.Services.Store;

namespace PapaDarioPizzaEmporium
{
    // properties class that stores variables for Pizza object
    public class GetPizza : Item
    {
        public int id { get; set; }
        public string name { get; set; }
        public string size { get; set; }
        public double price { get; set; }

        public override void getPizza(int id, string name, string size, double price)
        {
            this.id = id;
            this.name = name;
            this.size = size;
            this.price = price;
        }

        public override void getWings(int id, string name, string size, double price)
        {
            throw new NotImplementedException();
        }
    }
}
