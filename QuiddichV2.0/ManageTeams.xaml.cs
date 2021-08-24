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
    /// Interaction logic for ManageTeams.xaml
    /// </summary>
    public partial class ManageTeams : Page
    {
        private List<Team> teams = null;

        public ManageTeams()
        {
            InitializeComponent();
            teams = TeamDB.GetTeams();
            fillListOfTeams();
        }

        private void fillListOfTeams()
        {
            lstTeams.Items.Clear();

            foreach (Team t in teams)
            {
                lstTeams.Items.Add(t.GetDisplayText("\t"));
            }
        }

        private void btnAddTeam_Click(object sender, RoutedEventArgs e)
        {
            AddTeam addTeam = new AddTeam();
            addTeam.addTeam = true;
            NavigationService.Navigate(addTeam);
        }

       

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            ReporterMenu reporterMenu = new ReporterMenu();
            NavigationService.Navigate(reporterMenu);
        }

        private void btnModifyTeam_Click(object sender, RoutedEventArgs e)
        {
            int i = lstTeams.SelectedIndex;

            if (teamSelected())
            {
                ModifyTeam mod = new ModifyTeam(teams[i].TeamID);
                NavigationService.Navigate(mod);
            }
            else
            {
                MessageBox.Show("Please select a team.", "No Team Selected",
                                 MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private bool teamSelected()
        {
            bool valid = false;

            if (lstTeams.SelectedIndex >= 0)
            {
                valid = true;
            }

            return valid;
        }

        private void btnDeleteTeam_Click(object sender, RoutedEventArgs e)
        {
            int i = lstTeams.SelectedIndex;
            if (i != -1)
            {
                Team team = (Team)teams[i];
                string message = "Are you sure you want to delete the "
                    + team.TeamName.Trim() + "?";
                MessageBoxResult button =
                    MessageBox.Show(message, "Confirm Delete",
                    MessageBoxButton.YesNo);
                if (button == MessageBoxResult.Yes)
                {
                    teams.Remove(team);
                    QuiddichDB.DeleteTeam(team);
                    fillListOfTeams();
                }
            }

        }

        private void btnViewRoster_Click(object sender, RoutedEventArgs e)
        {
            int i = lstTeams.SelectedIndex;

            if (teamSelected())
            {
                PlayerRoster roster = new PlayerRoster(teams[i].TeamID);
                NavigationService.Navigate(roster);
            }
            else
            {
                MessageBox.Show("Please select a team.", "No Team Selected",
                                 MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void lstTeams_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int i = lstTeams.SelectedIndex;

            if(teams[i].getPlayers(teams[i].TeamID).Count < 4)
            {
                lblNumberPlayers.Content = "This team cannot play until\n it has four or more " +
                                           "players.";
            }
            else
            {
                lblNumberPlayers.Content = "";
            }
        }
    }
}
