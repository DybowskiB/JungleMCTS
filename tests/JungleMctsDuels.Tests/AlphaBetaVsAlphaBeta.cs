using JungleMCTS.Enums;
using JungleMCTS.GameBoard;
using JungleMCTS.Players.AutoPlayers.MctsPlayers;
using JungleMCTS.Players;
using JungleMCTS.Players.AutoPlayers;

namespace JungleMctsDuels.Tests
{
    public class AlphaBetaVsAlphaBeta
    {
        public static void Run()
        {
            List<int> maxMoveTimeInSeconds = [2, 4, 6, 8, 10];
            int iteration = 0;

            object lockObject = new();

            using StreamWriter writer = new("alphaBeta-alphaBeta-duelResults.txt");

            Parallel.For(0, maxMoveTimeInSeconds.Count, i =>
            {
                Parallel.For(0, maxMoveTimeInSeconds.Count, j =>
                {
                    int localIteration = Interlocked.Increment(ref iteration);

                    lock (lockObject)
                    {
                        Console.WriteLine($"\n Pending alphaBeta vs alphaBeta {localIteration} / 25 iteration...\n");
                    }

                    int alphaBeta1PlayerWins = 0;
                    int alphaBeta1PlayerWinsWhenStart = 0;
                    int draws = 0;
                    int alphaBeta2PlayerWins = 0;
                    int alphaBeta2PlayerWinsWhenStart = 0;

                    for (int seed = 1; seed <= 5; ++seed)
                    {
                        lock (lockObject)
                        {
                            Console.WriteLine($"\n Processing duel alphaBeta vs alphaBeta number {seed} for iteration {localIteration}... \n");
                        }

                        // Alpha-beta player 1 starts
                        AutoPlayer alphaBeta1 = new AlphaBetaPlayer(PlayerIdEnum.FirstPlayer, TimeSpan.FromSeconds(maxMoveTimeInSeconds[i]));
                        AutoPlayer alphaBeta2 = new AlphaBetaPlayer(PlayerIdEnum.SecondPlayer, TimeSpan.FromSeconds(maxMoveTimeInSeconds[j]));
                        Board board = new();
                        var (currentPlayer, secondPlayer) = (alphaBeta1, alphaBeta2);
                        var gameResult = board.GetGameResult();
                        while (gameResult == GameResult.None)
                        {
                            currentPlayer.Move(board);
                            (currentPlayer, secondPlayer) = (secondPlayer, currentPlayer);
                            gameResult = board.GetGameResult();
                        }
                        if (gameResult == GameResult.FirstPlayerWins)
                        {
                            ++alphaBeta1PlayerWins;
                            ++alphaBeta1PlayerWinsWhenStart;
                        }
                        else if (gameResult == GameResult.SecondPlayerWins)
                            ++alphaBeta2PlayerWins;
                        else
                            ++draws;

                        // Alpha-beta player 2 starts
                        alphaBeta2 = new AlphaBetaPlayer(PlayerIdEnum.FirstPlayer, TimeSpan.FromSeconds(maxMoveTimeInSeconds[j]));
                        alphaBeta1 = new AlphaBetaPlayer(PlayerIdEnum.SecondPlayer, TimeSpan.FromSeconds(maxMoveTimeInSeconds[i]));
                        board = new();
                        (currentPlayer, secondPlayer) = (alphaBeta2, alphaBeta1);
                        gameResult = board.GetGameResult();
                        while (gameResult == GameResult.None)
                        {
                            currentPlayer.Move(board);
                            (currentPlayer, secondPlayer) = (secondPlayer, currentPlayer);
                            gameResult = board.GetGameResult();
                        }
                        if (gameResult == GameResult.FirstPlayerWins)
                        {
                            ++alphaBeta2PlayerWins;
                            ++alphaBeta2PlayerWinsWhenStart;
                        }
                        else if (gameResult == GameResult.SecondPlayerWins)
                            ++alphaBeta1PlayerWins;
                        else
                            ++draws;
                    }

                    lock (lockObject)
                    {
                        writer.WriteLine("<------------------------------------------------>");
                        writer.WriteLine($"Alpha beta with {maxMoveTimeInSeconds[i]} seconds - alpha beta with {maxMoveTimeInSeconds[j]} seconds");
                        writer.WriteLine("Alpha beta 1 player wins: " + alphaBeta1PlayerWins);
                        writer.WriteLine("Alpha beta 1 player wins when start: " + alphaBeta1PlayerWinsWhenStart);
                        writer.WriteLine("Draw: " + draws);
                        writer.WriteLine("Alpha beta 2 player wins: " + alphaBeta2PlayerWins);
                        writer.WriteLine("Alpha beta 2 player wins when start: " + alphaBeta2PlayerWinsWhenStart);
                        writer.WriteLine();
                    }
                });
            });
        }
    }
}
