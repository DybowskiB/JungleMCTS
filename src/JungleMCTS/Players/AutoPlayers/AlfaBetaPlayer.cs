using JungleMCTS.Enums;
using JungleMCTS.GameBoard;

namespace JungleMCTS.Players.AutoPlayers
{
    public class AlfaBetaPlayer : AutoPlayer
    {
        public AlfaBetaPlayer(PlayerIdEnum playerIdEnum, TimeSpan maxMoveTime)
            : base(playerIdEnum, maxMoveTime) { }

        public override void Move(Board board)
        {
            throw new NotImplementedException();
        }
    }
}
