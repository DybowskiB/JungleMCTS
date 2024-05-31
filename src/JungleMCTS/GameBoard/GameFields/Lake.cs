using JungleMCTS.GamePiece;
using JungleMCTS.GamePiece.Pieces;

namespace JungleMCTS.GameBoard.GameFields
{
    public class Lake : GameField
    {
        public Lake() { }

        public override object Clone() => new Lake();


        // Movement
        public override bool CanContain(Piece piece) => piece.CanMoveTo(this);


        // Piece strength

        public override int GetDefenderStrength(Piece defender) => defender.DefaultStrength;

        public override int GetAttackerStrength(Piece attacker, Piece defender, GameField defenderField)
            => attacker.GetAttackerStrength(this, defender, defenderField);

        public override int GetAttackerStrengthBasedOnFields(Mouse attacker, Lake attackerField)
            => attacker.DefaultStrength;

        public override int GetAttackerStrengthBasedOnFields(Dog attacker, Lake attackerField)
            => attacker.DefaultStrength;

        public override int GetAttackerStrengthBasedOnFields(Wolf attacker, Lake attackerField)
            => attacker.DefaultStrength;
    }
}
