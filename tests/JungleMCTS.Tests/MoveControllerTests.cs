using JungleMCTS.GamePiece.Pieces;
using JungleMCTS.GamePiece;
using JungleMCTS.GameBoard;
using JungleMCTS.GameBoard.Controllers;

namespace JungleMCTS.Tests
{
    public class MoveControllerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ShouldPossiblePositionsBeCorrect()
        {
            // Board board
            Board board = new();

            // Clear board from pieces
            for (int x = 0; x < Board.BoardLength; ++x)
            {
                for (int y = 0; y < Board.BoardWidth; ++y)
                {
                    board.Pieces[x, y] = null;
                }
            }

            // Player pieces
            Mouse mouse = new(Enums.PlayerIdEnum.FirstPlayer);
            Cat cat = new(Enums.PlayerIdEnum.FirstPlayer);
            Dog dog = new(Enums.PlayerIdEnum.FirstPlayer);
            Wolf wolf = new(Enums.PlayerIdEnum.FirstPlayer);
            Cheetah cheetah = new(Enums.PlayerIdEnum.FirstPlayer);
            Tiger tiger = new(Enums.PlayerIdEnum.FirstPlayer);
            Lion lion = new(Enums.PlayerIdEnum.FirstPlayer);
            Elephant elephant = new(Enums.PlayerIdEnum.FirstPlayer);

            // Lists of pieces
            List<Piece> player1Pieces = [mouse, cat, dog, wolf, cheetah, tiger, lion, elephant];

            // Positions
            Position currentPosition;
            List<Position> expectedPossiblePositions;


            //
            // Positions for default fields around default fields
            //

            // Position = (1, 1)
            currentPosition = new(1, 1);
            expectedPossiblePositions =
                [
                    new Position(1, 0),
                    new Position(2, 1),
                    new Position(0, 1),
                    new Position(1, 2)
                ];
            foreach (var piece in player1Pieces)
            {
                var tempPiece = board.Pieces[currentPosition.X, currentPosition.Y];
                board.Pieces[currentPosition.X, currentPosition.Y] = piece;
                var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
                board.Pieces[currentPosition.X, currentPosition.Y] = tempPiece;
                CollectionAssert.AreEquivalent(expectedPossiblePositions, possiblePositions);
            }

            // Position = (7, 1)
            currentPosition = new(7, 1);
            expectedPossiblePositions =
                [
                    new Position(8, 1),
                    new Position(6, 1),
                    new Position(7, 0),
                    new Position(7, 2)
                ];
            foreach (var piece in player1Pieces)
            {
                var tempPiece = board.Pieces[currentPosition.X, currentPosition.Y];
                board.Pieces[currentPosition.X, currentPosition.Y] = piece;
                var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
                board.Pieces[currentPosition.X, currentPosition.Y] = tempPiece;
                CollectionAssert.AreEquivalent(expectedPossiblePositions, possiblePositions);
            }

            // Position = (1, 5)
            currentPosition = new(1, 5);
            expectedPossiblePositions =
                [
                    new Position(0, 5),
                    new Position(2, 5),
                    new Position(1, 6),
                    new Position(1, 4)
                ];
            foreach (var piece in player1Pieces)
            {
                var tempPiece = board.Pieces[currentPosition.X, currentPosition.Y];
                board.Pieces[currentPosition.X, currentPosition.Y] = piece;
                var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
                board.Pieces[currentPosition.X, currentPosition.Y] = tempPiece;
                CollectionAssert.AreEquivalent(expectedPossiblePositions, possiblePositions);
            }

            // Position = (7, 5)
            currentPosition = new(7, 5);
            expectedPossiblePositions =
                [
                    new Position(7, 4),
                    new Position(7, 6),
                    new Position(6, 5),
                    new Position(8, 5)
                ];
            foreach (var piece in player1Pieces)
            {
                var tempPiece = board.Pieces[currentPosition.X, currentPosition.Y];
                board.Pieces[currentPosition.X, currentPosition.Y] = piece;
                var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
                board.Pieces[currentPosition.X, currentPosition.Y] = tempPiece;
                CollectionAssert.AreEquivalent(expectedPossiblePositions, possiblePositions);
            }


