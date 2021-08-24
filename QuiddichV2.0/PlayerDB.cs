using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuiddichV2._0
{
    public static class PlayerDB
    {
        public static List<Player> GetPlayers(int teamID)
        {
            List<Player> players = new List<Player>();
            SqlConnection connection = LocalDB.GetConnection();
            string selectPlayerment = "SELECT * "
                                   + "FROM Players "
                                   + "WHERE TeamID = " + teamID;
            SqlCommand selectCommand =
                new SqlCommand(selectPlayerment, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                while (reader.Read())
                {
                    Player player = new Player();
                    player.TeamID = Convert.ToInt32(reader["TeamID"]);
                    player.FirstName = reader["FirstName"].ToString();
                    player.LastName = reader["LastName"].ToString();
                    player.UniformNumber = Convert.ToInt32(reader["UniformNumber"]);
                    player.Position = reader["Position"].ToString();

                    players.Add(player);
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
            return players;
        }
    }
}
