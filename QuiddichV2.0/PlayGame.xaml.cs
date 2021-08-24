using PusherServer;
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
    /// Interaction logic for PlayGame.xaml
    /// </summary>
    public partial class PlayGame : Page
    {
        string APP_ID = "100678";
        string APP_KEY = "1d1713fb67cdc97cee0e";
        string APP_SECRET = "97f15eb967ee36a6ae42";
        Pusher pusher;
        private Team team1;
        private Team team2;

        int scoreTeam1 = 0;
        int scoreTeam2 = 0;


        List<Player> team1Players;
        List<Player> team2Players;


        public PlayGame()
        {
            InitializeComponent();
            pusher = new Pusher(APP_ID, APP_KEY, APP_SECRET);
        }

        public PlayGame(Team team1, Team team2)
        {
            InitializeComponent();
            pusher = new Pusher(APP_ID, APP_KEY, APP_SECRET);
            this.team1 = team1;
            this.team2 = team2;
            btnNewMatch.IsEnabled = false;

            setButtons(team1, team2);
        }

        private void btnNewMatch_Click(object sender, RoutedEventArgs e)
        {
            this.startGame();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (btnNewMatch.IsEnabled)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to stop reporting before the match ends?", 
                                                          "Match Not Over", MessageBoxButton.YesNo, 
                                                          MessageBoxImage.Question);

                switch (result)
                {
                    case MessageBoxResult.Yes:
                        ReporterMenu reporterMenu = new ReporterMenu();
                        NavigationService.Navigate(reporterMenu);
                        break;
                }
            }
        }

        #region Button setup

        private void setButtons(Team team1, Team team2)
        {

            team1Players = PlayerDB.GetPlayers(team1.TeamID);
            team2Players = PlayerDB.GetPlayers(team2.TeamID);

            for (int i = 0; i < team1Players.Count; i++ )
            {
                if(team1Players[i].Position == QuiddichDB.GetPlayer(team1.TeamID, "Seeker").Position)
                {
                    team1Button4.Content = team1Players[i].LastName;
                }
            }

            for (int i = 0; i < team2Players.Count; i++)
            {
                if (team2Players[i].Position == QuiddichDB.GetPlayer(team2.TeamID, "Seeker").Position)
                {
                    team2Button4.Content = team2Players[i].LastName;
                }
            }

            for (int i = 0; i < 2; i++ )
            {
                if (team1Players[i].Position == QuiddichDB.GetPlayer(team1.TeamID, "Chaser").Position)
                {
                    team1Button1.Content = team1Players[i].LastName;
                    team1Button2.Content = team1Players[i + 1].LastName;
                    team1Button3.Content = team1Players[i + 2].LastName;
                }

                if (team2Players[i].Position == QuiddichDB.GetPlayer(team2.TeamID, "Chaser").Position)
                {
                    team2Button1.Content = team2Players[i].LastName;
                    team2Button2.Content = team2Players[i + 1].LastName;
                    team2Button3.Content = team2Players[i + 2].LastName;
                }
            }

            team1Button0.Content = team1.AbbrName;
            team2Button0.Content = team2.AbbrName;
            
        }

        private void startGame()
        {
            team1Button0.IsEnabled = true;
            team1Button1.IsEnabled = true;
            team1Button2.IsEnabled = true;
            team1Button3.IsEnabled = true;
            team1Button4.IsEnabled = true;

            team2Button0.IsEnabled = true;
            team2Button1.IsEnabled = true;
            team2Button2.IsEnabled = true;
            team2Button3.IsEnabled = true;
            team2Button4.IsEnabled = true;

            btnNewMatch.IsEnabled = false;
        }

        private void endGame()
        {
            team1Button0.IsEnabled = false;
            team1Button1.IsEnabled = false;
            team1Button2.IsEnabled = false;
            team1Button3.IsEnabled = false;
            team1Button4.IsEnabled = false;

            team2Button0.IsEnabled = false;
            team2Button1.IsEnabled = false;
            team2Button2.IsEnabled = false;
            team2Button3.IsEnabled = false;
            team2Button4.IsEnabled = false;

            btnNewMatch.IsEnabled = true;
        }

        #endregion
        
        #region Team 1 buttons

        private void team1Button0_Click(object sender, RoutedEventArgs e)
        {
            scoreTeam1 += 10;
            pusher.Trigger("my-channel", "my-event", new
            {
                message = team1.TeamName + " scored 10 points!" + "\n" +
                 team1.TeamName + " has " + scoreTeam1 + " points!"
            });
        }

        private void team1Button1_Click(object sender, RoutedEventArgs e)
        {
            scoreTeam1 += 10;
            pusher.Trigger("my-channel", "my-event", new
            {
                message = team1Players[0].LastName + " scored 10 points!" + "\n" +
                 team1.TeamName + " has " + scoreTeam1 + " points!"
            });
        }

        private void team1Button2_Click(object sender, RoutedEventArgs e)
        {
            scoreTeam1 += 10;
            pusher.Trigger("my-channel", "my-event", new
            {
                message = team1Players[1].LastName + " scored 10 points!" + "\n" +
                 team1.TeamName + " has " + scoreTeam1 + " points!"
            });
        }

        private void team1Button3_Click(object sender, RoutedEventArgs e)
        {
            scoreTeam1 += 10;
            pusher.Trigger("my-channel", "my-event", new
            {
                message = team1Players[2].LastName + " scored 10 points!" + "\n" +
                 team1.TeamName + " has " + scoreTeam1 + " points!"
            });
        }

        private void team1Button4_Click(object sender, RoutedEventArgs e)
        {
            string winner;

            scoreTeam1 += 150;

            if (scoreTeam1 > scoreTeam2)
            {
                 winner = team1.TeamName.Trim() + " wins the match!";
            }
            else
            {
                 winner = team2.TeamName.Trim() + " wins the match!";
            }

            pusher.Trigger("my-channel", "my-event", new
            {
                message = team1Players[3].LastName + " scored 150 points!" + "\n" +
                 team1.TeamName + " has " + scoreTeam1 + " points!" + "/n" +
                 winner
            });

            this.endGame();
        }

        #endregion

        #region Team 2 buttons

        private void team2Button0_Click(object sender, RoutedEventArgs e)
        {
            scoreTeam2 += 10;
            pusher.Trigger("my-channel", "my-event", new
            {
                message = team2.TeamName + " scored 10 points!" + "\n" +
                 team2.TeamName + " has " + scoreTeam2 + " points!"
            });
        }

        

        private void team2Button1_Click(object sender, RoutedEventArgs e)
        {
            scoreTeam2 += 10;
            pusher.Trigger("my-channel", "my-event", new
            {
                message = team2Players[0].LastName + " scored 10 points!" + "\n" +
                 team2.TeamName + " has " + scoreTeam2 + " points!"
            });
        }

        private void team2Button2_Click(object sender, RoutedEventArgs e)
        {
            scoreTeam2 += 10;
            pusher.Trigger("my-channel", "my-event", new
            {
                message = team2Players[1].LastName + " scored 10 points!" + "\n" +
                 team2.TeamName + " has " + scoreTeam2 + " points!"
            });
        }

        private void team2Button3_Click(object sender, RoutedEventArgs e)
        {
            scoreTeam2 += 10;
            pusher.Trigger("my-channel", "my-event", new
            {
                message = team2Players[2].LastName + " scored 10 points!" + "\n" +
                 team2.TeamName + " has " + scoreTeam2 + " points!"
            });
        }

        private void team2Button4_Click(object sender, RoutedEventArgs e)
        {

            string winner = "";
           
            scoreTeam2 += 150;

            if (scoreTeam1 > scoreTeam2)
            {
                 winner = team1.TeamName.Trim() + " wins the match!";
            }
            else
            {
                 winner = team2.TeamName.Trim() + " wins the match!";
            }

            pusher.Trigger("my-channel", "my-event", new
            {
                message = team2Players[3].LastName + " scored 150 points!" + "\n" +
                 team2.TeamName + " has " + scoreTeam2 + " points!" + "\n" +
                 winner
            });

            this.endGame();
        }

        #endregion

        

       
    }
}
