using JungleMCTS.Enums;
using JungleMCTS.GameBoard;
using JungleMCTS.Players.AutoPlayers.MctsPlayers;
using JungleMCTS.Players;

namespace JungleMctsDuels.Tests
{
    public class MctsUctVsReflexiveMcts
    {
        public static void Run()
        {
            List<int> maxMoveTimeInSeconds = [2, 4, 6, 8, 10];
            int iteration = 0;

            object lockObject = new();

            using StreamWriter writer = new("mctsUct-reflexiveMcts-duelResults.txt");

            Parallel.For(0, maxMoveTimeInSeconds.Count, i =>
            {
                Parallel.For(0, maxMoveTimeInSeconds.Count, j =>
                {
                    int localIteration = Interlocked.Increment(ref iteration);

                    lock (lockObject)
                    {
                        Console.WriteLine($"\n Pending mctsUct vs reflexiveMcts {localIteration} / 25 iteration...\n");
                    }

                    int mctsUctPlayerWins = 0;
                    int mctsUctPlayerWinsWhenStart = 0;
                    int draws = 0;
                    int reflexiveMctsPlayerWins = 0;
                    int reflexiveMctsPlayerWinsWhenStart = 0;

                    for (int seed = 1; seed <= 5; ++seed)
                    {
                        lock (lockObject)
                        {
                            Console.WriteLine($"\n Processing mctsUct vs reflexiveMcts seed {seed} for iteration {localIteration}... \n");
                        }

                        // Mcts player starts
                        AutoPlayer mctsUctPlayer = new MctsUctPlayer(PlayerIdEnum.FirstPlayer, TimeSpan.FromSeconds(maxMoveTimeInSeconds[i]), seed);
                        AutoPlayer reflexiveMctsPlayer = new ReflexiveMctsPlayer(PlayerIdEnum.SecondPlayer, TimeSpan.FromSeconds(maxMoveTimeInSeconds[j]), seed);
                        Board board = new();
                        var (currentPlayer, secondPlayer) = (mctsUctPlayer, reflexiveMctsPlayer);
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
                            ++reflexiveMctsPlayerWins;
                        else
                            ++draws;

                        // Mcts beam player starts
                        reflexiveMctsPlayer = new ReflexiveMctsPlayer(PlayerIdEnum.FirstPlayer, TimeSpan.FromSeconds(maxMoveTimeInSeconds[j]), seed);
                        mctsUctPlayer = new MctsUctPlayer(PlayerIdEnum.SecondPlayer, TimeSpan.FromSeconds(maxMoveTimeInSeconds[i]), seed);
                        board = new();
                        (currentPlayer, secondPlayer) = (reflexiveMctsPlayer, mctsUctPlayer);
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
                            ++mctsUctPlayerWins;
                        else
                            ++draws;
                    }

                    lock (lockObject)
                    {
                        writer.WriteLine("<------------------------------------------------>");
                        writer.WriteLine($"Mcts uct with {maxMoveTimeInSeconds[i]} seconds - reflexive mcts search with {maxMoveTimeInSeconds[j]} seconds");
                        writer.WriteLine("Mcts uct player wins: " + mctsUctPlayerWins);
                        writer.WriteLine("Mcts uct player wins when start: " + mctsUctPlayerWinsWhenStart);
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
