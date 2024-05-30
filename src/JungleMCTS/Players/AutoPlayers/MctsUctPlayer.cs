using JungleMCTS.Enums;
using JungleMCTS.GameBoard;

namespace JungleMCTS.Players.AutoPlayers
{
    public class MctsUctPlayer : AutoPlayer
    {
        public MctsUctPlayer(PlayerIdEnum playerIdEnum, TimeSpan maxMoveTime)
            : base(playerIdEnum, maxMoveTime) { }

        public override void Move(Board board)
        {
            throw new NotImplementedException();
        }
    }
}
