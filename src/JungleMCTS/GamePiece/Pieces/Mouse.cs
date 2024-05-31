using JungleMCTS.GameBoard.GameFields;
using JungleMCTS.Enums;
using JungleMCTS.GameBoard;
using JungleMCTS.GameBoard.Controllers;

namespace JungleMCTS.GamePiece.Pieces
{
    public class Mouse : SwimmingPiece
    {
        public Mouse(PlayerIdEnum playerIdEnum) : base(1, playerIdEnum) { }

        public override object Clone() => new Mouse(PlayerIdEnum);


        // Movement
        public override bool CanMoveTo(Lake lake) => true;

        public override bool CanMoveTo(Trap trap) => true;

        public override List<Position> GetPossiblePositions(Position currentPosition, Board board)
            => MoveController.GetDefaultPossiblePositions(currentPosition, board, this);


        // Capturing
        public override int GetAttackerStrength(DefaultField attackerField, Piece defender, GameField defenderField)
            => defender.GetAttackerStrengthBasedOnRivals(this);

        public override int GetAttackerStrength(Lake attackerField, Piece defender, GameField defenderField)
             => defenderField.GetAttackerStrengthBasedOnFields(this, attackerField);

        public override int GetAttackerStrength(Trap attackerField, Piece defender, GameField defenderField)
             => PlayerIdEnum == attackerField.PlayerIdEnum ? defender.GetAttackerStrengthBasedOnRivals(this) : 0;


        public override int GetAttackerStrengthBasedOnRivals(Mouse attacker)
            => attacker.DefaultStrength;
    }
}
