using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuiddichV2._0
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {

        public Login()
        {
            InitializeComponent();
        }

        private User user = null;
        private ReporterMenu reporterMenu = new ReporterMenu();

        private void btnNewUser_Click(object sender, RoutedEventArgs e)
        {
            var newUser = new NewUser();
            NavigationService.Navigate(newUser);
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Validator.IsPresent(txtUsername) && Validator.IsPresent(txtPassword))
                {
                    string username = txtUsername.Text;
                    string password = txtPassword.Password;
                    this.GetUser(username, password);
                    if (user == null)
                    {
                        MessageBox.Show("No user found with these credentials. " +
                             "Please try again.", "User Not Found", MessageBoxButton.OK, MessageBoxImage.Error );
                    }
                    else
                    {
                        NavigationService.Navigate(reporterMenu);
                        MessageBox.Show("Login Sucessful!\n" + "Welcome " + username + "!", "Welcome!");
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        private void GetUser(string username, string password)
        {
            try
            {
                user = QuiddichDB.GetUser(username, password);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        private void btnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (Validator.IsPresent(txtUsername) &&
               Validator.IsPresent(txtPassword))
            {
                user = new User();
                user.Username = txtUsername.Text;
                user.Password = txtPassword.Password;
                MessageBoxResult result = MessageBox.Show("Delete " + user.Username + "?",
                "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        if (!QuiddichDB.DeleteUser(user))
                        {
                            MessageBox.Show("Another user has updated or deleted " +
                                "that player.", "Database Error");
                            this.GetUser(user.Username, user.Password);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, ex.GetType().ToString());
                    }
                    finally
                    {
                        txtUsername.Text = "";
                        txtPassword.Password = "";
                    }
                }
            }
        }

    }
}
