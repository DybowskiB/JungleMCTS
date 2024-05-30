using JungleMCTS.GamePiece;

namespace JungleMCTS.GameBoard
{
    public static class MoveController
    {
        public static List<Position> GetPossiblePositions(Position currentPosition, Board board, Piece piece)
            => piece.GetPossiblePositions(currentPosition, board);

        public static List<Position> GetDefaultPossiblePositions(Position currentPosition, Board board, Piece piece)
        {
            var defaultPossiblePositions = GetAdjacentPositions(currentPosition);
            List<Position> possiblePositions = [];
            foreach (var defaultPossiblePosition in defaultPossiblePositions)
            {
                if (board.Fields[defaultPossiblePosition.X, defaultPossiblePosition.Y].CanContain(piece))
                    possiblePositions.Add(defaultPossiblePosition);
            }
            return possiblePositions;
        }

        public static List<Position> GetPossibleVerticalJumpPositions(Position currentPosition)
        {
            HashSet<int> jumpYCoordinates = [1, 2, 4, 5];
            if (currentPosition.X == 2 && jumpYCoordinates.Contains(currentPosition.Y))
                return [new(currentPosition.X + 4, currentPosition.Y)];
            if (currentPosition.X == 6 && jumpYCoordinates.Contains(currentPosition.Y))
                return [new(currentPosition.X - 4, currentPosition.Y)];
            return [];
        }

        public static List<Position> GetPossibleHorizontalJumpPositions(Position currentPosition)
        {
            HashSet<int> jumpXCoordinates = [3, 4, 5];
            if (currentPosition.Y == 0 && jumpXCoordinates.Contains(currentPosition.X))
                return [new(currentPosition.X, currentPosition.Y + 3)];
            if (currentPosition.Y == 3 && jumpXCoordinates.Contains(currentPosition.X))
                return [
                        new(currentPosition.X, currentPosition.Y + 3),
                        new(currentPosition.X, currentPosition.Y - 3)
                       ];
            if (currentPosition.Y == 6 && jumpXCoordinates.Contains(currentPosition.X))
                return [new(currentPosition.X, currentPosition.Y - 3)];
            return [];
        }

        private static List<Position> GetAdjacentPositions(Position currentPosition)
        {
            List<Position> possiblePositions = [];

            // Check movements along the long edge
            if (currentPosition.X + 1 < Board.BoardLength)
                possiblePositions.Add(new Position(currentPosition.X + 1, currentPosition.Y));
            if (currentPosition.X - 1 >= 0)
                possiblePositions.Add(new Position(currentPosition.X - 1, currentPosition.Y));

            // Check movements along the short edge
            if (currentPosition.Y + 1 < Board.BoardWidth)
                possiblePositions.Add(new Position(currentPosition.X, currentPosition.Y + 1));
            if (currentPosition.Y - 1 >= 0)
                possiblePositions.Add(new Position(currentPosition.X, currentPosition.Y - 1));

            return possiblePositions;
        }
    }
}
