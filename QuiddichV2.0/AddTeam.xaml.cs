using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    /// Interaction logic for AddTeam.xaml
    /// </summary>
    public partial class AddTeam : Page
    {
        public Team team;
        public bool addTeam;

        public AddTeam()
        {
            InitializeComponent();
        }

        private void btnSaveTeam_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidData())
            {
                if (addTeam)
                {
                    team = new Team();
                    this.PutTeamData(team);
                    try
                    {
                        QuiddichDB.AddTeam(team);

                        MessageBoxResult result = MessageBox.Show("In order to play a game, " + team.TeamName.Trim() + " must have at least four players."
                                                                 + "\nDo you wish to add players now?", "Add Players?", MessageBoxButton.YesNo,
                                                                   MessageBoxImage.Question);
                        switch(result)
                        {
                            case MessageBoxResult.Yes:
                                AddPlayer addPlayer = new AddPlayer(team.TeamID);
                                addPlayer.addPlayer = true;
                                NavigationService.Navigate(addPlayer);
                                break;

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, ex.GetType().ToString());
                    }
                }
                else
                {
                    Team newTeam = new Team();
                    newTeam.TeamID = team.TeamID;
                    newTeam.TeamName = team.TeamName;
                    newTeam.AbbrName = team.AbbrName;
                    newTeam.City = team.City;
                    newTeam.State = team.State;
                    newTeam.ZipCode = team.ZipCode;

                    this.PutTeamData(newTeam);
                    try
                    {
                        if (!QuiddichDB.UpdateTeam(team, newTeam))
                        {
                            MessageBox.Show("Another user has updated or " +
                                "deleted that team.", "Database Error",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {
                            team = newTeam;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, ex.GetType().ToString());
                    }
                }

                ManageTeams manageTeams = new ManageTeams();
                NavigationService.Navigate(manageTeams);

            }
        }

        private void PutTeamData(Team team)
        {
            team.TeamID = Convert.ToInt32(txtTeamID.Text.Trim());
            team.TeamName = txtTeamName.Text.Trim();
            team.AbbrName = txtAbbrName.Text.Trim();
            team.City = txtCity.Text.Trim();
            team.State = txtState.Text.Trim();
            team.ZipCode = txtZipCode.Text.Trim();

        }

        private bool IsValidData()
        {
            return Validator.IsPresent(txtTeamName) &&
                   Validator.IsPresent(txtAbbrName) &&
                   Validator.IsPresent(txtCity) &&
                   Validator.IsPresent(txtState) &&
                   Validator.IsPresent(txtZipCode) &&
                   Validator.IsValidZip(txtZipCode) &&
                   Validator.IsPresent(txtTeamID) &&
                   Validator.IsInt32(txtTeamID);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ManageTeams manageTeams = new ManageTeams();
            NavigationService.Navigate(manageTeams);
        }

        private void GetTeam(int teamID)
        {
            try
            {
                team = QuiddichDB.GetTeam(teamID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        private void btnFindTeam_Click(object sender, RoutedEventArgs e)
        {
            if (isValidData())
            {
                int teamID = Convert.ToInt32(txtTeamID.Text);
                this.GetTeam(teamID);
                if (team == null)
                {
                    MessageBox.Show("No team found with this name. " +
                         "Please try again.", "Team Not Found");
                }
                else
                {
                    this.DisplayTeam();
                }
            }
        }

        private bool isValidData()
        {
            bool valid = false;

            if (Validator.IsInt32(txtTeamID))
            {
                valid = true;
            }
            return valid;
        }

        private void DisplayTeam()
        {
            txtTeamID.Text = team.TeamID.ToString();
            txtTeamName.Text = team.TeamName;
            txtAbbrName.Text = team.AbbrName;
            txtCity.Text = team.City;
            txtState.Text = team.State;
            txtZipCode.Text = team.ZipCode;
        }

        private void txtZipCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            //GetInfo();
        }

        private void GetInfo()
        {
            //string location = new WebClient().DownloadString("http://api.ipinfodb.com/v3/ip-city/?key=9bf463d68b2c27af0366950c4e4a607100143e9c798b42959ee508f73f15b509");
            //string[] locationParams = new string[10];

            //locationParams = location.Split(';');

            //string city  = locationParams[6].ToLower().Trim();
            //char.ToUpper(city[0]);

            //string state = locationParams[5].Trim();
            //state = state.Substring(0,2);

            //txtCity.Text = city;
            //txtState.Text = state;
        }
    }
}
