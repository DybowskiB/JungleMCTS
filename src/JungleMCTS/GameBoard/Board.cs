using JungleMCTS.GameBoard.GameFields;
using JungleMCTS.Enums;
using JungleMCTS.GamePiece;
using JungleMCTS.GamePiece.Pieces;
using JungleMCTS.Exceptions;
using System.Text;
using JungleMCTS.Players.AutoPlayers.MctsPlayers;

namespace JungleMCTS.GameBoard
{
    public class Board : ICloneable
    {
        private readonly int _maxMoveWithoutCapturing = 30;
        private readonly int _maxPositionCount = 3;
        private readonly Dictionary<string, int> _positionDictionary = new();
        private int _movesWithoutCapturing = 0;

        public static int BoardLength { get; } = 9;

        public static int BoardWidth { get; } = 7;

        public Piece?[,] Pieces { get; } = new Piece?[BoardLength, BoardWidth];

        public GameField?[,] Fields { get; } = new GameField?[BoardLength, BoardWidth];


        public Board()
        {
            InitializePieces();
            InitializeFields();
            UpdatePositions(GetPositionKey(Pieces));
        }

        public Board(int movesWithoutCapturing)
        {
            _movesWithoutCapturing = movesWithoutCapturing;
        }


        public object Clone()
        {
            var clonedBoard = new Board(_movesWithoutCapturing);

            // Cloning pieces
            for (int i = 0; i < BoardLength; i++)
            {
                for (int j = 0; j < BoardWidth; j++)
                {
                    if (Pieces[i, j] != null)
                    {
                        clonedBoard.Pieces[i, j] = Pieces[i, j] is not null ? 
                            (Piece)Pieces[i, j]!.Clone() : null;
                    }
                }
            }

            // Cloning fields
            for (int i = 0; i < BoardLength; i++)
            {
                for (int j = 0; j < BoardWidth; j++)
                {
                    clonedBoard.Fields[i, j] = (GameField)Fields[i, j]!.Clone();
                }
            }

            // Cloning position dictionary
            foreach (var kvp in _positionDictionary)
            {
                clonedBoard._positionDictionary[kvp.Key] = kvp.Value;
            }

            return clonedBoard;
        }

        public GameResult GetGameResult()
        {
            if (Pieces[0, 3] != null || MctsUctPlayer.GetAvailableActions(this, PlayerIdEnum.FirstPlayer).Count == 0)
                return GameResult.SecondPlayerWins;
            if (Pieces[8, 3] != null || MctsUctPlayer.GetAvailableActions(this, PlayerIdEnum.SecondPlayer).Count == 0)
                return GameResult.FirstPlayerWins;
            if (_movesWithoutCapturing >= _maxMoveWithoutCapturing)
                return GameResult.DrawBecauseOfNotCapturing;
            if (_positionDictionary[GetPositionKey(Pieces)] >= _maxPositionCount)
                return GameResult.DrawBecauseOfSamePositions;
            return GameResult.None;
        }


        public void Move(Position from, Position to)
        {
            if (Pieces[from.X, from.Y] == null)
                throw new InvalidGameStateException("Cannot move from empty field.");
            if (Pieces[to.X, to.Y] == null)
                ++_movesWithoutCapturing;
            else
                _movesWithoutCapturing = 0;
            Pieces[to.X, to.Y] = Pieces[from.X, from.Y];
            Pieces[from.X, from.Y] = null;
            // Update board state
            RemovePiecesSwimmingLong();
            UpdatePositions(GetPositionKey(Pieces));
        }


