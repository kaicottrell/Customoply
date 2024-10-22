namespace CustomMonopoly.Server.Models.BoardSquares
{
    public class GoSquare : BoardSquare
    {
        public int RewardCash { get; }
        public GoSquare() {
            RewardCash = 300;
        } 
        public GoSquare(int rewardCash) {
            RewardCash = rewardCash;
        }
    }
}
