using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuiddichV2._0
{
    public static class TeamDB
    {
        public static List<Team> GetTeams()
        {
            List<Team> teams = new List<Team>();
            SqlConnection connection = LocalDB.GetConnection();
            string selectPlayerment = "SELECT TeamID, TeamName, AbbrName, City, State, ZipCode "
                                   + "FROM Teams "
                                   + "ORDER BY TeamID";
            SqlCommand selectCommand =
                new SqlCommand(selectPlayerment, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                while (reader.Read())
                {
                    Team team = new Team();
                    team.TeamID = Convert.ToInt32(reader["TeamID"]);
                    team.TeamName = reader["TeamName"].ToString();
                    team.AbbrName = reader["AbbrName"].ToString();
                    team.City = reader["City"].ToString();
                    team.State = reader["State"].ToString();
                    team.ZipCode = reader["ZipCode"].ToString();

                    teams.Add(team);
                }
                reader.Close();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return teams;
        }
    }
}
