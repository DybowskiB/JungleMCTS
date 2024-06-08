using JungleMCTS.Enums;
using JungleMCTS.GameBoard;
using JungleMCTS.Players.AutoPlayers.MctsPlayers;
using JungleMCTS.Players;

namespace JungleMctsDuels.Tests
{
    public class ReflexiveMctsVsReflexiveMcts
    {
        public static void Run()
        {
            List<int> maxMoveTimeInSeconds = [2, 4, 6, 8, 10];
            int iteration = 0;

            object lockObject = new();

            using StreamWriter writer = new("reflexiveMcts-reflexiveMcts-duelResults.txt");

            Parallel.For(0, maxMoveTimeInSeconds.Count, i =>
            {
                Parallel.For(0, maxMoveTimeInSeconds.Count, j =>
                {
                    int localIteration = Interlocked.Increment(ref iteration);

                    lock (lockObject)
                    {
                        Console.WriteLine($"\n Pending reflexiveMcts vs reflexiveMcts {localIteration} / 25 iteration...\n");
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
                            Console.WriteLine($"\n Processing reflexiveMcts vs reflexiveMcts seed {seed} for iteration {localIteration}... \n");
                        }

                        // Mcts player starts
                        AutoPlayer reflexiveMctsPlayer1 = new ReflexiveMctsPlayer(PlayerIdEnum.FirstPlayer, TimeSpan.FromSeconds(maxMoveTimeInSeconds[i]), seed);
                        AutoPlayer reflexiveMctsPlayer2 = new ReflexiveMctsPlayer(PlayerIdEnum.SecondPlayer, TimeSpan.FromSeconds(maxMoveTimeInSeconds[j]), seed);
                        Board board = new();
                        var (currentPlayer, secondPlayer) = (reflexiveMctsPlayer1, reflexiveMctsPlayer2);
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
                        reflexiveMctsPlayer2 = new ReflexiveMctsPlayer(PlayerIdEnum.FirstPlayer, TimeSpan.FromSeconds(maxMoveTimeInSeconds[j]), seed);
                        reflexiveMctsPlayer1 = new ReflexiveMctsPlayer(PlayerIdEnum.SecondPlayer, TimeSpan.FromSeconds(maxMoveTimeInSeconds[i]), seed);
                        board = new();
                        (currentPlayer, secondPlayer) = (reflexiveMctsPlayer2, reflexiveMctsPlayer1);
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
                        writer.WriteLine($"Reflexive mcts player 1 with {maxMoveTimeInSeconds[i]} seconds - Reflexive mcts player 2 with {maxMoveTimeInSeconds[j]} seconds");
                        writer.WriteLine("Reflexive mcts player 1 wins: " + mctsUctPlayerWins);
                        writer.WriteLine("Reflexive mcts player 1 wins when start: " + mctsUctPlayerWinsWhenStart);
                        writer.WriteLine("Draw: " + draws);
                        writer.WriteLine("Reflexive mcts player 2 wins: " + reflexiveMctsPlayerWins);
                        writer.WriteLine("Reflexive mcts player 2 wins when start: " + reflexiveMctsPlayerWinsWhenStart);
                        writer.WriteLine();
                    }
                });
            });
        }
    }
}