        private void InitializePieces()
        {
            for(int i = 0; i < BoardLength; ++i)
            {
                for(int j = 0; j < BoardWidth; ++j)
                {
                    Pieces[i, j] = null;
                }
            }

            // Elephant positions
            Pieces[2, 0] = new Elephant(PlayerIdEnum.FirstPlayer);
            Pieces[6, 6] = new Elephant(PlayerIdEnum.SecondPlayer);

            // Lion positions
            Pieces[0, 6] = new Lion(PlayerIdEnum.FirstPlayer);
            Pieces[8, 0] = new Lion(PlayerIdEnum.SecondPlayer);

            // Tiger positions
            Pieces[0, 0] = new Tiger(PlayerIdEnum.FirstPlayer);
            Pieces[8, 6] = new Tiger(PlayerIdEnum.SecondPlayer);

            // Cheetah positions
            Pieces[2, 4] = new Cheetah(PlayerIdEnum.FirstPlayer);
            Pieces[6, 2] = new Cheetah(PlayerIdEnum.SecondPlayer);

            // Wolf positions
            Pieces[2, 2] = new Wolf(PlayerIdEnum.FirstPlayer);
            Pieces[6, 4] = new Wolf(PlayerIdEnum.SecondPlayer);

            // Dog positions 
            Pieces[1, 5] = new Dog(PlayerIdEnum.FirstPlayer);
            Pieces[7, 1] = new Dog(PlayerIdEnum.SecondPlayer);

            // Cat positions
            Pieces[1, 1] = new Cat(PlayerIdEnum.FirstPlayer);
            Pieces[7, 5] = new Cat(PlayerIdEnum.SecondPlayer);

            // Mouse positions
            Pieces[2, 6] = new Mouse(PlayerIdEnum.FirstPlayer);
            Pieces[6, 0] = new Mouse(PlayerIdEnum.SecondPlayer);
        }

        private void InitializeFields()
        {
            for (int i = 0; i < BoardLength; ++i)
            {
                for (int j = 0; j < BoardWidth; ++j)
                {
                    Fields[i, j] = new DefaultField();
                }
            }

            // Cave positions
            Fields[0, 3] = new Cave(PlayerIdEnum.FirstPlayer);
            Fields[8, 3] = new Cave(PlayerIdEnum.SecondPlayer);

            // Trap positions
            Fields[0, 2] = new Trap(PlayerIdEnum.FirstPlayer);
            Fields[0, 4] = new Trap(PlayerIdEnum.FirstPlayer);
            Fields[1, 3] = new Trap(PlayerIdEnum.FirstPlayer);

            Fields[8, 2] = new Trap(PlayerIdEnum.SecondPlayer);
            Fields[8, 4] = new Trap(PlayerIdEnum.SecondPlayer);
            Fields[7, 3] = new Trap(PlayerIdEnum.SecondPlayer);

            // Lake positions
            CreateLake(3, 1);
            CreateLake(3, 4);
        }

        private void CreateLake(int startX, int startY)
        {
            for (int i = startX; i < startX + 3; ++i)
            {
                for (int j = startY; j < startY + 2; ++j)
                {
                    Fields[i, j] = new Lake();
                }
            }
        }


        private void RemovePiecesSwimmingLong()
        {
            List<int> lakeXIndexes = [3, 4, 5];
            List<int> lakeYIndexes = [1, 2, 4, 5];
            for(int x = 0; x < BoardLength; ++x)
            {
                for(int y = 0; y < BoardWidth; ++y)
                {
                    if (Pieces[x, y] is not SwimmingPiece)
                        continue;
                    var swimmingPiece = Pieces[x, y] as SwimmingPiece;
                    if (lakeXIndexes.Contains(x) && lakeYIndexes.Contains(y))
                    {
                        if (swimmingPiece!.IsDrowned())
                        {
                            Pieces[x, y] = null;
                        }
                        else
                        {
                            swimmingPiece.ExtendWaterTime();
                        }
                        continue;
                    }
                    swimmingPiece!.RestoreWaterTime();
                }
            }
        }

        private void UpdatePositions(string positionKey)
        {
            if (_positionDictionary.TryGetValue(positionKey, out int value))
            {
                _positionDictionary[positionKey] = ++value;
                return;
            }
            _positionDictionary.Add(positionKey, 0);
        }


        private static string GetPositionKey(Piece?[,] pieces)
        {
            StringBuilder positionsKey = new();
            for (int x = 0; x < BoardLength; ++x)
            {
                for (int y = 0; y < BoardWidth; ++y)
                {
                    if (pieces[x, y] is not null)
                    {
                        positionsKey.Append((int)pieces[x, y]!.PlayerIdEnum);
                        positionsKey.Append(pieces[x, y]!.DefaultStrength);
                        continue;
                    }
                    positionsKey.Append("00");
                }
            }
            return positionsKey.ToString();
        }
    }
}
