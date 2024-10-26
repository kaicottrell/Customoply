namespace CustomMonopoly.Server.Models
{
    /// <summary>
    /// Let's say we introduce tournments into our game. Where we have a list of UserResults {score: int, playerId} for the tournament and a list of subtournaments ( if any)
    /// Create a recurive algorithm that will get the highest player score from a tournment, including all of its subtournments. 
    /// </summary>
    public class Tournament
    {

        public List<UserResult> UserResults = new List<UserResult>();
        public List<Tournament> SubTournaments = new List<Tournament>();

        public static void TestTournaments()
        {
            UserResult ur1 = new UserResult { PlayerId = 1, Score = 10 };
            UserResult ur2 = new UserResult { PlayerId = 2, Score = 3 };
            UserResult ur3 = new UserResult { PlayerId = 3, Score = 12 };

            Tournament t2 = new Tournament
            {
                UserResults = new List<UserResult> { ur2, ur3 }
            };



            Tournament t3 = new Tournament
            {
                UserResults = new List<UserResult> { ur3 }
            };

            List<Tournament> SubTournments = new List<Tournament> { t2, t3};


            Tournament t1 = new Tournament
            {
                UserResults = new List<UserResult> { ur1, ur2, ur3 },
                SubTournaments = SubTournments
            };

            int tournamentMaxScore = getMaxTournamentScore(t1);



        }
        /// <summary>
        /// Recursive algorithm that returns the max score given a tournament and all of its sub tournaments 
        /// </summary>
        /// <returns></returns>
        public static int getMaxTournamentScore(Tournament t)
        {
            int max = -1;
            //Base case - no subtouraments to seek
          
            // loop through tournaments and return the max
            for (int i = 0; i < t.UserResults.Count(); i++)
            {
                max = Math.Max(max, t.UserResults[i].Score);
            }
           
            // base case where there are no sub tournaments left
            for (int i = 0; i < t.SubTournaments.Count(); i++ )
            {
               max = Math.Max(max, getMaxTournamentScore(t.SubTournaments[i]));
            }

            return max;
            

        }
    }
    public class UserResult
    {
        public int PlayerId { get; set; }
        public int Score { get; set; }
    }
}