            //
            // Corner positions
            //

            // Position = (0, 0)
            currentPosition = new(0, 0);
            expectedPossiblePositions =
                [
                    new Position(0, 1),
                    new Position(1, 0)
                ];
            foreach (var piece in player1Pieces)
            {
                var tempPiece = board.Pieces[currentPosition.X, currentPosition.Y];
                board.Pieces[currentPosition.X, currentPosition.Y] = piece;
                var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
                board.Pieces[currentPosition.X, currentPosition.Y] = tempPiece;
                CollectionAssert.AreEquivalent(expectedPossiblePositions, possiblePositions);
            }

            // Position = (0, 6)
            currentPosition = new(0, 6);
            expectedPossiblePositions =
                [
                    new Position(0, 5),
                    new Position(1, 6)
                ];
            foreach (var piece in player1Pieces)
            {
                var tempPiece = board.Pieces[currentPosition.X, currentPosition.Y];
                board.Pieces[currentPosition.X, currentPosition.Y] = piece;
                var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
                board.Pieces[currentPosition.X, currentPosition.Y] = tempPiece;
                CollectionAssert.AreEquivalent(expectedPossiblePositions, possiblePositions);
            }

            // Position = (8, 0)
            currentPosition = new(8, 0);
            expectedPossiblePositions =
                [
                    new Position(7, 0),
                    new Position(8, 1)
                ];
            foreach (var piece in player1Pieces)
            {
                var tempPiece = board.Pieces[currentPosition.X, currentPosition.Y];
                board.Pieces[currentPosition.X, currentPosition.Y] = piece;
                var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
                board.Pieces[currentPosition.X, currentPosition.Y] = tempPiece;
                CollectionAssert.AreEquivalent(expectedPossiblePositions, possiblePositions);
            }

            // Position = (8, 6)
            currentPosition = new(8, 6);
            expectedPossiblePositions =
                [
                    new Position(8, 5),
                    new Position(7, 6)
                ];
            foreach (var piece in player1Pieces)
            {
                var tempPiece = board.Pieces[currentPosition.X, currentPosition.Y];
                board.Pieces[currentPosition.X, currentPosition.Y] = piece;
                var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
                board.Pieces[currentPosition.X, currentPosition.Y] = tempPiece;
                CollectionAssert.AreEquivalent(expectedPossiblePositions, possiblePositions);
            }


            //
            // Positions near own cave
            //

            List<Piece> nearOwnCaveAllowedPieces = [mouse, cat, dog];
            // Position = (0, 2)
            currentPosition = new(0, 2);
            expectedPossiblePositions =
                [
                    new Position(0, 1),
                    new Position(1, 2)
                ];
            foreach (var piece in nearOwnCaveAllowedPieces)
            {
                var tempPiece = board.Pieces[currentPosition.X, currentPosition.Y];
                board.Pieces[currentPosition.X, currentPosition.Y] = piece;
                var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
                board.Pieces[currentPosition.X, currentPosition.Y] = tempPiece;
                CollectionAssert.AreEquivalent(expectedPossiblePositions, possiblePositions);
            }

            // Position = (1, 3)
            currentPosition = new(1, 3);
            expectedPossiblePositions =
                [
                    new Position(1, 2),
                    new Position(2, 3),
                    new Position(1, 4)
                ];
            foreach (var piece in nearOwnCaveAllowedPieces)
            {
                var tempPiece = board.Pieces[currentPosition.X, currentPosition.Y];
                board.Pieces[currentPosition.X, currentPosition.Y] = piece;
                var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
                board.Pieces[currentPosition.X, currentPosition.Y] = tempPiece;
                CollectionAssert.AreEquivalent(expectedPossiblePositions, possiblePositions);
            }

