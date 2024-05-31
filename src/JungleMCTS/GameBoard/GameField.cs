using JungleMCTS.GameBoard.GameFields;
using JungleMCTS.GamePiece;
using JungleMCTS.GamePiece.Pieces;

namespace JungleMCTS.GameBoard
{
    public abstract class GameField : ICloneable
    {
        public abstract object Clone();

        // Movement
        public abstract bool CanContain(Piece piece);


        // Piece strength
        public abstract int GetDefenderStrength(Piece defender);

        public abstract int GetAttackerStrength(Piece attacker, Piece defender, GameField defenderField);

        public abstract int GetAttackerStrengthBasedOnFields(Mouse attacker, Lake attackerField);

        public abstract int GetAttackerStrengthBasedOnFields(Dog attacker, Lake attackerField);

        public abstract int GetAttackerStrengthBasedOnFields(Wolf attacker, Lake attackerField);

    }

}
