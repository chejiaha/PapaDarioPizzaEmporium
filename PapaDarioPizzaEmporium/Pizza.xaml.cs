using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Chat;
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
    public sealed partial class Pizza : Page
    {
        public static List<GetPizza> pizzaList = new List<GetPizza>();

        public Pizza()
        {
            this.InitializeComponent();
        }

        private void toCart(object sender, TappedRoutedEventArgs e)
        {
            InCart cart = new InCart();
            cart.getCart(loginStatus, TbDiscount, TbTotal, receipt);
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

        // adds the selected pizza and size to pizzalist
        private void BtnAddCPToCart(object sender, RoutedEventArgs e)
        {
            // checks if user has selected a value from combobox before attempting to add to list
            DataAccess da = new DataAccess();
            if (CBcheesePizza.SelectedIndex != -1)
                // adds pizza name, size and price to list of pizza 
                addPizza(BtnCP.Tag.ToString(), CBcheesePizza.SelectedItem.ToString(),
                da.getPizzaPrice(BtnCP.Tag.ToString(), CBcheesePizza.SelectedItem.ToString()));
        }

        private void BtnAddPPToCart(object sender, RoutedEventArgs e)
        {
            DataAccess da = new DataAccess();
            if (CBpepperoniPizza.SelectedIndex != -1)
                addPizza(BtnPP.Tag.ToString(), CBpepperoniPizza.SelectedItem.ToString(),
                da.getPizzaPrice(BtnPP.Tag.ToString(), CBpepperoniPizza.SelectedItem.ToString()));
        }

        private void BtnAddMPToCart(object sender, RoutedEventArgs e)
        {
            DataAccess da = new DataAccess();
            if (CBmeatloversPizza.SelectedIndex != -1)
                addPizza(BtnMP.Tag.ToString(), CBmeatloversPizza.SelectedItem.ToString(),
                da.getPizzaPrice(BtnMP.Tag.ToString(), CBmeatloversPizza.SelectedItem.ToString()));
        }

        private void BtnAddVPToCart(object sender, RoutedEventArgs e)
        {
            DataAccess da = new DataAccess();
            if (CBvegePizza.SelectedIndex != -1)
                addPizza(BtnVP.Tag.ToString(), CBvegePizza.SelectedItem.ToString(),
                da.getPizzaPrice(BtnVP.Tag.ToString(), CBvegePizza.SelectedItem.ToString()));
        }

        private void BtnAddDPToCart(object sender, RoutedEventArgs e)
        {
            DataAccess da = new DataAccess();
            if (CBdeluxePizza.SelectedIndex != -1)
                addPizza(BtnDP.Tag.ToString(), CBdeluxePizza.SelectedItem.ToString(),
                da.getPizzaPrice(BtnDP.Tag.ToString(), CBdeluxePizza.SelectedItem.ToString()));
        }

        private void BtnAddCANPToCart(object sender, RoutedEventArgs e)
        {
            DataAccess da = new DataAccess();
            if (CBcanadianPizza.SelectedIndex != -1)
                addPizza(BtnCANP.Tag.ToString(), CBcanadianPizza.SelectedItem.ToString(),
                da.getPizzaPrice(BtnCANP.Tag.ToString(), CBcanadianPizza.SelectedItem.ToString()));
        }

        private void BtnAddChkPToCart(object sender, RoutedEventArgs e)
        {
            DataAccess da = new DataAccess();
            if (CBchickenPizza.SelectedIndex != -1)
                addPizza(BtnChkP.Tag.ToString(), CBchickenPizza.SelectedItem.ToString(),
                da.getPizzaPrice(BtnChkP.Tag.ToString(), CBchickenPizza.SelectedItem.ToString()));
        }

        private void BtnAddStPToCart(object sender, RoutedEventArgs e)
        {
            DataAccess da = new DataAccess();
            if (CBsteakPizza.SelectedIndex != -1)
                addPizza(BtnStP.Tag.ToString(), CBsteakPizza.SelectedItem.ToString(),
                da.getPizzaPrice(BtnStP.Tag.ToString(), CBsteakPizza.SelectedItem.ToString()));
        }

        // handles the checkout button in cart flyout
        private void BtnCheckout(object sender, RoutedEventArgs e)
        {
            Checkout checkout = new Checkout();
            checkout.goCheckout();
        }
        // passes in name, size and price to be added to the wingslist
        private void addPizza(string name, string size, double price)
        {
            GetPizza pizza = new GetPizza();
            pizza.name = name;
            pizza.size = size;
            pizza.price = price;
            pizzaList.Add(pizza);
        }

        // handles the suggestion button to send feedback into the database
        private void Btn_Suggestion(object sender, RoutedEventArgs e)
        {
            DataAccess da = new DataAccess();
            if (!String.IsNullOrEmpty(TbSuggestion.Text))
            {
                da.addSuggestion(TbSuggestion.Text);
                TbConfirmSuggestion.Text = "Thank you, your suggestion has been submitted";
            }
            else
            {
                TbConfirmSuggestion.Text = "Suggestion is empty";
            }
        }

    }
}