            // Position = (0, 4)
            currentPosition = new(0, 4);
            expectedPossiblePositions =
                [
                    new Position(0, 5),
                    new Position(1, 4)
                ];
            foreach (var piece in nearOwnCaveAllowedPieces)
            {
                var tempPiece = board.Pieces[currentPosition.X, currentPosition.Y];
                board.Pieces[currentPosition.X, currentPosition.Y] = piece;
                var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
                board.Pieces[currentPosition.X, currentPosition.Y] = tempPiece;
                CollectionAssert.AreEquivalent(expectedPossiblePositions, possiblePositions);
            }


            //
            // Positions near own traps (divide into two cases: { mouse, cat, dog } and { wolf, cheetah, tiger, lion, elephant } 
            //

            List<Piece> nearOwnCaveDisallowedPieces = [wolf, cheetah, tiger, lion, elephant];

            // Position = (0, 1)
            currentPosition = new(0, 1);
            // { mouse, cat, dog }
            expectedPossiblePositions =
                [
                    new Position(0, 0),
                    new Position(1, 1),
                    new Position(0, 2)
                ];
            foreach (var piece in nearOwnCaveAllowedPieces)
            {
                var tempPiece = board.Pieces[currentPosition.X, currentPosition.Y];
                board.Pieces[currentPosition.X, currentPosition.Y] = piece;
                var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
                board.Pieces[currentPosition.X, currentPosition.Y] = tempPiece;
                CollectionAssert.AreEquivalent(expectedPossiblePositions, possiblePositions);
            }
            // { wolf, cheetah, tiger, lion, elephant } 
            expectedPossiblePositions =
                [
                    new Position(0, 0),
                    new Position(1, 1)
                ];
            foreach (var piece in nearOwnCaveDisallowedPieces)
            {
                var tempPiece = board.Pieces[currentPosition.X, currentPosition.Y];
                board.Pieces[currentPosition.X, currentPosition.Y] = piece;
                var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
                board.Pieces[currentPosition.X, currentPosition.Y] = tempPiece;
                CollectionAssert.AreEquivalent(expectedPossiblePositions, possiblePositions);
            }

            // Position = (2, 3)
            currentPosition = new(2, 3);
            // { mouse, cat, dog }
            expectedPossiblePositions =
                [
                    new Position(2, 2),
                    new Position(2, 4),
                    new Position(1, 3),
                    new Position(3, 3)
                ];
            foreach (var piece in nearOwnCaveAllowedPieces)
            {
                var tempPiece = board.Pieces[currentPosition.X, currentPosition.Y];
                board.Pieces[currentPosition.X, currentPosition.Y] = piece;
                var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
                board.Pieces[currentPosition.X, currentPosition.Y] = tempPiece;
                CollectionAssert.AreEquivalent(expectedPossiblePositions, possiblePositions);
            }
            // { wolf, cheetah, tiger, lion, elephant } 
            expectedPossiblePositions =
                [
                    new Position(2, 2),
                    new Position(2, 4),
                    new Position(3, 3)
                ];
            foreach (var piece in nearOwnCaveDisallowedPieces)
            {
                var tempPiece = board.Pieces[currentPosition.X, currentPosition.Y];
                board.Pieces[currentPosition.X, currentPosition.Y] = piece;
                var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
                board.Pieces[currentPosition.X, currentPosition.Y] = tempPiece;
                CollectionAssert.AreEquivalent(expectedPossiblePositions, possiblePositions);
            }

            // Position = (1, 4)
            currentPosition = new(1, 4);
            // { mouse, cat, dog }
            expectedPossiblePositions =
                [
                    new Position(0, 4),
                    new Position(2, 4),
                    new Position(1, 3),
                    new Position(1, 5)
                ];
            foreach (var piece in nearOwnCaveAllowedPieces)
            {
                var tempPiece = board.Pieces[currentPosition.X, currentPosition.Y];
                board.Pieces[currentPosition.X, currentPosition.Y] = piece;
                var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
                board.Pieces[currentPosition.X, currentPosition.Y] = tempPiece;
                CollectionAssert.AreEquivalent(expectedPossiblePositions, possiblePositions);
            }
            // { wolf, cheetah, tiger, lion, elephant } 
            expectedPossiblePositions =
                [
                    new Position(2, 4),
                    new Position(1, 5)
                ];
            foreach (var piece in nearOwnCaveDisallowedPieces)
            {
                var tempPiece = board.Pieces[currentPosition.X, currentPosition.Y];
                board.Pieces[currentPosition.X, currentPosition.Y] = piece;
                var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
                board.Pieces[currentPosition.X, currentPosition.Y] = tempPiece;
                CollectionAssert.AreEquivalent(expectedPossiblePositions, possiblePositions);
            }


