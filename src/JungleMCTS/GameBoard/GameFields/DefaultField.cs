using JungleMCTS.GamePiece;
using JungleMCTS.GamePiece.Pieces;

namespace JungleMCTS.GameBoard.GameFields
{
    public class DefaultField : GameField
    {
        public DefaultField() { }

        public override object Clone() => new DefaultField();


        // Movement
        public override bool CanContain(Piece piece) => true;


        // Piece strength

        public override int GetDefenderStrength(Piece defender) => defender.DefaultStrength;

        public override int GetAttackerStrength(Piece attacker, Piece defender, GameField defenderField)
            => attacker.GetAttackerStrength(this, defender, defenderField);

        public override int GetAttackerStrengthBasedOnFields(Mouse attacker, Lake attackerField)
            => 0;

        public override int GetAttackerStrengthBasedOnFields(Dog attacker, Lake attackerField)
            => 0;

        public override int GetAttackerStrengthBasedOnFields(Wolf attacker, Lake attackerField)
            => 0;
    }
}
