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
    /// Interaction logic for ModifyTeam.xaml
    /// </summary>
    public partial class ModifyTeam : Page
    {
        private Team team;

        public ModifyTeam()
        {
            InitializeComponent();
        }

        public ModifyTeam(int teamID)
        {
            InitializeComponent();
            this.GetTeam(teamID);
        }

        private void GetTeam(int teamID)
        {
            team = QuiddichDB.GetTeam(teamID);
            txtTeamID.Text = team.TeamID.ToString();            
            txtTeamName.Text = team.TeamName;
            txtAbbrName.Text = team.AbbrName;
            txtCity.Text = team.City;
            txtState.Text = team.State;
            txtZipCode.Text = team.ZipCode;

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ManageTeams manageTeams = new ManageTeams();
            NavigationService.Navigate(manageTeams);
        }


        private void btnModifyTeam_Click(object sender, RoutedEventArgs e)
        {
            if(isValidData())
            {
                Team newTeam = new Team();
                newTeam.TeamName = team.TeamName;
                this.PutTeamData(newTeam);
                try
                {
                    if (!QuiddichDB.UpdateTeam(team, newTeam))
                    {
                        MessageBox.Show("Another user has updated or " +
                            "deleted that team.", "Database Error");
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

                ManageTeams manageTeams = new ManageTeams();
                NavigationService.Navigate(manageTeams);
            }
            
            
        }

        private void PutTeamData(Team team)
        {
            team.TeamID = Convert.ToInt32(txtTeamID.Text);
            team.TeamName = txtTeamName.Text;
            team.AbbrName = txtAbbrName.Text;
            team.City = txtCity.Text;
            team.State = txtState.Text;
            team.ZipCode = txtZipCode.Text;
        }

        private bool isValidData()
        {
            return Validator.IsPresent(txtTeamID) &&
                   Validator.IsInt32(txtTeamID) &&
                   Validator.IsPresent(txtTeamName) &&
                   Validator.IsPresent(txtAbbrName) &&
                   Validator.IsPresent(txtCity) &&
                   Validator.IsPresent(txtState) &&
                   Validator.IsPresent(txtZipCode) &&
                   Validator.IsValidZip(txtZipCode);
        }
    }
}
