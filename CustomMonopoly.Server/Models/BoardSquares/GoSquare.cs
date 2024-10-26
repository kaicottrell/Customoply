namespace CustomMonopoly.Server.Models.BoardSquares
{
    public class GoSquare : BoardSquare
    {
        public int RewardCash { get; set; }
        public GoSquare() {
            RewardCash = 200;
        } 
        public GoSquare(int rewardCash) {
            RewardCash = rewardCash;
        }
    }
}
