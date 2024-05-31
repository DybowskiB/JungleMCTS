using JungleMCTS.GamePiece;

namespace JungleMCTS.GameBoard.Controllers
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
                int possibleX = defaultPossiblePosition.X;
                int possibleY = defaultPossiblePosition.Y;

                // Check if field can contain piece and is free
                bool canFieldContainPiece = board.Fields[possibleX, possibleY]!.CanContain(piece);
                bool isFieldFree = board.Pieces[possibleX, possibleY] is null;
                if (canFieldContainPiece && isFieldFree)
                {
                    possiblePositions.Add(defaultPossiblePosition);
                    continue;
                }

                // If field is not free, check whether piece on field can be captured
                if (canFieldContainPiece)
                {
                    bool canCapture = CaptureController.CanCapture(
                        board.Pieces[currentPosition.X, currentPosition.Y]!,
                        board.Fields[currentPosition.X, currentPosition.Y]!,
                        board.Pieces[possibleX, possibleY]!,
                        board.Fields[possibleX, possibleY]!);
                    if (canCapture)
                    {
                        possiblePositions.Add(defaultPossiblePosition);
                    }
                }
            }

            return possiblePositions;
        }

        public static List<Position> GetPossibleVerticalJumpPositions(Position currentPosition, Board board)
        {
            List<Position> possiblePositions = [];
            HashSet<int> jumpYCoordinates = [1, 2, 4, 5];

            if (currentPosition.X == 2 && jumpYCoordinates.Contains(currentPosition.Y))
            {
                Position newPosition = new(currentPosition.X + 4, currentPosition.Y);
                GetJumpPositions(currentPosition, newPosition, board, possiblePositions);
                return possiblePositions;
            }

            if (currentPosition.X == 6 && jumpYCoordinates.Contains(currentPosition.Y))
            {
                Position newPosition = new(currentPosition.X - 4, currentPosition.Y);
                GetJumpPositions(currentPosition, newPosition, board, possiblePositions);
                return possiblePositions;
            }

            return [];
        }

        public static List<Position> GetPossibleHorizontalJumpPositions(Position currentPosition, Board board)
        {
            List<Position> possiblePositions = [];
            HashSet<int> jumpXCoordinates = [3, 4, 5];

            if (currentPosition.Y == 0 && jumpXCoordinates.Contains(currentPosition.X))
            {
                Position newPosition = new(currentPosition.X, currentPosition.Y + 3);
                GetJumpPositions(currentPosition, newPosition, board, possiblePositions);
                return possiblePositions;
            }

            if (currentPosition.Y == 3 && jumpXCoordinates.Contains(currentPosition.X))
            {
                Position newPosition1 = new(currentPosition.X, currentPosition.Y + 3);
                GetJumpPositions(currentPosition, newPosition1, board, possiblePositions);
                Position newPosition2 = new(currentPosition.X, currentPosition.Y - 3);
                GetJumpPositions(currentPosition, newPosition2, board, possiblePositions);
                return possiblePositions;
            }

            if (currentPosition.Y == 6 && jumpXCoordinates.Contains(currentPosition.X))
            {
                Position newPosition2 = new(currentPosition.X, currentPosition.Y - 3);
                GetJumpPositions(currentPosition, newPosition2, board, possiblePositions);
                return possiblePositions;
            }

            return [];
        }

        private static void GetJumpPositions(Position currentPosition, Position newPosition, Board board, List<Position> possiblePositions)
        {
            // If field is free jump
            if (board.Pieces[newPosition.X, newPosition.Y] is null)
            {
                possiblePositions.Add(new(newPosition.X, newPosition.Y));
            }
            // Otherwise try to capture
            else
            {
                bool canCapture = CaptureController.CanCapture(
                        board.Pieces[currentPosition.X, currentPosition.Y]!,
                        board.Fields[currentPosition.X, currentPosition.Y]!,
                        board.Pieces[newPosition.X, newPosition.Y]!,
                        board.Fields[newPosition.X, newPosition.Y]!);
                if (canCapture)
                {
                    possiblePositions.Add(new(newPosition.X, newPosition.Y));
                }
            }
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
