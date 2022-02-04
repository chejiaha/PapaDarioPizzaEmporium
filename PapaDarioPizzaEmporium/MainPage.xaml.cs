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
using System.Data.SqlClient;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace PapaDarioPizzaEmporium
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Login login = new Login();

        public MainPage()
        {
            this.InitializeComponent();
            // checks if admin is logged in, to set admin button to visible on homepage
            if (login.isAdminLoggedIn())
                BtnAdmin.Visibility = Visibility.Visible;
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

        private void toAdmin(object sender, TappedRoutedEventArgs e)
        {
            AdminPage adminPage = new AdminPage();
            this.Content = adminPage;
        }

        private void BtnCheckout(object sender, RoutedEventArgs e)
        {
            Checkout checkout = new Checkout();
            checkout.goCheckout();
        }
    }
}
