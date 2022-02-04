using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Linq;
using System.Data;
using Microsoft.VisualBasic;
using System.Dynamic;
using Windows.UI.Xaml.Controls;
using Windows.ApplicationModel.VoiceCommands;
using Windows.ApplicationModel.Core;

namespace PapaDarioPizzaEmporium
{
    public class DataAccess
    {
        // declaring data variables for each table in database
        private SqlConnection _conn;
        private SqlDataAdapter _adapter;
        private SqlCommandBuilder _cmdBuilder;
        private DataSet _dataSet;
        private DataTable Customers;

        private SqlConnection _connPizza;
        private SqlDataAdapter _adapterPizza;
        private SqlCommandBuilder _cmdBuilderPizza;
        private DataSet _dataSetPizza;
        private DataTable PizzaTable;

        private SqlConnection _connWings;
        private SqlDataAdapter _adapterWings;
        private SqlCommandBuilder _cmdBuilderWings;
        private DataSet _dataSetWings;
        private DataTable WingsTable;

        private SqlConnection _connReceipt;
        private SqlDataAdapter _adapterReceipt;
        private SqlCommandBuilder _cmdBuilderReceipt;
        private DataSet _dataSetReceipt;
        private DataTable ReceiptTable;

        private SqlConnection _connSuggestion;
        private SqlDataAdapter _adapterSuggestion;
        private SqlCommandBuilder _cmdBuilderSuggestion;
        private DataSet _dataSetSuggestion;
        private DataTable SuggestionTable;

        private SqlConnection _connAdmin;
        private SqlDataAdapter _adapterAdmin;
        private SqlCommandBuilder _cmdBuilderAdmin;
        private DataSet _dataSetAdmin;
        private DataTable AdminTable;

        // constructor
        public DataAccess()
        {
            // name of database
            string cs = GetConnectionString("DarioPizzaDB");

            // SQL DQL query to select columns from table
            string query = "Select custID, username, custPassword, email, custAddress from Customers";
            _conn = new SqlConnection(cs); // establishes connection
            _adapter = new SqlDataAdapter(query, _conn); // runs query inside sql connection
            _cmdBuilder = new SqlCommandBuilder(_adapter); // builds teh command
            FillDataSet(); // updates table

            string queryPizzaTable = "Select PizzaID, PizzaName, Size, Price from Pizza";
            _connPizza = new SqlConnection(cs);
            _adapterPizza = new SqlDataAdapter(queryPizzaTable, _connPizza);
            _cmdBuilderPizza = new SqlCommandBuilder(_adapterPizza);
            FillDataSetPizza();

            string queryWingsTable = "Select WingsID, WingsName, Size, Price from Wings";
            _connWings = new SqlConnection(cs);
            _adapterWings = new SqlDataAdapter(queryWingsTable, _connWings);
            _cmdBuilderWings = new SqlCommandBuilder(_adapterWings);
            FillDataSetWings();

            string queryReceiptTable = "Select ReceiptID, CustID, Username, Email, CustAddress, OrderSummary, Discount, TotalPrice from Receipt";
            _connReceipt = new SqlConnection(cs);
            _adapterReceipt = new SqlDataAdapter(queryReceiptTable, _connReceipt);
            _cmdBuilderReceipt = new SqlCommandBuilder(_adapterReceipt);
            FillDataSetReceipt();

            string querySuggestionTable = "Select SuggestionID, Suggestion, SuggestionDate from Suggestion";
            _connSuggestion = new SqlConnection(cs);
            _adapterSuggestion = new SqlDataAdapter(querySuggestionTable, _connSuggestion);
            _cmdBuilderSuggestion = new SqlCommandBuilder(_adapterSuggestion);
            FillDataSetSuggestion();

            string queryAdminTable = "Select AdminID, Username, AdminPassword from AdminTable";
            _connAdmin = new SqlConnection(cs);
            _adapterAdmin = new SqlDataAdapter(queryAdminTable, _connAdmin);
            _cmdBuilderAdmin = new SqlCommandBuilder(_adapterAdmin);
            FillDataSetAdmin();
        }

        // method to grab connection string from json 
        static string GetConnectionString(string connectionStringName)
        {
            // passes connection type into builder object
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            // builder sets the path of directory
            configurationBuilder.SetBasePath(Directory.GetCurrentDirectory());
            // adds json file to builder object
            configurationBuilder.AddJsonFile("config.json");
            // builds the connection string
            IConfiguration config = configurationBuilder.Build();
            // return connection string
            return config["ConnectionStrings:" + connectionStringName];
        }

        // fills dataset for different tables in database
        public void FillDataSet()
        {
            //resets the data set
            _dataSet = new DataSet();
            _adapter.Fill(_dataSet);
            Customers = _dataSet.Tables[0];

            //define primary key
            DataColumn[] pk = new DataColumn[1];
            pk[0] = Customers.Columns["custID"];
            Customers.PrimaryKey = pk;
        }

