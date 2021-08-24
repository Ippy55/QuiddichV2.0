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
    /// Interaction logic for PlayerRoster.xaml
    /// </summary>
    public partial class PlayerRoster : Page
    {
        private List<Player> players = null;
        private int teamID;

        /// <summary>
        /// Default constructor
        /// </summary>
        public PlayerRoster()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Customized construtor
        /// </summary>
        /// <param name="teamID"></param>
        public PlayerRoster(int teamID)
        {
            InitializeComponent();
            players = PlayerDB.GetPlayers(teamID);
            this.teamID = teamID;
            fillListOfPlayers();
            
        }

        /// <summary>
        /// Fill the list box with players
        /// </summary>
        /// 
        private void fillListOfPlayers()
        {
            lstPlayers.Items.Clear();

            foreach (Player p in players)
            {
                lstPlayers.Items.Add(p.GetDisplayText("\t"));
            }
        }

        /// <summary>
        /// Opens a form to add a player
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void btnAddPlayer_Click(object sender, RoutedEventArgs e)
        {
            AddPlayer addplayer = new AddPlayer(teamID);
            addplayer.addPlayer = true;
            NavigationService.Navigate(addplayer);
            
        }

        /// <summary>
        /// Opens a form to modify a player
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void btnModifyPlayer_Click(object sender, RoutedEventArgs e)
        {
            if(playerSelected())
            {
                int i = lstPlayers.SelectedIndex;

                AddPlayer addplayer = new AddPlayer(players[i].TeamID,
                                                    players[i].FirstName,
                                                    players[i].LastName);
                addplayer.addPlayer = false;
                NavigationService.Navigate(addplayer);
            }
            else
            {
                MessageBox.Show("Please select a player.", "No Player Selected",
                                 MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private bool playerSelected()
        {
            bool valid = false;

            if (lstPlayers.SelectedIndex >= 0)
            {
                valid = true;
            }

            return valid;
        }

        private void btnDeletePlayer_Click(object sender, RoutedEventArgs e)
        {
            int i = lstPlayers.SelectedIndex;
            if (i != -1)
            {
                Player player = (Player)players[i];
                string message = "Are you sure you want to delete the player "
                    + player.FirstName.Trim() + " " + player.LastName.Trim() + "?";
                MessageBoxResult button =
                    MessageBox.Show(message, "Confirm Delete",
                    MessageBoxButton.YesNo);
                if (button == MessageBoxResult.Yes)
                {
                    players.Remove(player);
                    QuiddichDB.DeletePlayer(player);
                    fillListOfPlayers();
                }
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            ManageTeams manageTeams = new ManageTeams();
            NavigationService.Navigate(manageTeams);
        }
    }
}
