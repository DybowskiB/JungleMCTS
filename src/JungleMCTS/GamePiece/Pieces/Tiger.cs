using JungleMCTS.GameBoard.GameFields;
using JungleMCTS.Enums;
using JungleMCTS.Exceptions;
using JungleMCTS.GameBoard;

namespace JungleMCTS.GamePiece.Pieces
{
    public class Tiger : Piece
    {
        public Tiger(PlayerIdEnum playerIdEnum) : base(6, playerIdEnum) { }


        // Movement
        public override bool CanMoveTo(Lake lake) => false;

        public override bool CanMoveTo(Trap trap) => PlayerIdEnum != trap.PlayerIdEnum;

        public override List<Position> GetPossiblePositions(Position currentPosition, Board board)
        {
            var possiblePositions = MoveController.GetDefaultPossiblePositions(currentPosition, board, this);
            possiblePositions.AddRange(MoveController.GetPossibleHorizontalJumpPositions(currentPosition));
            possiblePositions.AddRange(MoveController.GetPossibleVerticalJumpPositions(currentPosition));
            return possiblePositions;
        }


        // Capturing
        public override int GetAttackerStrength(DefaultField attackerField, Piece piece, GameField defenderField)
            => DefaultStrength;

        public override int GetAttackerStrength(Lake attackerField, Piece piece, GameField defenderField)
             => throw new InvalidGameStateException("Cannot get cat strength in lake: cat cannot swim.");

        public override int GetAttackerStrength(Trap attackerField, Piece piece, GameField defenderField)
             => PlayerIdEnum == attackerField.PlayerIdEnum ? DefaultStrength : 0;


        public override int GetAttackerStrengthBasedOnRivals(Mouse attacker)
            => attacker.DefaultStrength;
    }
}
