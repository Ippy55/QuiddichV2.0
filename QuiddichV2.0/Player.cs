using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuiddichV2._0
{
    public class Player
    {
        private int teamID;
        private string firstName;
        private string lastName;
        private int uniformNumber;
        private string position;
       

        #region Properties

        public int TeamID
        {
            get
            {
                return teamID;
            }
            set
            {
                teamID = value;
            }
        }

        public String FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                firstName = value;
            }
        }
        public String LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                lastName = value;
            }
        }
        public int UniformNumber
        {
            get
            {
                return uniformNumber;
            }

            set
            {
                uniformNumber = value;
            }
        }

        public String Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }

        

        #endregion

        public Player()
        {

        }

        public override bool Equals(object obj)
        {
            bool valid = false;

            // If parameter is null return false.
            if (obj == null)
            {
                valid = false;
            }

            // If parameter cannot be cast to has return false.
            Player p = obj as Player;
            if ((System.Object)p == null)
            {
                valid = false;
            }

            // Check to see if all parameters match
            if (p.firstName == firstName && p.lastName == lastName && p.uniformNumber == uniformNumber && p.position == position && p.teamID == teamID)
            {
                valid = true;
            }

            return valid;
        }

        public void addPlayerToTeam(Player player, Team team)
        {
            team.add(player);
        }

        public override int GetHashCode()
        {
            string hashString = firstName + lastName + uniformNumber + position + teamID;
            return hashString.GetHashCode();
        }

        public override string ToString()
        {
            return "\nTeam ID: " + teamID + "\nFirst name: " + firstName + "\nLast name: " + lastName + "\nUniform number: " 
                + uniformNumber + "\nPosition: " + position;
        }

        public string GetDisplayText()
        {
            return teamID + ", " + firstName + ", " + lastName + ", " + uniformNumber + ", " + position;
        }

        public string GetDisplayText(string sep)
        {
            return teamID + sep + firstName + sep + lastName + sep + uniformNumber + sep + position;
        }
    }
}
