using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace PapaDarioPizzaEmporium
{
    // class to handle the cart flyout 
    public class InCart
    {
        public static double total;

        public void getCart(TextBlock loginStatus, TextBlock TbDiscount, TextBlock TbTotal, StackPanel receipt)
        {
            // instantiate classes
            DataAccess da = new DataAccess();
            Login login = new Login();

            // if a user is logged, alert user with a prompt and take 10% off the total price.
            if (login.isLoggedIn())
            {
                loginStatus.Text = da.getUser(login.userID()) + " is Logged in";
                total = (calculatePizza(Pizza.pizzaList) + calculateWings(Wings.wingsList)) * 0.9;
                TbDiscount.Text = "Member Discount applied!! 10% off your total order!";
            }
            else // if user is not logged in, total is calculated as normal
            {
                total = (calculatePizza(Pizza.pizzaList) + calculateWings(Wings.wingsList));
                loginStatus.Text = "Not logged in";
            }
            TbTotal.Text = "Order Total: $" + total.ToString();
            // clears the previous cart before attempting to create new list of receipt
            receipt.Children.Clear();
            // gets pizza object for each pizza in the pizzalist
            foreach (GetPizza pizza in Pizza.pizzaList)
            {
                // create a textblock object each time
                TextBlock tb = new TextBlock();
                // set textblock to the name, size and price from GetPizza 
                tb.Text = pizza.name.ToString() + " " + pizza.size.ToString() + " $" + pizza.price;
                // add the textblock to stackpanel 
                receipt.Children.Add(tb);
            }
            foreach (GetWings wings in Wings.wingsList)
            {
                TextBlock tb = new TextBlock();
                tb.Text = wings.name.ToString() + " " + wings.size.ToString() + " $" + wings.price;
                receipt.Children.Add(tb);
            }
        }

        // calls the getHistory method in data access class to grab history
        public void purchaseHistory(TextBox history)
        {
            DataAccess da = new DataAccess();
            Login login = new Login();
            // checks if user is logged in
            if (login.isLoggedIn())
            {
                history.Text = da.getHistoryByUser(da.getUser(login.userID()));
            }
            
        }

        // calculates pizza price
        public double calculatePizza(List<GetPizza> list)
        {
            double total = 0;
            foreach (GetPizza pizza in list)
            {
                total += pizza.price;
            }
            return Math.Round(total, 2);
        }

        // calculates wings price
        public double calculateWings(List<GetWings> list)
        {
            double total = 0;
            foreach (GetWings wings in list)
            {
                total += wings.price;
            }

            return Math.Round(total, 2);
        }

        // returns total price to be used in checkout class
        public double getTotal()
        {
            return total;
        }
    }
}
