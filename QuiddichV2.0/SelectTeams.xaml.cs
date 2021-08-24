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
    /// Interaction logic for SelectTeams.xaml
    /// </summary>
    public partial class SelectTeams : Page
    {
        private List<Team> teams = null;

        public SelectTeams()
        {
            InitializeComponent();
            fillComboBoxes();
        }

        public void fillComboBoxes()
        {
            teams = TeamDB.GetTeams();

            foreach (Team t in teams)
            {
                if(t.getPlayers(t.TeamID).Count < 4)
                {

                }
                else
                {
                    cboTeam1.Items.Add(t.TeamName);
                    cboTeam2.Items.Add(t.TeamName);
                }
                
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ReporterMenu reporterMenu = new ReporterMenu();
            NavigationService.Navigate(reporterMenu);
        }

        private void btnStartGame_Click(object sender, RoutedEventArgs e)
        {
            if (cboTeam1.SelectedIndex == -1 || cboTeam2.SelectedIndex == -1)
            {
                MessageBox.Show("Two teams must be selected.", "Invalid Team Selection", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (cboTeam1.SelectedIndex == cboTeam2.SelectedIndex)
            {
                MessageBox.Show("Teams cannot play themselves!", "Invalid Teams", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                int team1Index = cboTeam1.SelectedIndex;
                int team2Index = cboTeam2.SelectedIndex;

                PlayGame playGame = new PlayGame(teams[team1Index], teams[team2Index]);
                NavigationService.Navigate(playGame);
            }
        }
    }
}
