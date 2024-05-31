using JungleMCTS.GameBoard.GameFields;
using JungleMCTS.Enums;
using JungleMCTS.GameBoard;
using JungleMCTS.GameBoard.Controllers;

namespace JungleMCTS.GamePiece.Pieces
{
    public class Wolf : SwimmingPiece
    {
        public Wolf(PlayerIdEnum playerIdEnum) : base(4, playerIdEnum) { }

        public override object Clone() => new Wolf(PlayerIdEnum);


        // Movement
        public override bool CanMoveTo(Lake lake) => true;

        public override bool CanMoveTo(Trap trap) => PlayerIdEnum != trap.PlayerIdEnum;

        public override List<Position> GetPossiblePositions(Position currentPosition, Board board)
            => MoveController.GetDefaultPossiblePositions(currentPosition, board, this);


        // Capturing
        public override int GetAttackerStrength(DefaultField attackerField, Piece defender, GameField defenderField)
            => DefaultStrength;

        public override int GetAttackerStrength(Lake attackerField, Piece defender, GameField defenderField)
             => defenderField.GetAttackerStrengthBasedOnFields(this, attackerField);

        public override int GetAttackerStrength(Trap attackerField, Piece defender, GameField defenderField)
             => PlayerIdEnum == attackerField.PlayerIdEnum ? DefaultStrength : 0;


        public override int GetAttackerStrengthBasedOnRivals(Mouse attacker)
            => attacker.DefaultStrength;
    }
}
