using JungleMCTS.GameBoard;

namespace JungleMCTS.Players.AutoPlayers.MctsPlayers.MctsResources
{
    public class MctsAction
    {
        public Position CurrentPosition { get; set; }

        public Position NewPosition { get; set; }


        public MctsAction(Position currentPostion, Position newPosition)
        {
            CurrentPosition = currentPostion;
            NewPosition = newPosition;
        }
    }
}
