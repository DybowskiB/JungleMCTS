using JungleMCTS.Enums;
using JungleMCTS.GameBoard;

namespace JungleMCTS.Players
{
    public class HumanPlayer : Player
    {
        public HumanPlayer(PlayerIdEnum playerIdEnum) : base(playerIdEnum)
        {
        }

        public void Move(Board board, Position currentPosition, Position newPosition)
            => board.Move(currentPosition, newPosition);
    }
}
