using JungleMCTS.Enums;
using JungleMCTS.Exceptions;
using JungleMCTS.GamePiece;
using JungleMCTS.GamePiece.Pieces;

namespace JungleMCTS.GameBoard.GameFields
{
    public class Trap : GameField
    {
        public PlayerIdEnum PlayerIdEnum { get; }

        public Trap(PlayerIdEnum playerIdEnum) : base()
        {
            PlayerIdEnum = playerIdEnum;
        }

        // Movement
        public override bool CanContain(Piece piece) => piece.CanMoveTo(this);


        // Piece strength

        public override int GetDefenderStrength(Piece defender)
            => PlayerIdEnum == defender.PlayerIdEnum ? defender.DefaultStrength : 0;

        public override int GetAttackerStrength(Piece attacker, Piece defender, GameField defenderField)
            => attacker.GetAttackerStrength(this, defender, defenderField);

        public override int GetAttackerStrengthBasedOnFields(Mouse attacker, Lake attackerField)
            => throw new InvalidGameStateException("Cannot find strength for mouse attack from lake to trap, because there is no such move possible.");

        public override int GetAttackerStrengthBasedOnFields(Dog attacker, Lake attackerField)
            => throw new InvalidGameStateException("Cannot find strength for dog attack from lake to trap, because there is no such move possible.");

        public override int GetAttackerStrengthBasedOnFields(Wolf attacker, Lake attackerField)
            => throw new InvalidGameStateException("Cannot find strength for wolf attack from lake to trap, because there is no such move possible.");
    }
}
