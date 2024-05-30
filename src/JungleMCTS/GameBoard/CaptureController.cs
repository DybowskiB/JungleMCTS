using JungleMCTS.GameBoard.GameFields;
using JungleMCTS.GamePiece;
using JungleMCTS.GamePiece.Pieces;

namespace JungleMCTS.GameBoard
{
    public static class CaptureController
    {
        public static bool CanCapture(Piece attacker, GameField attackerField, Piece defender, GameField defenderField)
        {
            if (attacker.PlayerIdEnum == defender.PlayerIdEnum)
                return false;
            var attackerStrength = attackerField.GetAttackerStrength(attacker, defender, defenderField);
            var defenderStrength = defenderField.GetDefenderStrength(defender);
            return attackerStrength >= defenderStrength;
        }
    }
}
