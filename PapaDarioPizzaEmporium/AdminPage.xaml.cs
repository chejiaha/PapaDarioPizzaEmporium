using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace PapaDarioPizzaEmporium
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AdminPage : Page
    {

        public AdminPage()
        {
            this.InitializeComponent();
            // initialize the combobox to modify pizza on load
            CmbPizza();
            CmbSize();
        }
        // each method send the user that page by creating an instance of the class and passing the content.
        private void toCart(object sender, TappedRoutedEventArgs e)
        {
            InCart cart = new InCart();
            // displays the cart flyout
            cart.getCart(loginStatus, TbDiscount, TbTotal, receipt);
            // displays user purchase history 
            cart.purchaseHistory(TxHistory);
        }

        private void toLogin(object sender, TappedRoutedEventArgs e)
        {
            Login login = new Login();
            this.Content = login;
        }

        private void toAboutUs(object sender, TappedRoutedEventArgs e)
        {
            AboutUs aboutUs = new AboutUs();
            this.Content = aboutUs;
        }

        private void toPizza(object sender, TappedRoutedEventArgs e)
        {
            Pizza pizza = new Pizza();
            this.Content = pizza;
        }

        private void toWings(object sender, TappedRoutedEventArgs e)
        {
            Wings wings = new Wings();
            this.Content = wings;
        }

        private void toHomePage(object sender, TappedRoutedEventArgs e)
        {
            MainPage mainPage = new MainPage();
            this.Content = mainPage;
        }

        private void BtnCheckout(object sender, RoutedEventArgs e)
        {
            Checkout checkout = new Checkout();
            checkout.goCheckout();
        }

        private void BtnShowSuggestions(object sender, RoutedEventArgs e)
        {
            DataAccess da = new DataAccess();
            TxSuggestionReport.Text = da.getAllSuggestion();
        }

        // loads the combobox that contains all pizzas in database 
        private void CmbPizza()
        {
            DataAccess da = new DataAccess();
            // create 2 lists 
            List<string> pizzaList = new List<string>();
            List<string> pizzaTableList = da.GetProductByName("PizzaName");

            // iterate through the list of pizzas from database and store the unique values into a new list
            for (int i = 0; i < pizzaTableList.Count; i++)
            {
                // if the new list does not contain the next value in database list, add it 
                if (!pizzaList.Contains(pizzaTableList[i]))
                    pizzaList.Add(pizzaTableList[i]);
            }
            // set the combobox values to the items in pizzalist
            modifyPizza.ItemsSource = pizzaList;
        }

        // loads the combobox that contains all pizza sizes in database 
        private void CmbSize()
        {
            DataAccess da = new DataAccess();
            List<string> sizeList = new List<string>();
            List<string> pizzaTableList = da.GetProductByName("Size");

            for (int i = 0; i < pizzaTableList.Count; i++)
            {
                if (!sizeList.Contains(pizzaTableList[i]))
                    sizeList.Add(pizzaTableList[i]);
            }
            modifyPrice.ItemsSource = sizeList;
        }

        // modifies the price in pizza database with price user entered 
        private void BtnModifyPizzaPrice(object sender, RoutedEventArgs e)
        {
            DataAccess da = new DataAccess();
            double newPrice;
            // gets values to determine which pizza admin has selected to modify from combobox
            string pizza = modifyPizza.SelectedItem.ToString();
            string size = modifyPrice.SelectedItem.ToString();
            int id = da.getPizzaID(pizza, size);
            // checks if admin has entered an appropriate value before trying to store into database
            if (Double.TryParse(TxPrice.Text, out newPrice))
            {
                // updates the product price with price admin set then prompts admin task is complete
                newPrice = Convert.ToDouble(TxPrice.Text);
                da.UpdateProduct(id, pizza, size, newPrice);
                TbSuccess.Text = pizza + " " + size + " " + "price changed successfully";
            }
        }

        // deletes the pizza and size that user has selected from combobox
        private void BtnDeletePizza(object sender, RoutedEventArgs e)
        {
            DataAccess da = new DataAccess();
            string pizza = modifyPizza.SelectedItem.ToString();
            string size = modifyPrice.SelectedItem.ToString();
            int id = da.getPizzaID(pizza, size);
            da.DeleteProduct(id);
            TbSuccess.Text = pizza + " " + size + " " + "deleted successfully";
        }
    }
}
