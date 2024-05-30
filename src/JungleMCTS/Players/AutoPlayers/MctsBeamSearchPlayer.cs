using JungleMCTS.Enums;
using JungleMCTS.GameBoard;

namespace JungleMCTS.Players.AutoPlayers
{
    public class MctsBeamSearchPlayer : AutoPlayer
    {
        public MctsBeamSearchPlayer(PlayerIdEnum playerIdEnum, TimeSpan maxMoveTime)
            : base(playerIdEnum, maxMoveTime) { }

        public override void Move(Board board)
        {
            throw new NotImplementedException();
        }
    }
}