        public void FillDataSetPizza()
        {
            _dataSetPizza = new DataSet();
            _adapterPizza.Fill(_dataSetPizza);
            PizzaTable = _dataSetPizza.Tables[0];

            DataColumn[] pkPizza = new DataColumn[1];
            pkPizza[0] = PizzaTable.Columns["PizzaID"];
            PizzaTable.PrimaryKey = pkPizza;
        }

        public void FillDataSetWings()
        {
            _dataSetWings = new DataSet();
            _adapterWings.Fill(_dataSetWings);
            WingsTable = _dataSetWings.Tables[0];

            DataColumn[] pkWings = new DataColumn[1];
            pkWings[0] = WingsTable.Columns["WingsID"];
            WingsTable.PrimaryKey = pkWings;
        }

        public void FillDataSetReceipt()
        {
            _dataSetReceipt = new DataSet();
            _adapterReceipt.Fill(_dataSetReceipt);
            ReceiptTable = _dataSetReceipt.Tables[0];

            DataColumn[] pkReceipt = new DataColumn[1];
            pkReceipt[0] = ReceiptTable.Columns["ReceiptID"];
            ReceiptTable.PrimaryKey = pkReceipt;
        }

        public void FillDataSetSuggestion()
        {
            _dataSetSuggestion = new DataSet();
            _adapterSuggestion.Fill(_dataSetSuggestion);
            SuggestionTable = _dataSetSuggestion.Tables[0];

            DataColumn[] pkSuggestion = new DataColumn[1];
            pkSuggestion[0] = SuggestionTable.Columns["SuggestionID"];
            SuggestionTable.PrimaryKey = pkSuggestion;
        }

        public void FillDataSetAdmin()
        {
            _dataSetAdmin = new DataSet();
            _adapterAdmin.Fill(_dataSetAdmin);
            AdminTable = _dataSetAdmin.Tables[0];

            DataColumn[] pkAdmin = new DataColumn[1];
            pkAdmin[0] = AdminTable.Columns["AdminID"];
            AdminTable.PrimaryKey = pkAdmin;
        }

        //CREATE 
        // adds a new customers to database when user creates an account
        public void InsertCustomer(string username, string password, string email, string address)
        {
            //Create a new row
            DataRow newRow = Customers.NewRow();

            //column
            newRow["custID"] = 0; //this value will be overwritten by sql server anyways
            newRow["username"] = username;
            newRow["custPassword"] = password;
            newRow["email"] = email;
            newRow["custaddress"] = address;

            //add the new rows into the collection
            Customers.Rows.Add(newRow);

            //adapter object communicates between your app and the db
            //read the INSERTQuery fromt he SqlCommandBuilderObject
            _adapter.InsertCommand = _cmdBuilder.GetInsertCommand();
            _adapter.Update(Customers); //saving the changes to the database.

            FillDataSet(); //refreshes/updates the dataset
        }

        // adds the user order to receipt after user clicks checkout
        public void addReceipt(int custID, string username, string email, string address, string order, double discount, double totalPrice)
        {
            DataRow newRow = ReceiptTable.NewRow();

            newRow["ReceiptID"] = 0;
            newRow["CustID"] = custID;
            newRow["Username"] = username;
            newRow["Email"] = email;
            newRow["CustAddress"] = address;
            newRow["OrderSummary"] = order;
            newRow["Discount"] = discount;
            newRow["TotalPrice"] = totalPrice;

            ReceiptTable.Rows.Add(newRow);
            _adapterReceipt.InsertCommand = _cmdBuilderReceipt.GetInsertCommand();
            _adapterReceipt.Update(ReceiptTable);
            FillDataSetReceipt();
        }

        // adds user suggestion to table
        public void addSuggestion(string suggestion)
        {
            DataRow newRow = SuggestionTable.NewRow();

            // sets the new row values based on the column names
            newRow["SuggestionID"] = 0;
            newRow["Suggestion"] = suggestion;
            newRow["SuggestionDate"] = DateTime.Now.ToString("yyyy-MM-dd");

            SuggestionTable.Rows.Add(newRow);
            _adapterSuggestion.InsertCommand = _cmdBuilderSuggestion.GetInsertCommand();
            _adapterSuggestion.Update(SuggestionTable);
            FillDataSetSuggestion();
        }

        //finding a item based on product name
        public List<string> GetProductByName(string name)
        {
            List<string> product = new List<string>();
            foreach (DataRow col in PizzaTable.Rows)
            {
                product.Add(col[name].ToString());
            }
            return product;
        }

        //updates product in pizza table
        public void UpdateProduct(int id, string pizza, string size, double price)
        {
            //find a row based on its primary key
            DataRow row = PizzaTable.Rows.Find(id);
            if (row != null)
            {
                row["PizzaName"] = pizza;
                row["Size"] = size;
                row["Price"] = price;
                _adapterPizza.UpdateCommand = _cmdBuilderPizza.GetUpdateCommand();
                _adapterPizza.Update(PizzaTable);
                FillDataSetPizza();
            }
        }

