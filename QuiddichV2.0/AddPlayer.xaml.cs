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
    /// Interaction logic for AddPlayer.xaml
    /// </summary>
    public partial class AddPlayer : Page
    {
        public Player player;
        public bool addPlayer;
        private int teamID;
        private string firstName;
        private string lastName;

        public AddPlayer()
        {
            InitializeComponent();
        }

        public AddPlayer(int teamID)
        {
            InitializeComponent();
            this.teamID = teamID;
        }

        public AddPlayer(int teamID, string firstName, string lastName)
        {
            InitializeComponent();
            this.teamID = teamID;
            this.firstName = firstName;
            this.lastName = lastName;
        }

        private void formLoaded(object sender, RoutedEventArgs e)
        {
            if(addPlayer)
            {
                txtTeamID.IsReadOnly = true;
                txtTeamID.Text = teamID.ToString();
            }
            else
            {
                this.GetPlayer(teamID, firstName, lastName);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidData())
            {
                if (addPlayer)
                {
                    
                    player = new Player();
                    this.PutPlayerData(player);
                    try
                    {
                        QuiddichDB.AddPlayer(player);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, ex.GetType().ToString());
                    }
                }
                else
                {
                    this.GetPlayer(teamID, firstName, lastName);
                    Player newPlayer = new Player();
                    newPlayer.TeamID = player.TeamID;
                    newPlayer.FirstName = player.FirstName;
                    newPlayer.LastName = player.LastName;
                    newPlayer.UniformNumber = player.UniformNumber;
                    newPlayer.Position = player.Position;
                    this.PutPlayerData(newPlayer);
                    try
                    {
                        if (!QuiddichDB.UpdatePlayer(player, newPlayer))
                        {
                            MessageBox.Show("Another user has updated or " +
                                "deleted that team.", "Database Error");
                        }
                        else
                        {
                            player = newPlayer;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, ex.GetType().ToString());
                    }
                }

                PlayerRoster roster = new PlayerRoster(teamID);
                NavigationService.Navigate(roster);

            }
        }

        private void PutPlayerData(Player player)
        {

            player.TeamID = Convert.ToInt32(txtTeamID.Text);
            player.FirstName = txtFirstName.Text;
            player.LastName = txtLastName.Text;
            player.UniformNumber = Convert.ToInt32(txtUniformNumber.Text);
            player.Position = txtPosition.Text;

        }

        private void GetPlayer(int teamID)
        {
            player = QuiddichDB.GetPlayer(teamID);
            txtTeamID.Text = player.TeamID.ToString();
            txtFirstName.Text = player.FirstName;
            txtLastName.Text = player.LastName;
            txtUniformNumber.Text = player.UniformNumber.ToString();
            txtPosition.Text = player.Position;

        }

        private void GetPlayer(int teamID, string firstName, string lastName)
        {
            player = QuiddichDB.GetPlayer(teamID, firstName, lastName);
            txtTeamID.Text = player.TeamID.ToString();
            txtFirstName.Text = player.FirstName;
            txtLastName.Text = player.LastName;
            txtUniformNumber.Text = player.UniformNumber.ToString();
            txtPosition.Text = player.Position;

        }

        private bool IsValidData()
        {
            return Validator.IsPresent(txtFirstName) &&
                   Validator.IsPresent(txtLastName) &&
                   Validator.IsPresent(txtUniformNumber) &&
                   Validator.IsInt32(txtUniformNumber) &&
                   Validator.IsPresent(txtPosition);

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            PlayerRoster roster = new PlayerRoster(teamID);
            NavigationService.Navigate(roster);
        }
    }
}
