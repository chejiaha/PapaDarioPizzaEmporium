using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
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
    public sealed partial class CreateUser : Page
    {

        public CreateUser()
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
        // handles sign up button. checks if user has entered valid information using regex
        private void BtnSignUp_Click(object sender, RoutedEventArgs e)
        {
            //SETTING up the flags
            bool emailFlag = false;
            bool PasswordMatch = false;

            //getting all of the values
            string username = TbUsername.Text;
            string email = TbEmail.Text;
            string password = TbPassword.Password.ToString();
            string confirmPass = TbConfirmPass.Password.ToString();
            string address = TbAddress.Text;

            // validation
            txConn.Text = " ";

            //checking for the Email
            if (Regex.IsMatch(email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$") == false)
            {
                //if the emial is written wrong
                txConn.Text = "Your Email must be formatted like xyz@xyz.com ";
            }
            else
            {
                emailFlag = true;
            }

            //checking if the password matches
            if (password.Equals(confirmPass))
            {
                //if they password match.
                PasswordMatch = true;
            }
            else
            {
                txConn.Text = "The passwords do not match";
            }

            //if all are true add items into the database, and redirect to other page
            if ( emailFlag == true && PasswordMatch == true)
            {
                txConn.Text = "User created";
                //create new instance and add all items into the database
                DataAccess da = new DataAccess();
                da.InsertCustomer(username, password, email, address);
            }
        }

        private void BtnCheckout(object sender, RoutedEventArgs e)
        {
            Checkout checkout = new Checkout();
            checkout.goCheckout();
        }
    }
}
