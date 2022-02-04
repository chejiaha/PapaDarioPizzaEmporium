using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaDarioPizzaEmporium
{
    class Checkout
    {
        // handles the checkout button in cart flyout
        public void goCheckout()
        {
            DataAccess da = new DataAccess();
            InCart cart = new InCart();
            Login login = new Login();

            string order = "";
            // gets items from list in respective wings and pizza class
            foreach (GetWings item in Wings.wingsList)
            {
                order += item.name;
            }
            foreach (GetPizza item in Pizza.pizzaList)
            {
                order += item.name;
            }
            if (login.isLoggedIn())
            {
                // if user is logged in then add all user info into receipt table
                int id = login.userID();
                da.addReceipt(id, da.getUser(id), da.getEmail(id), da.getAddress(id), order, 10, cart.getTotal());
            }
            else
            {
                // there is not user logged in then add entries as guest
                da.addReceipt(0, "Guest", "N/A", "N/A", order, 0, cart.getTotal());
            }
        }
    }
}
