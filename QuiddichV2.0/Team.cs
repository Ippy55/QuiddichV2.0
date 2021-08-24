using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuiddichV2._0
{
    public class Team
    {
        private int teamID;
        private string teamName;
        private string abbrName;
        private string city;
        private string state;
        private string zipCode;
        private List<Player> roster;
       

        #region Properties

        public String TeamName
        {
            get
            {
                return teamName;
            }
            set
            {
                teamName = value;
            }
        }

        public String AbbrName
        {
            get
            {
                return abbrName;
            }
            set
            {
                abbrName = value;
            }
        }

        public String City
        {
            get
            {
                return city;
            }
            set
            {
                city = value;
            }
        }

        public String State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
            }
        }

        public String ZipCode
        {
            get
            {
                return zipCode;
            }
            set
            {
                zipCode = value;
            }
        }

        public List<Player> Roster
        {
            get
            {
                return roster;
            }
            set
            {
                roster = value;
            }
        }

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

        #endregion

        public Team()
        {
        }

        public List<Player> getPlayers(int teamID)
        {
            return PlayerDB.GetPlayers(teamID);
        }

        public void add(Player playerToAdd)
        {
            roster.Add(playerToAdd);
        }

        public override bool Equals(object obj)
        {
            bool valid = false;

            if (obj == null)
            {
                valid = false;
            }


            Team p = obj as Team;
            if ((System.Object)p == null)
            {
                valid = false;
            }

            if (teamName == p.teamName && abbrName == p.abbrName && city == p.city && state == p.state && teamID == p.teamID)
            {
                valid = true;
            }

            return valid;
        }

        public override int GetHashCode()
        {
            String hashString = teamID + teamName + abbrName  + city + state + zipCode;
            return hashString.GetHashCode();
        }

        public override string ToString()
        {
            return  "Team ID: " + teamID + "\nTeam Name: " + teamName + "\nAbbreviated Name: " + abbrName + "\nCity: " +
                    city + "\nState: " + state + "\nZip Code: " + zipCode;
        }

        public string GetDisplayText()
        {
            return teamID + ", " + teamName + ", " + abbrName + ", " + city + ", " 
                    + state + ", " + zipCode;
        }

        public string GetDisplayText(string sep)
        {
            return  teamID + sep + teamName + sep + abbrName + sep + city + sep
                + state + sep + zipCode;
        }

    }
}
