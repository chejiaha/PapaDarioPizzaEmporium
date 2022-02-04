using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
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
    public sealed partial class Login : Page
    {
        // declaring variables and setting flags
        private static bool isUser = false;
        private static bool isAdmin = false;
        private static int id = 0;
        private static int adminID = 0;
        public Login()
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

        private void creatUser(object sender, PointerRoutedEventArgs e)
        {
            CreateUser createUser = new CreateUser();
            this.Content = createUser;
        }

        // handles the login button. Checks if user has entered a valid login credential
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            DataAccess da = new DataAccess();
            // grabs username and password from their respective textboxes
            string username = Tbusername.Text;
            string password = Tbpassword.Password.ToString();
            // checks if credentials entered by the user matches one in database
            if (da.isUser(username, password))
            {
                loginStatus.Text = "Login Successful";
                // sets id to the user ID in database
                // sets flags to true
                id = da.getID(username);
                isUser = true;
                isAdmin = false;
            }
            // checks if credentials match that of admin 
            else if (da.isAdmin(username, password))
            {
                loginStatus.Text = "Admin Login Successful";
                adminID = da.getAdminID(username);
                isAdmin = true;
                isUser = false;
            }
            else
            {
                // if nothing matches, then set flags to false
                loginStatus.Text = "Incorrect username or password";
                isAdmin = false;
                isUser = false;
            }
        }
        private void BtnCheckout(object sender, RoutedEventArgs e)
        {
            Checkout checkout = new Checkout();
            checkout.goCheckout();
        }
        // button to display the password for the username entered if it is found in database
        private void BtnForgotPass(object sender, RoutedEventArgs e)
        {
            DataAccess da = new DataAccess();
            if(!da.getPassword(Tbusername.Text).Equals("F"))
            loginStatus.Text = da.getPassword(Tbusername.Text);
            if (!da.getPasswordAdmin(Tbusername.Text).Equals("F"))
                loginStatus.Text = da.getPasswordAdmin(Tbusername.Text);
        }

        public bool isLoggedIn()
        {
            return isUser;
        }

        public int userID()
        {
            return id;
        }

        public bool isAdminLoggedIn()
        {
            return isAdmin;
        }

        public int AdminID()
        {
            return adminID;
        }
    }
}
