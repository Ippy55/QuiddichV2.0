using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuiddichV2._0
{
    public static class QuiddichDB
    {
        #region Users

        public static User GetUser(string username, string password)
        {
            SqlConnection connection = LocalDB.GetConnection();
            string selectStatement
                = "SELECT Username, Password "
                + "FROM Reporters "
                + "WHERE Username = @Username "
                + "AND Password = @Password ";
            SqlCommand selectCommand =
                new SqlCommand(selectStatement, connection);
            selectCommand.Parameters.AddWithValue("@Username", username);
            selectCommand.Parameters.AddWithValue("@Password", password);

            try
            {
                connection.Open();
                SqlDataReader userReader =
                    selectCommand.ExecuteReader();
                if (userReader.Read())
                {
                    User user = new User();
                    user.Username = userReader["Username"].ToString();
                    user.Password = userReader["Password"].ToString();
                    return user;
                }
                else
                {
                    return null;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public static void AddUser(User user)
        {
            SqlConnection connection = LocalDB.GetConnection();
            string insertStatement =
                "INSERT Reporters " +
                "(Username, Password) " +
                "VALUES (@Username, @Password)";
            SqlCommand insertCommand =
                new SqlCommand(insertStatement, connection);
            insertCommand.Parameters.AddWithValue(
                "@Username", user.Username);
            insertCommand.Parameters.AddWithValue(
                "@Password", user.Password);
            try
            {
                connection.Open();
                insertCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public static bool DeleteUser(User user)
        {
            SqlConnection connection = LocalDB.GetConnection();
            string deleteStatement =
                "DELETE FROM Reporters " +
                "WHERE Username = @Username " +
                "AND Password = @Password ";
            SqlCommand deleteCommand =
                new SqlCommand(deleteStatement, connection);
            deleteCommand.Parameters.AddWithValue(
                "@Username", user.Username);
            deleteCommand.Parameters.AddWithValue(
                "@Password", user.Password);
            try
            {
                connection.Open();
                int count = deleteCommand.ExecuteNonQuery();
                if (count > 0)
                    return true;
                else
                    return false;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        #endregion

        #region Teams

        public static Team GetTeam(int teamID)
        {
            SqlConnection connection = LocalDB.GetConnection();
            string selectStatement
                = "SELECT TeamName, AbbrName, City, State, ZipCode, TeamID "
                + "FROM Teams "
                + "WHERE TeamID = @TeamID ";
            SqlCommand selectCommand =
                new SqlCommand(selectStatement, connection);
            selectCommand.Parameters.AddWithValue("@TeamID", teamID);

            try
            {
                connection.Open();
                SqlDataReader teamReader =
                    selectCommand.ExecuteReader();
                if (teamReader.Read())
                {
                    Team team = new Team();
                    team.TeamID = Convert.ToInt32(teamReader["TeamID"].ToString());
                    team.TeamName = teamReader["TeamName"].ToString();
                    team.AbbrName = teamReader["AbbrName"].ToString();
                    team.City = teamReader["City"].ToString();
                    team.State = teamReader["State"].ToString();
                    team.ZipCode = teamReader["ZipCode"].ToString();
                   

                    return team;
                }
                else
                {
                    return null;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public static void AddTeam(Team team)
        {
            SqlConnection connection = LocalDB.GetConnection();
            string insertStatement =
                "INSERT Teams " +
                "(TeamID, TeamName, AbbrName, City, State, ZipCode) " +
                "VALUES (@TeamID, @TeamName, @AbbrName, @City, @State, @ZipCode)";
            SqlCommand insertCommand =
                new SqlCommand(insertStatement, connection);
            insertCommand.Parameters.AddWithValue(
                "@TeamID", team.TeamID);
            insertCommand.Parameters.AddWithValue(
                "@TeamName", team.TeamName);
            insertCommand.Parameters.AddWithValue(
                "@AbbrName", team.AbbrName);
            insertCommand.Parameters.AddWithValue(
                "@City", team.City);
            insertCommand.Parameters.AddWithValue(
                "@State", team.State);
            insertCommand.Parameters.AddWithValue(
                "@ZipCode", team.ZipCode);
            
            try
            {
                connection.Open();
                insertCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public static bool UpdateTeam(Team oldTeam, Team newTeam)
        {
            SqlConnection connection = LocalDB.GetConnection();
            string updateStatement =
                "UPDATE Teams SET " +
                "TeamID = @NewTeamID, " +
                "TeamName = @NewTeamName, " +
                "AbbrName = @NewAbbrName, " +
                "City = @NewCity, " +
                "State = @NewState, " +
                "ZipCode = @NewZipCode " +

                "WHERE TeamID = @OldTeamID " +
                "AND TeamName = @OldTeamName " +
                "AND AbbrName = @OldAbbrName " +
                "AND City = @OldCity " +
                "AND State = @OldState " +
                "AND ZipCode = @OldZipCode ";

            SqlCommand updateCommand =
                new SqlCommand(updateStatement, connection);

            updateCommand.Parameters.AddWithValue(
               "@NewTeamID", newTeam.TeamID);
            updateCommand.Parameters.AddWithValue(
                "@NewTeamName", newTeam.TeamName);
            updateCommand.Parameters.AddWithValue(
                "@NewAbbrName", newTeam.AbbrName);
            updateCommand.Parameters.AddWithValue(
                "@NewCity", newTeam.City);
            updateCommand.Parameters.AddWithValue(
                "@NewState", newTeam.State);
            updateCommand.Parameters.AddWithValue(
                "@NewZipCode", newTeam.ZipCode);

            updateCommand.Parameters.AddWithValue(
                "@OldTeamID", oldTeam.TeamID);
            updateCommand.Parameters.AddWithValue(
                "@OldTeamName", oldTeam.TeamName);
            updateCommand.Parameters.AddWithValue(
                "@OldAbbrName", oldTeam.AbbrName);
            updateCommand.Parameters.AddWithValue(
                "@OldCity", oldTeam.City);
            updateCommand.Parameters.AddWithValue(
                "@OldState", oldTeam.State);
            updateCommand.Parameters.AddWithValue(
                "@OldZipCode", oldTeam.ZipCode);
            
            try
            {
                connection.Open();
                int count = updateCommand.ExecuteNonQuery();
                if (count > 0)
                    return true;
                else
                    return false;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public static bool DeleteTeam(Team team)
        {
            SqlConnection connection = LocalDB.GetConnection();
            string deleteStatement =
                "DELETE FROM Teams " +
                "WHERE TeamID = @TeamID " +
                "AND TeamName = @TeamName " +
                "AND AbbrName = @AbbrName " +
                "AND City = @City " +
                "AND State = @State " +
                "AND ZipCode = @ZipCode ";
            SqlCommand deleteCommand =
                new SqlCommand(deleteStatement, connection);
            deleteCommand.Parameters.AddWithValue(
                "@TeamID", team.TeamID);
            deleteCommand.Parameters.AddWithValue(
                "@TeamName", team.TeamName);
            deleteCommand.Parameters.AddWithValue(
                "@AbbrName", team.AbbrName);
            deleteCommand.Parameters.AddWithValue(
                "@City", team.City);
            deleteCommand.Parameters.AddWithValue(
                "@State", team.State);
            deleteCommand.Parameters.AddWithValue(
                "@ZipCode", team.ZipCode);
            try
            {
                connection.Open();
                int count = deleteCommand.ExecuteNonQuery();
                if (count > 0)
                    return true;
                else
                    return false;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        #endregion

        #region Players

        public static Player GetPlayer(int teamID)
        {
            SqlConnection connection = LocalDB.GetConnection();
            string selectStatement
                = "SELECT * "
                + "FROM Players "
                + "WHERE TeamID = @TeamID ";
            SqlCommand selectCommand =
                new SqlCommand(selectStatement, connection);
            selectCommand.Parameters.AddWithValue("@TeamID", teamID);

            try
            {
                connection.Open();
                SqlDataReader playerReader =
                    selectCommand.ExecuteReader();
                if (playerReader.Read())
                {
                    Player player = new Player();
                    player.TeamID = Convert.ToInt32(playerReader["TeamID"]);
                    player.FirstName = playerReader["FirstName"].ToString();
                    player.LastName = playerReader["LastName"].ToString();
                    player.UniformNumber = Convert.ToInt32(playerReader["UniformNumber"]);
                    player.Position = playerReader["Position"].ToString();
                    return player;
                }
                else
                {
                    return null;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public static Player GetPlayer(int teamID, string position)
        {
            SqlConnection connection = LocalDB.GetConnection();
            string selectStatement
                = "SELECT * "
                + "FROM Players "
                + "WHERE TeamID = @TeamID "
                + "AND Position = @Position ";
            SqlCommand selectCommand =
                new SqlCommand(selectStatement, connection);
            selectCommand.Parameters.AddWithValue("@TeamID", teamID);
            selectCommand.Parameters.AddWithValue("@Position", position);

            try
            {
                connection.Open();
                SqlDataReader playerReader =
                    selectCommand.ExecuteReader();
                if (playerReader.Read())
                {
                    Player player = new Player();
                    player.TeamID = Convert.ToInt32(playerReader["TeamID"]);
                    player.FirstName = playerReader["FirstName"].ToString();
                    player.LastName = playerReader["LastName"].ToString();
                    player.UniformNumber = Convert.ToInt32(playerReader["UniformNumber"]);
                    player.Position = playerReader["Position"].ToString();
                    return player;
                }
                else
                {
                    return null;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public static Player GetPlayer(int teamID, string firstName, string lastName)
        {
            SqlConnection connection = LocalDB.GetConnection();
            string selectStatement
                = "SELECT * "
                + "FROM Players "
                + "WHERE TeamID = @TeamID "
                + "AND FirstName = @FirstName "
                + "AND LastName = @LastName ";
            SqlCommand selectCommand =
                new SqlCommand(selectStatement, connection);
            selectCommand.Parameters.AddWithValue("@TeamID", teamID);
            selectCommand.Parameters.AddWithValue("@FirstName", firstName);
            selectCommand.Parameters.AddWithValue("@LastName", lastName);

            try
            {
                connection.Open();
                SqlDataReader playerReader =
                    selectCommand.ExecuteReader();
                if (playerReader.Read())
                {
                    Player player = new Player();
                    player.TeamID = Convert.ToInt32(playerReader["TeamID"]);
                    player.FirstName = playerReader["FirstName"].ToString();
                    player.LastName = playerReader["LastName"].ToString();
                    player.UniformNumber = Convert.ToInt32(playerReader["UniformNumber"]);
                    player.Position = playerReader["Position"].ToString();
                    return player;
                }
                else
                {
                    return null;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public static void AddPlayer(Player player)
        {
            SqlConnection connection = LocalDB.GetConnection();
            string insertStatement =
                "INSERT Players " +
                "(TeamID, FirstName, LastName, UniformNumber, Position) " +
                "VALUES (@TeamID, @FirstName, @LastName, @UniformNumber, @Position)";
            SqlCommand insertCommand =
                new SqlCommand(insertStatement, connection);
            insertCommand.Parameters.AddWithValue(
                "@TeamID", player.TeamID);
            insertCommand.Parameters.AddWithValue(
                "@FirstName", player.FirstName);
            insertCommand.Parameters.AddWithValue(
                "@LastName", player.LastName);
            insertCommand.Parameters.AddWithValue(
                "@UniformNumber", player.UniformNumber);
            insertCommand.Parameters.AddWithValue(
                "@Position", player.Position);

            try
            {
                connection.Open();
                insertCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public static bool UpdatePlayer(Player oldPlayer, Player newPlayer)
        {
            SqlConnection connection = LocalDB.GetConnection();
            string updateStatement =
                "UPDATE Players SET " +
                "TeamID = @NewTeamID, " +
                "FirstName = @NewFirstName, " +
                "LastName = @NewLastName, " +
                "UniformNumber = @NewUniformNumber, " +
                "Position = @NewPosition " +

                "WHERE TeamID = @OldTeamID " +
                "AND FirstName = @OldFirstName " +
                "AND LastName = @OldLastName " +
                "AND UniformNumber = @OldUniformNumber " +
                "AND Position = @OldPosition ";

            SqlCommand updateCommand =
                new SqlCommand(updateStatement, connection);

            updateCommand.Parameters.AddWithValue(
               "@NewTeamID", newPlayer.TeamID);
            updateCommand.Parameters.AddWithValue(
                "@NewFirstName", newPlayer.FirstName);
            updateCommand.Parameters.AddWithValue(
                "@NewLastName", newPlayer.LastName);
            updateCommand.Parameters.AddWithValue(
                "@NewUniformNumber", newPlayer.UniformNumber);
            updateCommand.Parameters.AddWithValue(
                "@NewPosition", newPlayer.Position);

            updateCommand.Parameters.AddWithValue(
                "@OldTeamID", oldPlayer.TeamID);
            updateCommand.Parameters.AddWithValue(
                "@OldFirstName", oldPlayer.FirstName);
            updateCommand.Parameters.AddWithValue(
                "@OldLastName", oldPlayer.LastName);
            updateCommand.Parameters.AddWithValue(
                "@OldUniformNumber", oldPlayer.UniformNumber);
            updateCommand.Parameters.AddWithValue(
                "@OldPosition", oldPlayer.Position);

            try
            {
                connection.Open();
                int count = updateCommand.ExecuteNonQuery();
                if (count > 0)
                    return true;
                else
                    return false;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public static bool DeletePlayer(Player player)
        {
            SqlConnection connection = LocalDB.GetConnection();
            string deleteStatement =
                "DELETE FROM Players " +
                "WHERE TeamID = @TeamID " +
                "AND FirstName = @FirstName " +
                "AND LastName = @LastName " +
                "AND UniformNumber = @UniformNumber " +
                "AND Position = @Position ";

            SqlCommand deleteCommand =
                new SqlCommand(deleteStatement, connection);
            deleteCommand.Parameters.AddWithValue(
                "@TeamID", player.TeamID);
            deleteCommand.Parameters.AddWithValue(
                "@FirstName", player.FirstName);
            deleteCommand.Parameters.AddWithValue(
                "@LastName", player.LastName);
            deleteCommand.Parameters.AddWithValue(
                "@UniformNumber", player.UniformNumber);
            deleteCommand.Parameters.AddWithValue(
                "@Position", player.Position);
            try
            {
                connection.Open();
                int count = deleteCommand.ExecuteNonQuery();
                if (count > 0)
                    return true;
                else
                    return false;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        #endregion
    }
}
