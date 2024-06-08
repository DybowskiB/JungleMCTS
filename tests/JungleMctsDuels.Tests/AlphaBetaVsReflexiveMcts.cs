using JungleMCTS.Enums;
using JungleMCTS.GameBoard;
using JungleMCTS.Players.AutoPlayers.MctsPlayers;
using JungleMCTS.Players;
using JungleMCTS.Players.AutoPlayers;

namespace JungleMctsDuels.Tests
{
    public class AlphaBetaVsReflexiveMcts
    {
        public static void Run()
        {
            List<int> maxMoveTimeInSeconds = [2, 4, 6, 8, 10];
            int iteration = 0;

            object lockObject = new();

            using StreamWriter writer = new("alphaBeta-reflexiveMcts-duelResults.txt");

            Parallel.For(0, maxMoveTimeInSeconds.Count, i =>
            {
                Parallel.For(0, maxMoveTimeInSeconds.Count, j =>
                {
                    int localIteration = Interlocked.Increment(ref iteration);

                    lock (lockObject)
                    {
                        Console.WriteLine($"\n Pending Alpha-Beta vs reflexiveMcts {localIteration} / 25 iteration...\n");
                    }

                    int alphaBetaPlayerWins = 0;
                    int alphaBetaPlayerWinsWhenStart = 0;
                    int draws = 0;
                    int reflexiveMctsPlayerWins = 0;
                    int reflexiveMctsPlayerWinsWhenStart = 0;

                    for (int seed = 1; seed <= 5; ++seed)
                    {
                        lock (lockObject)
                        {
                            Console.WriteLine($"\n Processing Alpha-Beta vs reflexiveMcts seed {seed} for iteration {localIteration}... \n");
                        }

                        // Mcts player starts
                        AutoPlayer alphaBetaPlayer = new AlphaBetaPlayer(PlayerIdEnum.FirstPlayer, TimeSpan.FromSeconds(maxMoveTimeInSeconds[i]));
                        AutoPlayer reflexiveMctsPlayer = new ReflexiveMctsPlayer(PlayerIdEnum.SecondPlayer, TimeSpan.FromSeconds(maxMoveTimeInSeconds[j]), seed);
                        Board board = new();
                        var (currentPlayer, secondPlayer) = (alphaBetaPlayer, reflexiveMctsPlayer);
                        var gameResult = board.GetGameResult();
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
                            ++reflexiveMctsPlayerWins;
                        else
                            ++draws;

                        // Mcts beam player starts
                        reflexiveMctsPlayer = new ReflexiveMctsPlayer(PlayerIdEnum.FirstPlayer, TimeSpan.FromSeconds(maxMoveTimeInSeconds[j]), seed);
                        alphaBetaPlayer = new AlphaBetaPlayer(PlayerIdEnum.SecondPlayer, TimeSpan.FromSeconds(maxMoveTimeInSeconds[i]));
                        board = new();
                        (currentPlayer, secondPlayer) = (reflexiveMctsPlayer, alphaBetaPlayer);
                        gameResult = board.GetGameResult();
                        while (gameResult == GameResult.None)
                        {
                            currentPlayer.Move(board);
                            (currentPlayer, secondPlayer) = (secondPlayer, currentPlayer);
                            gameResult = board.GetGameResult();
                        }
                        if (gameResult == GameResult.FirstPlayerWins)
                        {
                            ++reflexiveMctsPlayerWins;
                            ++reflexiveMctsPlayerWinsWhenStart;
                        }
                        else if (gameResult == GameResult.SecondPlayerWins)
                            ++alphaBetaPlayerWins;
                        else
                            ++draws;
                    }

                    lock (lockObject)
                    {
                        writer.WriteLine("<------------------------------------------------>");
                        writer.WriteLine($"Alpha-beta with {maxMoveTimeInSeconds[i]} seconds - reflexive mcts search with {maxMoveTimeInSeconds[j]} seconds");
                        writer.WriteLine("Alpha-beta player wins: " + alphaBetaPlayerWins);
                        writer.WriteLine("Alpha-beta player wins when start: " + alphaBetaPlayerWinsWhenStart);
                        writer.WriteLine("Draw: " + draws);
                        writer.WriteLine("Reflexive mcts player wins: " + reflexiveMctsPlayerWins);
                        writer.WriteLine("Reflexive player wins when start: " + reflexiveMctsPlayerWinsWhenStart);
                        writer.WriteLine();
                    }
                });
            });
        }
    }
}
