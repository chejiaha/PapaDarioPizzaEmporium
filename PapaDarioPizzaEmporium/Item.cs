using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaDarioPizzaEmporium
{
    public abstract class Item
    {
        public abstract void getPizza(int id, string name, string size, double price);
        public abstract void getWings(int id, string name, string size, double price);
    }
}
