using JungleMCTS.Enums;
using JungleMCTS.GameBoard;

namespace JungleMCTS.Players
{
    public abstract class AutoPlayer : Player
    {
        protected TimeSpan _maxMoveTime;

        protected AutoPlayer(PlayerIdEnum playerIdEnum, TimeSpan maxMoveTime) : base(playerIdEnum) 
        {
            _maxMoveTime = maxMoveTime;
        }

        public abstract void Move(Board board);
    }
}
