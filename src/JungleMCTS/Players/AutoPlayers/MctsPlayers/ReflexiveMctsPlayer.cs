using JungleMCTS.Enums;
using JungleMCTS.GameBoard;

namespace JungleMCTS.Players.AutoPlayers.MctsPlayers
{
    public class ReflexiveMctsPlayer : AutoPlayer
    {
        public ReflexiveMctsPlayer(PlayerIdEnum playerIdEnum, TimeSpan maxMoveTime)
            : base(playerIdEnum, maxMoveTime) { }

        public override void Move(Board board)
        {
            throw new NotImplementedException();
        }
    }
}
