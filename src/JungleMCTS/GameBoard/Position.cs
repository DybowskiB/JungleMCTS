namespace JungleMCTS.GameBoard
{
    public class Position
    {
        public int X { get; set; }

        public int Y { get; set; }


        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object? obj)
        {
            if (obj is Position other)
            {
                return this.X == other.X && this.Y == other.Y;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }
    }
}
