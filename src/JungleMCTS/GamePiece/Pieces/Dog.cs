using JungleMCTS.GameBoard.GameFields;
using JungleMCTS.Enums;
using JungleMCTS.GameBoard;

namespace JungleMCTS.GamePiece.Pieces
{
    public class Dog : SwimmingPiece
    {
        public Dog(PlayerIdEnum playerIdEnum) : base(3, playerIdEnum) { }


        // Movement
        public override bool CanMoveTo(Lake lake) => true;

        public override bool CanMoveTo(Trap trap) => true;

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