            // Positions near opponent cave

            // Position = (8, 2)
            currentPosition = new(8, 2);
            expectedPossiblePositions =
                [
                    new Position(8, 1),
                    new Position(7, 2),
                    new Position(8, 3)
                ];
            foreach (var piece in player1Pieces)
            {
                var tempPiece = board.Pieces[currentPosition.X, currentPosition.Y];
                board.Pieces[currentPosition.X, currentPosition.Y] = piece;
                var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
                board.Pieces[currentPosition.X, currentPosition.Y] = tempPiece;
                CollectionAssert.AreEquivalent(expectedPossiblePositions, possiblePositions);
            }

            // Position = (7, 3)
            currentPosition = new(7, 3);
            expectedPossiblePositions =
                [
                    new Position(7, 2),
                    new Position(7, 4),
                    new Position(6, 3),
                    new Position(8, 3)
                ];
            foreach (var piece in player1Pieces)
            {
                var tempPiece = board.Pieces[currentPosition.X, currentPosition.Y];
                board.Pieces[currentPosition.X, currentPosition.Y] = piece;
                var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
                board.Pieces[currentPosition.X, currentPosition.Y] = tempPiece;
                CollectionAssert.AreEquivalent(expectedPossiblePositions, possiblePositions);
            }

            // Position = (8, 4)
            currentPosition = new(8, 4);
            expectedPossiblePositions =
                [
                    new Position(8, 3),
                    new Position(8, 5),
                    new Position(7, 4)
                ];
            foreach (var piece in player1Pieces)
            {
                var tempPiece = board.Pieces[currentPosition.X, currentPosition.Y];
                board.Pieces[currentPosition.X, currentPosition.Y] = piece;
                var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
                board.Pieces[currentPosition.X, currentPosition.Y] = tempPiece;
                CollectionAssert.AreEquivalent(expectedPossiblePositions, possiblePositions);
            }


            //
            // Positions near lake for swimming pieces
            //


            // Near lake y indexes
            List<int> yIndexes = [1, 2, 4, 5];

