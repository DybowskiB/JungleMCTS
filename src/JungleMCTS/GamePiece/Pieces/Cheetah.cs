using JungleMCTS.GameBoard.GameFields;
using JungleMCTS.Enums;
using JungleMCTS.Exceptions;
using JungleMCTS.GameBoard;
using JungleMCTS.GameBoard.Controllers;

namespace JungleMCTS.GamePiece.Pieces
{
    public class Cheetah : Piece
    {
        public Cheetah(PlayerIdEnum playerIdEnum) : base(5, playerIdEnum) { }

        public override object Clone() => new Cheetah(PlayerIdEnum);


        // Movement
        public override bool CanMoveTo(Lake lake) => false;

        public override bool CanMoveTo(Trap trap) => PlayerIdEnum != trap.PlayerIdEnum;

        public override List<Position> GetPossiblePositions(Position currentPosition, Board board)
        {
            var possiblePositions = MoveController.GetDefaultPossiblePositions(currentPosition, board, this);
            possiblePositions.AddRange(MoveController.GetPossibleHorizontalJumpPositions(currentPosition, board));
            return possiblePositions;
        }


        // Capturing
        public override int GetAttackerStrength(DefaultField attackerField, Piece defender, GameField defenderField)
            => DefaultStrength;

        public override int GetAttackerStrength(Lake attackerField, Piece defender, GameField defenderField)
             => throw new InvalidGameStateException("Cannot get cheetah strength in lake: cheetah cannot swim.");

        public override int GetAttackerStrength(Trap attackerField, Piece defender, GameField defenderField)
             => PlayerIdEnum == attackerField.PlayerIdEnum ? DefaultStrength : 0;


        public override int GetAttackerStrengthBasedOnRivals(Mouse attacker)
            => attacker.DefaultStrength;

        public override int GetAttackerStrengthBasedOnRivals(Elephant attacker)
            => attacker.DefaultStrength;
    }
}