        //deletes a product from pizza table
        public void DeleteProduct(int id)
        {
            DataRow row = PizzaTable.Rows.Find(id);
            if (row != null)
            {
                row.Delete();
                _adapterPizza.DeleteCommand = _cmdBuilderPizza.GetDeleteCommand();
                _adapterPizza.Update(PizzaTable);
                FillDataSetPizza();
            }
        }

        // returns all rows based on table name
        public string getAllSuggestion()
        {
            string suggestion = "";
            foreach (DataRow col in SuggestionTable.Rows)
            {
                // concatenats suggestion and suggestion date to 1 string
                suggestion += col["Suggestion"].ToString() + " " + col["SuggestionDate"].ToString() + "\n";
            }
            return suggestion;
        }

        // retreives user's history based on username
        public string getHistoryByUser(string username)
        {
            string query = "Username = '" + username + "'";
            DataRow[] row = ReceiptTable.Select(query);
            string product = "";
            // checks if user has any stored receipt in the receipt table before attempting to extract information
            if (row.Length >= 1)
            {
                foreach (DataRow col in ReceiptTable.Rows)
                {
                    // if username matches in the receipt username column
                    if (row[0][2].Equals(username))
                        // then store found previous orders into string product
                        product += "order: " + col["OrderSummary"].ToString() + "\n";
                }
            }
            return product;
        }

        // return pizza priced based on name and size
        public double getPizzaPrice(string pizza, string size)
        {
            string query = "PizzaName = '" + pizza + "' AND Size = '" + size + "'";
            DataRow[] row = PizzaTable.Select(query);
            return Convert.ToDouble(row[0][3]);
        }
        // return wings price based on name and size
        public double getWingsPrice(string wings, string size)
        {
            string query = "WingsName = '" + wings + "' AND Size = '" + size + "'";
            DataRow[] row = WingsTable.Select(query);
            return Convert.ToDouble(row[0][3]);
        }

        // returns pizza ID based on name and size
        public int getPizzaID(string pizza, string size)
        {
            string query = "PizzaName = '" + pizza + "' AND Size = '" + size + "'";
            DataRow[] row = PizzaTable.Select(query);
            return Convert.ToInt32(row[0][0]);
        }

        // returns the user's password based on username
        public string getPassword(string username)
        {
            string query = "Username = '" + username + "'";
            DataRow[] row = Customers.Select(query);
            // checks if user is found in column username
            if (row.Length > 0)
                return row[0][2].ToString();
            else
                return "F";
        }

        // returns the admin's password based on username
        public string getPasswordAdmin(string username)
        {
            string query = "Username = '" + username + "'";
            DataRow[] row = AdminTable.Select(query);
            if (row.Length > 0)
                return row[0][2].ToString();
            else
                return "F";
        }

        // returns admin ID based on admin username
        public int getAdminID(string username)
        {
            string query = "Username='" + username + "'";
            DataRow[] row = AdminTable.Select(query);
            return Convert.ToInt32(row[0][0].ToString());
        }

        // returns admin name based on ID
        public string getAdmin(int id)
        {
            string query = "AdminID='" + id + "'";
            DataRow[] row = AdminTable.Select(query);
            return row[0][1].ToString();
        }

        // return customer ID based on username
        public int getID(string username)
        {
            string query = "username='" + username + "'";
            DataRow[] row = Customers.Select(query);
            return Convert.ToInt32(row[0][0].ToString());
        }

        // returns username based on ID
        public string getUser(int id)
        {
            string query = "custID='" + id + "'";
            DataRow[] row = Customers.Select(query);
            return row[0][1].ToString();
        }

        // returns uer's email based on ID
        public string getEmail(int id)
        {
            string query = "CustID='" + id + "'";
            DataRow[] row = Customers.Select(query);
            return row[0][3].ToString();
        }

        // returns user address based on ID
        public string getAddress(int id)
        {
            string query = "CustID='" + id + "'";
            DataRow[] row = Customers.Select(query);
            return row[0][4].ToString();
        }

        // checks if the credentials entered match to customer table 
        public bool isUser(string username, string password)
        {
            string query = "username = '" + username + "'";
            DataRow[] user = Customers.Select(query);
            // if length is less than 1, username does not exist in table
            if (user.Length < 1)
            {
                return false;
            }
            else if (user[0][2].Equals(password))
            {
                return true;
            }
            return false;
        }

        // check if the credentials entered match to admin table
        public bool isAdmin(string username, string password)
        {
            string query = "username = '" + username + "'";
            DataRow[] user = AdminTable.Select(query);
            if (user.Length < 1)
            {
                return false;
            }
            else if (user[0][2].Equals(password))
            {
                return true;
            }
            return false;
        }
    }
}
