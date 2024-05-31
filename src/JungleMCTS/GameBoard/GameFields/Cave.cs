using JungleMCTS.Enums;
using JungleMCTS.Exceptions;
using JungleMCTS.GamePiece;
using JungleMCTS.GamePiece.Pieces;

namespace JungleMCTS.GameBoard.GameFields
{
    public class Cave : GameField
    {
        public PlayerIdEnum PlayerIdEnum { get; } 

        public Cave(PlayerIdEnum playerIdEnum) : base()
        {
            PlayerIdEnum = playerIdEnum;
        }

        public override object Clone() => new Cave(PlayerIdEnum);


        // Movement
        public override bool CanContain(Piece piece) => PlayerIdEnum != piece.PlayerIdEnum;


        // Piece strength

        public override int GetDefenderStrength(Piece piece)
            => throw new InvalidGameStateException("Cannot get piece strength in cave.");

        public override int GetAttackerStrength(Piece attacker, Piece defender, GameField defenderField)
            => throw new InvalidGameStateException("Cannot get piece strength in cave.");

        public override int GetAttackerStrengthBasedOnFields(Mouse attacker, Lake attackerField)
            => throw new InvalidGameStateException("Cannot find strength for mouse attack from lake to cave, because there is no such move possible.");

        public override int GetAttackerStrengthBasedOnFields(Dog attacker, Lake attackerField)
            => throw new InvalidGameStateException("Cannot find strength for dog attack from lake to cave, because there is no such move possible.");

        public override int GetAttackerStrengthBasedOnFields(Wolf attacker, Lake attackerField)
            => throw new InvalidGameStateException("Cannot find strength for wolf attack from lake to cave, because there is no such move possible.");
    }
}
