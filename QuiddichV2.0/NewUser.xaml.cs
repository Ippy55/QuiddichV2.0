using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Interaction logic for NewUser.xaml
    /// </summary>
    public partial class NewUser : Page
    {
        public User user;
        Login login = new Login();

        public NewUser()
        {
            InitializeComponent();
        }

        private void btnCreateUser_Click(object sender, RoutedEventArgs e)
        {
            user = new User();
            this.PutUserData(user);
            try
            {
                if(isValidData())
                {
                    QuiddichDB.AddUser(user);
                    NavigationService.Navigate(login);
                    MessageBox.Show("User " + txtUsername.Text + " created sucessfully!", "User Created");
                }
                else if(txtUsername.Text == "")
                {
                    MessageBox.Show("Please enter a username.", "Invalid Username", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if(txtPassword.Password == "")
                {
                    MessageBox.Show("Please enter a password.", "Invalid Password", MessageBoxButton.OK,MessageBoxImage.Error);
                }
                else if(txtPassword.Password != txtConfirmPassword.Password)
                {
                    MessageBox.Show("Passwords must match.", "Passwords Not Matching", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                
            }
            catch(SqlException ex)
            {
                if (ex.Number == 2627)
                {
                    MessageBox.Show("User already exists in database.", 
                        "Duplicate User", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            
            NavigationService.Navigate(login);
            
        }

        private void PutUserData(User user)
        {
            user.Username = txtUsername.Text;
            user.Password = txtPassword.Password;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            String password = txtPassword.Password;
            PasswordScore passwordStrengthScore = PasswordAdvisor.CheckStrength(password);

            switch (passwordStrengthScore)
            {
                case PasswordScore.Blank:
                    break;
                case PasswordScore.VeryWeak:
                    lblPasswordStrength.Content = "Very Weak";
                    break;
                case PasswordScore.Weak:
                    lblPasswordStrength.Content = "Weak";
                    break;
                case PasswordScore.Medium:
                    lblPasswordStrength.Content = "Medium";
                    break;
                case PasswordScore.Strong:
                    lblPasswordStrength.Content = "Strong";
                    break;
                case PasswordScore.VeryStrong:
                    lblPasswordStrength.Content = "Very Strong";
                    break;
            }
        }

        private bool isValidData()
        {
            bool valid = false;

            if(txtUsername.Text != ""  
              && txtPassword.Password == txtConfirmPassword.Password
              && txtPassword.Password != "")
            {
                valid = true;
            }

            return valid;

        }
        
    }
}