            List<Piece> swimmingPieces = [mouse, dog, wolf];
            foreach (var piece in swimmingPieces)
            {
                // Top edge
                for (int x = 3; x < 6; ++x)
                {
                    currentPosition = new Position(x, 0);
                    expectedPossiblePositions =
                        [
                            new Position(x - 1, 0),
                            new Position(x + 1, 0),
                            new Position(x, 1),
                        ];
                    var tempPiece = board.Pieces[currentPosition.X, currentPosition.Y];
                    board.Pieces[currentPosition.X, currentPosition.Y] = piece;
                    var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
                    board.Pieces[currentPosition.X, currentPosition.Y] = tempPiece;
                    CollectionAssert.AreEquivalent(expectedPossiblePositions, possiblePositions);
                }

                // Middle line
                for (int x = 3; x < 6; ++x)
                {
                    currentPosition = new Position(x, 3);
                    expectedPossiblePositions =
                        [
                            new Position(x - 1, 3),
                            new Position(x + 1, 3),
                            new Position(x, 2),
                            new Position(x, 4)
                        ];
                    var tempPiece = board.Pieces[currentPosition.X, currentPosition.Y];
                    board.Pieces[currentPosition.X, currentPosition.Y] = piece;
                    var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
                    board.Pieces[currentPosition.X, currentPosition.Y] = tempPiece;
                    CollectionAssert.AreEquivalent(expectedPossiblePositions, possiblePositions);
                }

                // Bottom edge
                for (int x = 3; x < 6; ++x)
                {
                    currentPosition = new Position(x, 6);
                    expectedPossiblePositions =
                        [
                            new Position(x - 1, 6),
                            new Position(x + 1, 6),
                            new Position(x, 5),
                        ];
                    var tempPiece = board.Pieces[currentPosition.X, currentPosition.Y];
                    board.Pieces[currentPosition.X, currentPosition.Y] = piece;
                    var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
                    board.Pieces[currentPosition.X, currentPosition.Y] = tempPiece;
                    CollectionAssert.AreEquivalent(expectedPossiblePositions, possiblePositions);
                }

                // To the left of the lake
                foreach (int y in yIndexes)
                {
                    currentPosition = new Position(2, y);
                    expectedPossiblePositions =
                        [
                            new Position(2, y - 1),
                            new Position(2, y + 1),
                            new Position(1, y),
                            new Position(3, y)
                        ];
                    var tempPiece = board.Pieces[currentPosition.X, currentPosition.Y];
                    board.Pieces[currentPosition.X, currentPosition.Y] = piece;
                    var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
                    board.Pieces[currentPosition.X, currentPosition.Y] = tempPiece;
                    CollectionAssert.AreEquivalent(expectedPossiblePositions, possiblePositions);
                }

                // To the right of the lake
                foreach (int y in yIndexes)
                {
                    currentPosition = new Position(6, y);
                    expectedPossiblePositions =
                        [
                            new Position(6, y - 1),
                            new Position(6, y + 1),
                            new Position(5, y),
                            new Position(7, y)
                        ];
                    var tempPiece = board.Pieces[currentPosition.X, currentPosition.Y];
                    board.Pieces[currentPosition.X, currentPosition.Y] = piece;
                    var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
                    board.Pieces[currentPosition.X, currentPosition.Y] = tempPiece;
                    CollectionAssert.AreEquivalent(expectedPossiblePositions, possiblePositions);
                }
            }


            //
            // Positions near lake for only horizontally jumping pieces (cheetah)
            //


            // Top edge
            for (int x = 3; x < 6; ++x)
            {
                currentPosition = new Position(x, 0);
                expectedPossiblePositions =
                    [
                        new Position(x - 1, 0),
                            new Position(x + 1, 0),
                            new Position(x, 3),
                        ];
                var tempPiece = board.Pieces[currentPosition.X, currentPosition.Y];
                board.Pieces[currentPosition.X, currentPosition.Y] = cheetah;
                var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, cheetah);
                board.Pieces[currentPosition.X, currentPosition.Y] = tempPiece;
                CollectionAssert.AreEquivalent(expectedPossiblePositions, possiblePositions);
            }

