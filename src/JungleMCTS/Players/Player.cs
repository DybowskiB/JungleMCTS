using JungleMCTS.Enums;
using JungleMCTS.GameBoard;

namespace JungleMCTS.Players
{
    public abstract class Player
    {
        public PlayerIdEnum PlayerIdEnum { get; set; }

        public List<Position> PiecePositions { get; }

        public Player(PlayerIdEnum playerIdEnum)
        {
            PlayerIdEnum = playerIdEnum;
            if (playerIdEnum is PlayerIdEnum.FirstPlayer)
                PiecePositions =
                    [
                        new Position(0 ,0),
                        new Position(0, 6),
                        new Position(1, 1),
                        new Position(1, 5),
                        new Position(2, 0),
                        new Position(2, 2),
                        new Position(2, 4),
                        new Position(2, 6)
                    ];
            else
                PiecePositions =
                    [
                        new Position(8, 0),
                        new Position(8, 6),
                        new Position(7, 1),
                        new Position(7, 5),
                        new Position(6, 0),
                        new Position(6, 2),
                        new Position(6, 4),
                        new Position(6, 6),
                    ];
        }
    }
}
