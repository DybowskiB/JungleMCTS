using JungleMCTS.Enums;
using JungleMCTS.GameBoard;
using JungleMCTS.Players.AutoPlayers.MctsPlayers.MctsResources;

namespace JungleMCTS.Players.AutoPlayers.MctsPlayers
{
    public class MctsBeamSearchPlayer : AutoPlayer
    {
        private readonly double _c = Math.Sqrt(2);
        private readonly Random _random = new();
        private readonly int _beamWidth;
        private readonly Dictionary<int, int> _beamWidthDictionary = [];

        public MctsBeamSearchPlayer(PlayerIdEnum playerIdEnum, TimeSpan maxMoveTime, int beamWidth)
            : base(playerIdEnum, maxMoveTime) 
        { 
            _beamWidth = beamWidth;
        }

        public override void Move(Board board)
        {
            MctsUctNode root = new(null, board, GetAvailableActions(board, PlayerIdEnum), null);
            // Initialize dictionary
            _beamWidthDictionary.Add(0, 1);
            _beamWidthDictionary.Add(1, 0);
            MctsAction action = Search(root);
            board.Move(action.CurrentPosition, action.NewPosition);
        }

        private MctsAction Search(MctsUctNode root)
        {
            DateTime startTime = DateTime.Now;
            DateTime endTime = startTime + _maxMoveTime;
            while (startTime < endTime)
            {
                MctsUctNode? node = root;
                int nodeChildLevel = 1;
                int numberOfNodesOnChildLevel = _beamWidthDictionary[1];

                // Selection
                while ((node.UntriedActions.Count == 0 || numberOfNodesOnChildLevel > _beamWidth) 
                    && node.Children.Count != 0)
                {
                    node = node.SelectChild(_c);
                    ++nodeChildLevel;
                    if (_beamWidthDictionary.TryGetValue(nodeChildLevel, out var numberOfNodes))
                    {
                        numberOfNodesOnChildLevel = numberOfNodes;
                    }
                }

                // Expansion
                if (node.UntriedActions.Count != 0)
                {
                    MctsAction action = node.UntriedActions[_random.Next(node.UntriedActions.Count)];
                    node.UntriedActions.Remove(action);
                    Board clonedBoard = node.Board.Clone() as Board
                        ?? throw new NullReferenceException("Cannot create board copy.");
                    clonedBoard.Move(action.CurrentPosition, action.NewPosition);
                    MctsUctNode child = new(node, clonedBoard, GetAvailableActions(clonedBoard, PlayerIdEnum), action);
                    node.AddChild(child);
                    if (_beamWidthDictionary.TryGetValue(nodeChildLevel, out int value))
                    {
                        _beamWidthDictionary[nodeChildLevel] = ++value;
                    }
                    node = child;
                }

                // Simulation
                double result = Simulate(node);

                // Backpropagation
                while (node != null)
                {
                    node.Visits++;
                    node.Wins += result;
                    node = node.Parent;
                }
            }

            return root.Children.OrderByDescending(c => c.Visits).First().Action!;
        }

        private double Simulate(MctsUctNode node)
        {
            // Run simulation
            Board board = (Board)node.Board.Clone();
            bool isFirstPlayer = PlayerIdEnum == PlayerIdEnum.FirstPlayer;
            // Opponent starts
            var currentPlayer = isFirstPlayer ? PlayerIdEnum.SecondPlayer : PlayerIdEnum.FirstPlayer;
            var gameResult = board.GetGameResult();
            while (gameResult == GameResult.None)
            {
                gameResult = SimulateMove(board, currentPlayer);
                currentPlayer = (PlayerIdEnum)((int)currentPlayer ^ 1);
            }

            // Choose result
            bool isFirstPlayerWinner = gameResult == GameResult.FirstPlayerWins;
            bool isSecondPlayerWinner = gameResult == GameResult.SecondPlayerWins;
            if ((isFirstPlayerWinner && isFirstPlayer) || (isSecondPlayerWinner && !isFirstPlayer))
            {
                return 1;
            }
            if ((isFirstPlayerWinner && !isFirstPlayer) || (isSecondPlayerWinner && isFirstPlayer))
            {
                return -1;
            }
            return 0;
        }

        private GameResult SimulateMove(Board board, PlayerIdEnum playerId)
        {
            List<MctsAction> possibleActions = GetAvailableActions(board, playerId);
            if (possibleActions.Count == 0)
                return GameResult.Draw;
            MctsAction action = possibleActions[_random.Next(possibleActions.Count)];
            board.Move(action.CurrentPosition, action.NewPosition);
            return board.GetGameResult();
        }

        private static List<MctsAction> GetAvailableActions(Board board, PlayerIdEnum playerIdEnum)
        {
            List<MctsAction> availableActions = [];
            for (int x = 0; x < Board.BoardLength; ++x)
            {
                for (int y = 0; y < Board.BoardWidth; ++y)
                {
                    if (board.Pieces[x, y] is null) continue;
                    if (board.Pieces[x, y]!.PlayerIdEnum != playerIdEnum) continue;
                    var position = new Position(x, y);
                    var possiblePositions = board.Pieces[x, y]!.GetPossiblePositions(position, board);
                    foreach (var newPosition in possiblePositions)
                    {
                        availableActions.Add(new MctsAction(position, newPosition));
                    }
                }
            }
            return availableActions;
        }
    }
}
