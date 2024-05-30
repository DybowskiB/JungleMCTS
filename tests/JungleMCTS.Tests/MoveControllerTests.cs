using JungleMCTS.GamePiece.Pieces;
using JungleMCTS.GamePiece;
using JungleMCTS.GameBoard;

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
                var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
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
                var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
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
                var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
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
                var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
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
                var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
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
                var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
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
                var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
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
                var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
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
                var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
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
                var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
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
                var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
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
                var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
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
                var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
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
                var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
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
                var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
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
                var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
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
                var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
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
                var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
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
                var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
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
                var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
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
                for(int x = 3; x < 6; ++x)
                {
                    currentPosition = new Position(x, 0);
                    expectedPossiblePositions =
                        [
                            new Position(x - 1, 0),
                            new Position(x + 1, 0),
                            new Position(x, 1),
                        ];
                    var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
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
                    var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
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
                    var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
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
                    var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
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
                    var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
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
                var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, cheetah);
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
                var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, cheetah);
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
                var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, cheetah);
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
                var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, cheetah);
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
                var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, cheetah);
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
                    var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
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
                    var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
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
                    var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
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
                    var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
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
                    var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
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
                    var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
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
                    var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
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
                    var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
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
                    var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
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
                    var possiblePositions = MoveController.GetPossiblePositions(currentPosition, board, piece);
                    CollectionAssert.AreEquivalent(expectedPossiblePositions, possiblePositions);
                }
            }

        }
    }
}
