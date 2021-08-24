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
    /// Interaction logic for ReporterMenu.xaml
    /// </summary>
    public partial class ReporterMenu : Page
    { 

        public ReporterMenu()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
           Login login = new Login();
           NavigationService.Navigate(login);
        }

        private void btnManage_Click(object sender, RoutedEventArgs e)
        {
            ManageTeams manageTeams = new ManageTeams();
            NavigationService.Navigate(manageTeams);
        }

        private void btnReportGame_Click(object sender, RoutedEventArgs e)
        {
            SelectTeams selectTeams = new SelectTeams();
            NavigationService.Navigate(selectTeams);
        }
    }
}
