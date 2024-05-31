using JungleMCTS.GameBoard.GameFields;
using JungleMCTS.Enums;
using JungleMCTS.Exceptions;
using JungleMCTS.GameBoard;
using JungleMCTS.GameBoard.Controllers;

namespace JungleMCTS.GamePiece.Pieces
{
    public class Cat : Piece
    {
        public Cat(PlayerIdEnum playerIdEnum) : base(2, playerIdEnum) { }

        public override object Clone() => new Cat(PlayerIdEnum);


        // Movement
        public override bool CanMoveTo(Lake lake) => false;

        public override bool CanMoveTo(Trap trap) => true;

        public override List<Position> GetPossiblePositions(Position currentPosition, Board board)
            => MoveController.GetDefaultPossiblePositions(currentPosition, board, this);


        // Capturing
        public override int GetAttackerStrength(DefaultField attackerField, Piece defender, GameField defenderField)
            => DefaultStrength;

        public override int GetAttackerStrength(Lake attackerField, Piece defender, GameField defenderField)
             => throw new InvalidGameStateException("Cannot get cat strength in lake: cat cannot swim.");

        public override int GetAttackerStrength(Trap attackerField, Piece defender, GameField defenderField)
            => PlayerIdEnum == attackerField.PlayerIdEnum ? DefaultStrength : 0;


        public override int GetAttackerStrengthBasedOnRivals(Mouse attacker)
            => attacker.DefaultStrength;
    }
}
