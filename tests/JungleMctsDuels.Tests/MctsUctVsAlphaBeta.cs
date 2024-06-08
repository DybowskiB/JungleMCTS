using JungleMCTS.Enums;
using JungleMCTS.GameBoard;
using JungleMCTS.Players;
using JungleMCTS.Players.AutoPlayers;
using JungleMCTS.Players.AutoPlayers.MctsPlayers;

namespace JungleMctsDuels.Tests
{
    public class MctsUctVsAlphaBeta
    {
        public static void Run()
        {
            List<int> maxMoveTimeInSeconds = [2, 4, 6, 8, 10];
            int iteration = 0;

            object lockObject = new();

            using StreamWriter writer = new("mctsUct-alphaBeta-duelResults.txt");

            Parallel.For(0, maxMoveTimeInSeconds.Count, i =>
            {
                Parallel.For(0, maxMoveTimeInSeconds.Count, j =>
                {
                    int localIteration = Interlocked.Increment(ref iteration);

                    lock (lockObject)
                    {
                        Console.WriteLine($"\n Pending mctsUct vs Alpha-beta {localIteration} / 25 iteration...\n");
                    }

                    int mctsUctPlayerWins = 0;
                    int mctsUctPlayerWinsWhenStart = 0;
                    int draws = 0;
                    int alphaBetaPlayerWins = 0;
                    int alphaBetaPlayerWinsWhenStart = 0;

                    for (int seed = 1; seed <= 5; ++seed)
                    {
                        lock (lockObject)
                        {
                            Console.WriteLine($"\n Processing mctsUct vs Alpha-beta seed {seed} for iteration {localIteration}... \n");
                        }

                        // Mcts player starts
                        AutoPlayer mctsUctPlayer = new MctsUctPlayer(PlayerIdEnum.FirstPlayer, TimeSpan.FromSeconds(maxMoveTimeInSeconds[i]), seed);
                        AutoPlayer alphaBetaPlayer = new AlphaBetaPlayer(PlayerIdEnum.SecondPlayer, TimeSpan.FromSeconds(maxMoveTimeInSeconds[j]));
                        Board board = new();
                        var (currentPlayer, secondPlayer) = (mctsUctPlayer, alphaBetaPlayer);
                        var gameResult = board.GetGameResult();
                        while (gameResult == GameResult.None)
                        {
                            currentPlayer.Move(board);
                            (currentPlayer, secondPlayer) = (secondPlayer, currentPlayer);
                            gameResult = board.GetGameResult();
                        }
                        if (gameResult == GameResult.FirstPlayerWins)
                        {
                            ++mctsUctPlayerWins;
                            ++mctsUctPlayerWinsWhenStart;
                        }
                        else if (gameResult == GameResult.SecondPlayerWins)
                            ++alphaBetaPlayerWins;
                        else
                            ++draws;

                        // Alpha beta player starts
                        alphaBetaPlayer = new AlphaBetaPlayer(PlayerIdEnum.FirstPlayer, TimeSpan.FromSeconds(maxMoveTimeInSeconds[j]));
                        mctsUctPlayer = new MctsUctPlayer(PlayerIdEnum.SecondPlayer, TimeSpan.FromSeconds(maxMoveTimeInSeconds[i]), seed);
                        board = new();
                        (currentPlayer, secondPlayer) = (alphaBetaPlayer, mctsUctPlayer);
                        gameResult = board.GetGameResult();
                        while (gameResult == GameResult.None)
                        {
                            currentPlayer.Move(board);
                            (currentPlayer, secondPlayer) = (secondPlayer, currentPlayer);
                            gameResult = board.GetGameResult();
                        }
                        if (gameResult == GameResult.FirstPlayerWins)
                        {
                            ++alphaBetaPlayerWins;
                            ++alphaBetaPlayerWinsWhenStart;
                        }
                        else if (gameResult == GameResult.SecondPlayerWins)
                            ++mctsUctPlayerWins;
                        else
                            ++draws;
                    }

                    lock (lockObject)
                    {
                        writer.WriteLine("<------------------------------------------------>");
                        writer.WriteLine($"Mcts uct with {maxMoveTimeInSeconds[i]} seconds - alpha beta with {maxMoveTimeInSeconds[j]} seconds");
                        writer.WriteLine("Mcts uct player wins: " + mctsUctPlayerWins);
                        writer.WriteLine("Mcts uct player wins when start: " + mctsUctPlayerWinsWhenStart);
                        writer.WriteLine("Draw: " + draws);
                        writer.WriteLine("Alpha-beta player wins: " + alphaBetaPlayerWins);
                        writer.WriteLine("Alpha-beta player wins when start: " + alphaBetaPlayerWinsWhenStart);
                        writer.WriteLine();
                    }
                });
            });
        }
    }
}