            // Middle line
            for (int x = 3; x < 6; ++x)
            {
                currentPosition = new Position(x, 3);
                expectedPossiblePositions =
                    [
                        new Position(x - 1, 3),
                            new Position(x + 1, 3),
                            new Position(x, 0),
                            new Position(x, 6)
                    ];
                var tempPiece = board.Pieces[currentPosition.X, currentPosition.Y];
                board.Pieces[currentPosition.X, currentPosition.Y] = cheetah;
                var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, cheetah);
                board.Pieces[currentPosition.X, currentPosition.Y] = tempPiece;
                CollectionAssert.AreEquivalent(expectedPossiblePositions, possiblePositions);
            }

            // Bottom edge
            for (int x = 3; x < 6; ++x)
            {
                currentPosition = new Position(x, 6);
                expectedPossiblePositions =
                    [
                        new Position(x - 1, 6),
                            new Position(x + 1, 6),
                            new Position(x, 3),
                        ];
                var tempPiece = board.Pieces[currentPosition.X, currentPosition.Y];
                board.Pieces[currentPosition.X, currentPosition.Y] = cheetah;
                var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, cheetah);
                board.Pieces[currentPosition.X, currentPosition.Y] = tempPiece;
                CollectionAssert.AreEquivalent(expectedPossiblePositions, possiblePositions);
            }


            // To the left of the lake
            foreach (int y in yIndexes)
            {
                currentPosition = new Position(2, y);
                expectedPossiblePositions =
                    [
                        new Position(2, y - 1),
                            new Position(2, y + 1),
                            new Position(1, y)
                    ];
                var tempPiece = board.Pieces[currentPosition.X, currentPosition.Y];
                board.Pieces[currentPosition.X, currentPosition.Y] = cheetah;
                var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, cheetah);
                board.Pieces[currentPosition.X, currentPosition.Y] = tempPiece;
                CollectionAssert.AreEquivalent(expectedPossiblePositions, possiblePositions);
            }

            // To the right of the lake
            foreach (int y in yIndexes)
            {
                currentPosition = new Position(6, y);
                expectedPossiblePositions =
                    [
                        new Position(6, y - 1),
                            new Position(6, y + 1),
                            new Position(7, y)
                    ];
                var tempPiece = board.Pieces[currentPosition.X, currentPosition.Y];
                board.Pieces[currentPosition.X, currentPosition.Y] = cheetah;
                var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, cheetah);
                board.Pieces[currentPosition.X, currentPosition.Y] = tempPiece;
                CollectionAssert.AreEquivalent(expectedPossiblePositions, possiblePositions);
            }



            //
            // Positions near lake for vertically and horizontally jumping pieces
            //

            List<Piece> jumpingPieces = [tiger, lion];
            foreach (var piece in jumpingPieces)
            {
                // Top edge
                for (int x = 3; x < 6; ++x)
                {
                    currentPosition = new Position(x, 0);
                    expectedPossiblePositions =
                        [
                            new Position(x - 1, 0),
                            new Position(x + 1, 0),
                            new Position(x, 3)
                        ];
                    var tempPiece = board.Pieces[currentPosition.X, currentPosition.Y];
                    board.Pieces[currentPosition.X, currentPosition.Y] = piece;
                    var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
                    board.Pieces[currentPosition.X, currentPosition.Y] = tempPiece;
                    CollectionAssert.AreEquivalent(expectedPossiblePositions, possiblePositions);
                }

                // Middle line
                for (int x = 3; x < 6; ++x)
                {
                    currentPosition = new Position(x, 3);
                    expectedPossiblePositions =
                        [
                            new Position(x - 1, 3),
                            new Position(x + 1, 3),
                            new Position(x, 0),
                            new Position(x, 6)
                        ];
                    var tempPiece = board.Pieces[currentPosition.X, currentPosition.Y];
                    board.Pieces[currentPosition.X, currentPosition.Y] = piece;
                    var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
                    board.Pieces[currentPosition.X, currentPosition.Y] = tempPiece;
                    CollectionAssert.AreEquivalent(expectedPossiblePositions, possiblePositions);
                }

                // Bottom edge
                for (int x = 3; x < 6; ++x)
                {
                    currentPosition = new Position(x, 6);
                    expectedPossiblePositions =
                        [
                            new Position(x - 1, 6),
                            new Position(x + 1, 6),
                            new Position(x, 3)
                        ];
                    var tempPiece = board.Pieces[currentPosition.X, currentPosition.Y];
                    board.Pieces[currentPosition.X, currentPosition.Y] = piece;
                    var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
                    board.Pieces[currentPosition.X, currentPosition.Y] = tempPiece;
                    CollectionAssert.AreEquivalent(expectedPossiblePositions, possiblePositions);
                }

                // To the left of the lake
                foreach (int y in yIndexes)
                {
                    currentPosition = new Position(2, y);
                    expectedPossiblePositions =
                        [
                            new Position(2, y - 1),
                            new Position(2, y + 1),
                            new Position(1, y),
                            new Position(6, y)
                        ];
                    var tempPiece = board.Pieces[currentPosition.X, currentPosition.Y];
                    board.Pieces[currentPosition.X, currentPosition.Y] = piece;
                    var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
                    board.Pieces[currentPosition.X, currentPosition.Y] = tempPiece;
                    CollectionAssert.AreEquivalent(expectedPossiblePositions, possiblePositions);
                }

                // To the right of the lake
                foreach (int y in yIndexes)
                {
                    currentPosition = new Position(6, y);
                    expectedPossiblePositions =
                        [
                            new Position(6, y - 1),
                            new Position(6, y + 1),
                            new Position(2, y),
                            new Position(7, y)
                        ];
                    var tempPiece = board.Pieces[currentPosition.X, currentPosition.Y];
                    board.Pieces[currentPosition.X, currentPosition.Y] = piece;
                    var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
                    board.Pieces[currentPosition.X, currentPosition.Y] = tempPiece;
                    CollectionAssert.AreEquivalent(expectedPossiblePositions, possiblePositions);
                }
            }


            //
            // Positions for non-jumping and non-swimming pieces
            //

            List<Piece> nonJumpingNonSwimmingPieces = [cat, elephant];
            foreach (var piece in nonJumpingNonSwimmingPieces)
            {
                // Top edge
                for (int x = 3; x < 6; ++x)
                {
                    currentPosition = new Position(x, 0);
                    expectedPossiblePositions =
                        [
                            new Position(x - 1, 0),
                            new Position(x + 1, 0)
                        ];
                    var tempPiece = board.Pieces[currentPosition.X, currentPosition.Y];
                    board.Pieces[currentPosition.X, currentPosition.Y] = piece;
                    var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
                    board.Pieces[currentPosition.X, currentPosition.Y] = tempPiece;
                    CollectionAssert.AreEquivalent(expectedPossiblePositions, possiblePositions);
                }

                // Middle line
                for (int x = 3; x < 6; ++x)
                {
                    currentPosition = new Position(x, 3);
                    expectedPossiblePositions =
                        [
                            new Position(x - 1, 3),
                            new Position(x + 1, 3)
                        ];
                    var tempPiece = board.Pieces[currentPosition.X, currentPosition.Y];
                    board.Pieces[currentPosition.X, currentPosition.Y] = piece;
                    var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
                    board.Pieces[currentPosition.X, currentPosition.Y] = tempPiece;
                    CollectionAssert.AreEquivalent(expectedPossiblePositions, possiblePositions);
                }

                // Bottom edge
                for (int x = 3; x < 6; ++x)
                {
                    currentPosition = new Position(x, 6);
                    expectedPossiblePositions =
                        [
                            new Position(x - 1, 6),
                            new Position(x + 1, 6)
                        ];
                    var tempPiece = board.Pieces[currentPosition.X, currentPosition.Y];
                    board.Pieces[currentPosition.X, currentPosition.Y] = piece;
                    var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
                    board.Pieces[currentPosition.X, currentPosition.Y] = tempPiece;
                    CollectionAssert.AreEquivalent(expectedPossiblePositions, possiblePositions);
                }

                // To the left of the lake
                foreach (int y in yIndexes)
                {
                    currentPosition = new Position(2, y);
                    expectedPossiblePositions =
                        [
                            new Position(2, y - 1),
                            new Position(2, y + 1),
                            new Position(1, y)
                        ];
                    var tempPiece = board.Pieces[currentPosition.X, currentPosition.Y];
                    board.Pieces[currentPosition.X, currentPosition.Y] = piece;
                    var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
                    board.Pieces[currentPosition.X, currentPosition.Y] = tempPiece;
                    CollectionAssert.AreEquivalent(expectedPossiblePositions, possiblePositions);
                }

                // To the right of the lake
                foreach (int y in yIndexes)
                {
                    currentPosition = new Position(6, y);
                    expectedPossiblePositions =
                        [
                            new Position(6, y - 1),
                            new Position(6, y + 1),
                            new Position(7, y)
                        ];
                    var tempPiece = board.Pieces[currentPosition.X, currentPosition.Y];
                    board.Pieces[currentPosition.X, currentPosition.Y] = piece;
                    var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
                    board.Pieces[currentPosition.X, currentPosition.Y] = tempPiece;
                    CollectionAssert.AreEquivalent(expectedPossiblePositions, possiblePositions);
                }
            }

        }

        [Test]
        public void ShouldHaveNoPossibleMoves()
        {
            // Board board
            Board board = new();

            // Clear board from pieces
            for (int x = 0; x < Board.BoardLength; ++x)
            {
                for (int y = 0; y < Board.BoardWidth; ++y)
                {
                    board.Pieces[x, y] = null;
                }
            }

            // Player 1 pieces
            Mouse mouse1 = new(Enums.PlayerIdEnum.FirstPlayer);
            Cat cat1 = new(Enums.PlayerIdEnum.FirstPlayer);
            Dog dog1 = new(Enums.PlayerIdEnum.FirstPlayer);
            Wolf wolf1 = new(Enums.PlayerIdEnum.FirstPlayer);
            Cheetah cheetah1 = new(Enums.PlayerIdEnum.FirstPlayer);
            Tiger tiger1 = new(Enums.PlayerIdEnum.FirstPlayer);
            Lion lion1 = new(Enums.PlayerIdEnum.FirstPlayer);
            Elephant elephant1 = new(Enums.PlayerIdEnum.FirstPlayer);

            // Player 2 pieces
            Mouse mouse2 = new(Enums.PlayerIdEnum.SecondPlayer);
            Cat cat2 = new(Enums.PlayerIdEnum.SecondPlayer);
            Dog dog2 = new(Enums.PlayerIdEnum.SecondPlayer);
            Wolf wolf2 = new(Enums.PlayerIdEnum.SecondPlayer);
            Cheetah cheetah2 = new(Enums.PlayerIdEnum.SecondPlayer);
            Tiger tiger2 = new(Enums.PlayerIdEnum.SecondPlayer);
            Lion lion2 = new(Enums.PlayerIdEnum.SecondPlayer);
            Elephant elephant2 = new(Enums.PlayerIdEnum.SecondPlayer);

            // Lists of pieces
            List<Piece> player1Pieces = [mouse1, cat1, dog1, wolf1, cheetah1, tiger1, lion1, elephant1];
            List<Piece> player2Pieces = [mouse2, cat2, dog2, wolf2, cheetah2, tiger2, lion2, elephant2];

            // Positions
            List<Position> expectedPossiblePositions = [];
            List<Position> currentPositions =
                [
                    new Position(1, 1),
                    new Position(1, 5),
                    new Position(7, 1),
                    new Position(7, 5)
                ];


            // Test - surrounded by friendly pieces
            foreach (var currentPosition in currentPositions)
            {
                for (int i = 0; i < player1Pieces.Count; ++i)
                {
                    board.Pieces[currentPosition.X, currentPosition.Y] = player1Pieces[i];
                    board.Pieces[currentPosition.X + 1, currentPosition.Y] = player1Pieces[i];
                    board.Pieces[currentPosition.X - 1, currentPosition.Y] = player1Pieces[i];
                    board.Pieces[currentPosition.X, currentPosition.Y + 1] = player1Pieces[i];
                    board.Pieces[currentPosition.X, currentPosition.Y - 1] = player1Pieces[i];
                    var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, player1Pieces[i]);
                    CollectionAssert.AreEquivalent(expectedPossiblePositions, possiblePositions);
                }
            }

            // Test - surrounded by enemies
            foreach (var currentPosition in currentPositions)
            {
                for (int i = 0; i < player1Pieces.Count; ++i)
                {
                    for (int j = i + 1; j < player1Pieces.Count; ++j)
                    {
                        // Case when mouse attacks elephants
                        if (i == 0 && j == player1Pieces.Count - 1) continue;
                        board.Pieces[currentPosition.X, currentPosition.Y] = player1Pieces[i];
                        board.Pieces[currentPosition.X + 1, currentPosition.Y] = player2Pieces[j];
                        board.Pieces[currentPosition.X - 1, currentPosition.Y] = player2Pieces[j];
                        board.Pieces[currentPosition.X, currentPosition.Y + 1] = player2Pieces[j];
                        board.Pieces[currentPosition.X, currentPosition.Y - 1] = player2Pieces[j];
                        var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, player1Pieces[i]);
                        CollectionAssert.AreEquivalent(expectedPossiblePositions, possiblePositions);
                    }
                }
            }
        }
    }
}
