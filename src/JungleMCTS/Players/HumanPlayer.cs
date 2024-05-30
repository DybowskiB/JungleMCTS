using JungleMCTS.GameBoard;

namespace JungleMCTS.Players
{
    public class HumanPlayer
    {
        public void Move(Board board, Position currentPosition, Position newPosition)
            => board.Move(currentPosition, newPosition);
    }
}
