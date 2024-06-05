using JungleMCTS.Enums;

namespace JungleMCTS.GamePiece.Pieces
{
    public abstract class SwimmingPiece : Piece
    {
        private const int maxTimeInWater = 3;

        private int timeInWater = 0;

        protected SwimmingPiece(int initialStrength, PlayerIdEnum playerIdEnum)
            : base(initialStrength, playerIdEnum) { }

        public void ExtendWaterTime() => ++timeInWater;

        public void RestoreWaterTime() => timeInWater = 0;

        public bool IsDrowned() => timeInWater > maxTimeInWater;
    }
}
