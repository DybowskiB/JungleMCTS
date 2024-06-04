using JungleMCTS.Enums;
using JungleMCTS.GameBoard;
using JungleMCTS.GamePiece;
using JungleMCTS.GamePiece.Pieces;
using JungleMCTS.Players.AutoPlayers.MctsPlayers;
using JungleMCTS.Players.AutoPlayers.MctsPlayers.MctsResources;

namespace JungleMCTS.Players.AutoPlayers
{
    public class AlphaBetaPlayer : AutoPlayer
    {
        public AlphaBetaPlayer(PlayerIdEnum playerIdEnum, TimeSpan maxMoveTime)
            : base(playerIdEnum, maxMoveTime) { }

        public override void Move(Board board)
        { 
            var action = AlphaBeta(PlayerIdEnum, board, 5, double.MinValue, double.MaxValue, true).Item2;
            if (action == null)
            {
                return;
            }
            board.Move(action.CurrentPosition, action.NewPosition);
        }
        (double, MctsAction?) AlphaBeta(PlayerIdEnum playerId, Board board, int deepLevel, double alfa, double beta, bool isFirstLevel )
        {
            if (deepLevel == 0)
            {
                return (EvaluateBoard(board), null);
            }
            if(!isFirstLevel)
            {
                var result = board.GetGameResult();
                if (result != GameResult.None)
                {
                    return (EvaluateBoard(board), null);
                }
            }
            var list = MctsUctPlayer.GetAvailableActions(board, playerId);
            Shuffle(list);
            MctsAction? chosenMove = null;
            if (playerId == PlayerIdEnum.SecondPlayer)
            {
                foreach (var move in list)
                {
                    var newBoard = (Board)board.Clone();
                    newBoard.Move(move.CurrentPosition, move.NewPosition);
                    var temp = AlphaBeta(PlayerIdEnum.FirstPlayer, newBoard, deepLevel - 1, alfa, beta, false).Item1;
                    if (beta > temp)
                    {
                        beta = temp;
                        chosenMove = move;
                    }
                    if (alfa >= beta)
                    {
                        break;
                    }
                }
                return (beta, chosenMove);
            }
            else
            {
                foreach (var move in list)
                {
                    var newBoard = (Board)board.Clone();
                    newBoard.Move(move.CurrentPosition, move.NewPosition);

                    var temp = AlphaBeta(PlayerIdEnum.SecondPlayer, newBoard, deepLevel - 1, alfa, beta, false).Item1;
                    if (alfa < temp)
                    {
                        alfa = temp;
                        chosenMove = move;
                    }
                    if (alfa >= beta)
                    {
                        break;
                    }
                }
                return (alfa, chosenMove);
            }
        }


        double EvaluateBoard(Board board)
        {
            var result = board.GetGameResult();
            if (result == GameResult.FirstPlayerWins)
            {
                return 100000;
            }
            if (result == GameResult.SecondPlayerWins)
            {
                return -100000;
            }
            if (result != GameResult.None) //Draw
            {
                return 0;
            }

            double value = 0;
            for (int i = 0; i < Board.BoardLength; i++)
            {
                for (int j = 0; j < Board.BoardWidth; j++)
                {
                    if (board.Pieces[i, j] is null) { continue; }
                    var piece = board.Pieces[i, j];
                    if (piece!.PlayerIdEnum == PlayerIdEnum.FirstPlayer)
                    {
                        value += piece.DefaultStrength * 10 + i;
                        if(piece is SwimmingPiece && ((SwimmingPiece)piece).IsAboutToDrown())
                        {
                            value -= 1000;
                        }
                    }
                    if (piece.PlayerIdEnum == PlayerIdEnum.SecondPlayer)
                    {
                        value -= piece.DefaultStrength * 10 - (Board.BoardLength - i);
                        if (piece is SwimmingPiece && ((SwimmingPiece)piece).IsAboutToDrown())
                        {
                            value += 1000;
                        }
                    }
                }
            }
            return value;

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
