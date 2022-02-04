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
    public sealed partial class Wings : Page
    {
        public static List<GetWings> wingsList = new List<GetWings>();

        public Wings()
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

        // adds the selected wing and size to wingslist
        private void BtnAddBBQToCart(object sender, RoutedEventArgs e)
        {
            DataAccess da = new DataAccess();
            // checks if user has selected a value from combobox before attempting to add to list
            if (CbBbq.SelectedIndex != -1)
                // adds wing name, size and price to list of wings 
                addWings(BtnBBQ.Tag.ToString(), CbBbq.SelectedItem.ToString(),
                da.getWingsPrice(BtnBBQ.Tag.ToString(), CbBbq.SelectedItem.ToString()));
        }

        private void BtnAddHGToCart(object sender, RoutedEventArgs e)
        {
            DataAccess da = new DataAccess();
            if (CbHG.SelectedIndex != -1)
                addWings(BtnHG.Tag.ToString(), CbHG.SelectedItem.ToString(),
                da.getWingsPrice(BtnHG.Tag.ToString(), CbHG.SelectedItem.ToString()));
        }

        private void BtnAddTWToCart(object sender, RoutedEventArgs e)
        {
            DataAccess da = new DataAccess();
            if (CbTW.SelectedIndex != -1)
                addWings(BtnTW.Tag.ToString(), CbTW.SelectedItem.ToString(),
                da.getWingsPrice(BtnTW.Tag.ToString(), CbTW.SelectedItem.ToString()));
        }

        private void BtnAddBWToCart(object sender, RoutedEventArgs e)
        {
            DataAccess da = new DataAccess();
            if (CbBW.SelectedIndex != -1)
                addWings(BtnBW.Tag.ToString(), CbBW.SelectedItem.ToString(),
                da.getWingsPrice(BtnBW.Tag.ToString(), CbBW.SelectedItem.ToString()));
        }

        private void BtnAddCajunToCart(object sender, RoutedEventArgs e)
        {
            DataAccess da = new DataAccess();
            if (CbCajun.SelectedIndex != -1)
                addWings(BtnCajun.Tag.ToString(), CbCajun.SelectedItem.ToString(),
                da.getWingsPrice(BtnCajun.Tag.ToString(), CbCajun.SelectedItem.ToString()));
        }

        private void BtnAddSnPToCart(object sender, RoutedEventArgs e)
        {
            DataAccess da = new DataAccess();
            if (CbSnP.SelectedIndex != -1)
                addWings(BtnSnP.Tag.ToString(), CbSnP.SelectedItem.ToString(),
                da.getWingsPrice(BtnSnP.Tag.ToString(), CbSnP.SelectedItem.ToString()));
        }

        private void BtnAddJWToCart(object sender, RoutedEventArgs e)
        {
            DataAccess da = new DataAccess();
            if (CbJW.SelectedIndex != -1)
                addWings(BtnJW.Tag.ToString(), CbJW.SelectedItem.ToString(),
                da.getWingsPrice(BtnJW.Tag.ToString(), CbJW.SelectedItem.ToString()));
        }

        private void BtnAddLPWToCart(object sender, RoutedEventArgs e)
        {
            DataAccess da = new DataAccess();
            if (CbLPW.SelectedIndex != -1)
                addWings(BtnLPW.Tag.ToString(), CbLPW.SelectedItem.ToString(),
                da.getWingsPrice(BtnLPW.Tag.ToString(), CbLPW.SelectedItem.ToString()));
        }

        // passes in name, size and price to be added to the wingslist
        private void addWings(string name, string size, double price)
        {
            GetWings wings = new GetWings();
            wings.name = name;
            wings.size = size;
            wings.price = price;
            wingsList.Add(wings);
        }

        // handles the suggestion button to send feedback into the database
        private void Btn_Suggestion(object sender, RoutedEventArgs e)
        {
            DataAccess da = new DataAccess();
            // textbox cannot be empty or null
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

        // handles the checkout button in cart flyout
        private void BtnCheckout(object sender, RoutedEventArgs e)
        {
            Checkout checkout = new Checkout();
            checkout.goCheckout();
        }
    }
}
