using JungleMCTS.Enums;

namespace JungleMCTS.GamePiece.Pieces
{
    public abstract class SwimmingPiece : Piece
    {
        public static readonly int MaxTimeInWater = 3;

        public int TimeInWater { get; set; } = 0;

        protected SwimmingPiece(int initialStrength, PlayerIdEnum playerIdEnum)
            : base(initialStrength, playerIdEnum) { }

        public void ExtendWaterTime() => ++TimeInWater;

        public void RestoreWaterTime() => TimeInWater = 0;

        public bool IsDrowned() => TimeInWater >= MaxTimeInWater;
    }
}
