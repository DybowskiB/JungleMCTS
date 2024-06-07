using JungleMCTS.Enums;
using JungleMCTS.GameBoard;
using JungleMCTS.GamePiece.Pieces;
using JungleMCTS.Players.AutoPlayers.MctsPlayers.MctsResources;

namespace JungleMCTS.Players.AutoPlayers
{
    public class AlphaBetaPlayer : AutoPlayer
    {
        public AlphaBetaPlayer(PlayerIdEnum playerIdEnum, TimeSpan maxMoveTime)
            : base(playerIdEnum, maxMoveTime) { }

        public override void Move(Board board)
        { 
            var action = AlphaBeta(PlayerIdEnum, board, 4, double.MinValue, double.MaxValue, true).Item2;
            if (action == null)
            {
                throw new Exception("not found any move");
            }
            board.Move(action.CurrentPosition, action.NewPosition, PlayerIdEnum);
        }
        (double, MctsAction?, int) AlphaBeta(PlayerIdEnum playerId, Board board, int deepLevel, double alfa, double beta, bool isFirstLevel )
        {
            if (deepLevel == 0)
            {
                return (EvaluateBoard(board), null, 100);
            }
            if(!isFirstLevel)
            {
                var result = board.GetGameResult();
                if (result != GameResult.None)
                {
                    return (getGameResultEvalution(result), null, 0);
                }
            }
            MctsAction? chosenMove = null;
            int m = 100000; 
            if (playerId == PlayerIdEnum.SecondPlayer)
            {
                foreach (var move in getPossibleMoves(board, playerId))
                {
                    var newBoard = (Board)board.Clone();
                    newBoard.Move(move.CurrentPosition, move.NewPosition, playerId);
                    var resultAlphaBeta = AlphaBeta(PlayerIdEnum.FirstPlayer, newBoard, deepLevel - 1, alfa, beta, false);
                    var temp = resultAlphaBeta.Item1;
                    var tempM = resultAlphaBeta.Item3 + 1;
                    if (temp < beta|| (temp == beta && tempM < m))
                    {
                        beta = temp;
                        chosenMove = move;
                        m = tempM;
                    }
                    if (alfa >= beta)
                    {
                        break;
                    }
                }
                return (beta, chosenMove, m);
            }
            else
            {
                foreach(var move in getPossibleMoves(board, playerId))
                {
                    var newBoard = (Board)board.Clone();
                    newBoard.Move(move.CurrentPosition, move.NewPosition, playerId);

                    var resultAlphaBeta = AlphaBeta(PlayerIdEnum.SecondPlayer, newBoard, deepLevel - 1, alfa, beta, false);
                    var temp = resultAlphaBeta.Item1;
                    var tempM = resultAlphaBeta.Item3 + 1;
                    if (temp > alfa || (temp == alfa && tempM < m))
                    {
                        alfa = temp;
                        chosenMove = move;
                        m = tempM;
                    }
                    if (alfa >= beta)
                    {
                        break;
                    }
                }
                return (alfa, chosenMove, m);
            }
        }


        double EvaluateBoard(Board board)
        {
            double value = 0;
            for (int i = 0; i < Board.BoardLength; i++)
            {
                for (int j = 0; j < Board.BoardWidth; j++)
                {
                    if (board.Pieces[i, j] is null) { continue; }
                    var piece = board.Pieces[i, j];
                    if (piece!.PlayerIdEnum == PlayerIdEnum.FirstPlayer)
                    {
                        value += piece.DefaultStrength*100 + (i+1)*10 + (3 - Math.Abs(j - 3));
                    }
                    if (piece.PlayerIdEnum == PlayerIdEnum.SecondPlayer)
                    {
                        value -= (piece.DefaultStrength*100 + (Board.BoardLength - i)*10 + (3 - Math.Abs(j - 3)));
                    }
                }
            }
            var result = board.GetGameResult();
            if (result != GameResult.None)
            {
                return getGameResultEvalution(result);
            }
            return value;
        }

        static double getGameResultEvalution(GameResult result)
        {
            if (result == GameResult.FirstPlayerWins)
            {
                return +1000000;
            }
            if (result == GameResult.SecondPlayerWins)
            {
                return -1000000;
            }
            return 0;
        }

        static IEnumerable<MctsAction> getPossibleMoves(Board board, PlayerIdEnum playerId)
        {
            for (int x = 0; x < Board.BoardLength; ++x)
            {
                for (int y = 0; y < Board.BoardWidth; ++y)
                {
                    if (board.Pieces[x, y] is null) continue;
                    if (board.Pieces[x, y]!.PlayerIdEnum != playerId) continue;
                    var position = new Position(x, y);
                    var possiblePositions = board.Pieces[x, y]!.GetPossiblePositions(position, board);
                    Shuffle<Position>(possiblePositions);
                    foreach (var newPosition in possiblePositions)
                    {
                        yield return new MctsAction(position, newPosition);
                    }
                }
            }
        }

        private static Random rng = new Random();
        public static void Shuffle<T>(List<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
