using JungleMCTS.GameBoard.GameFields;
using JungleMCTS.Enums;
using JungleMCTS.GameBoard;
using JungleMCTS.GamePiece.Pieces;

namespace JungleMCTS.GamePiece
{
    public abstract class Piece : ICloneable
    {
        public PlayerIdEnum PlayerIdEnum { get; set; }

        public int DefaultStrength { get; set; }

        public Piece(int initialStrength, PlayerIdEnum playerIdEnum)
        {
            DefaultStrength = initialStrength;
            PlayerIdEnum = playerIdEnum;
        }

        public abstract object Clone();


        // Movement
        public abstract bool CanMoveTo(Lake lake);

        public abstract bool CanMoveTo(Trap trap);

        public abstract List<Position> GetPossiblePositions(Position currentPosition, Board board);


        // Capturing
        public abstract int GetAttackerStrength(DefaultField attackerField, Piece defender, GameField defenderField);

        public abstract int GetAttackerStrength(Lake attackerField, Piece defender, GameField defenderField);

        public abstract int GetAttackerStrength(Trap attackerField, Piece defender, GameField defenderField);


        public abstract int GetAttackerStrengthBasedOnRivals(Mouse attacker);

        public abstract int GetAttackerStrengthBasedOnRivals(Elephant attacker);
    }
}
