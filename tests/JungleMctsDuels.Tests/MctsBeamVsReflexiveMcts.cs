using JungleMCTS.Enums;
using JungleMCTS.GameBoard;
using JungleMCTS.Players.AutoPlayers.MctsPlayers;
using JungleMCTS.Players;

namespace JungleMctsDuels.Tests
{
    public class MctsBeamVsReflexiveMcts
    {
        public static void Run()
        {
            List<int> maxMoveTimeInSeconds = [2, 4, 6, 8, 10];
            int iteration = 0;

            object lockObject = new();

            using StreamWriter writer = new("mctsBeam-reflexiveMcts-duelResults.txt");

            Parallel.For(0, maxMoveTimeInSeconds.Count, i =>
            {
                Parallel.For(0, maxMoveTimeInSeconds.Count, j =>
                {
                    int localIteration = Interlocked.Increment(ref iteration);

                    lock (lockObject)
                    {
                        Console.WriteLine($"\n Pending mctsBeam vs reflexiveMcts {localIteration} / 25 iteration...\n");
                    }

                    int mctsBeamPlayerWins = 0;
                    int mctsBeamPlayerWinsWhenStart = 0;
                    int draws = 0;
                    int reflexiveMctsPlayerWins = 0;
                    int reflexiveMctsPlayerWinsWhenStart = 0;

                    for (int seed = 1; seed <= 5; ++seed)
                    {
                        lock (lockObject)
                        {
                            Console.WriteLine($"\n Processing mctsBeam vs reflexiveMcts seed {seed} for iteration {localIteration}... \n");
                        }

                        // Mcts player starts
                        AutoPlayer mctsBeamPlayer = new MctsBeamSearchPlayer(PlayerIdEnum.FirstPlayer, TimeSpan.FromSeconds(maxMoveTimeInSeconds[i]), seed);
                        AutoPlayer reflexiveMctsPlayer = new ReflexiveMctsPlayer(PlayerIdEnum.SecondPlayer, TimeSpan.FromSeconds(maxMoveTimeInSeconds[j]), seed);
                        Board board = new();
                        var (currentPlayer, secondPlayer) = (mctsBeamPlayer, reflexiveMctsPlayer);
                        var gameResult = board.GetGameResult();
                        while (gameResult == GameResult.None)
                        {
                            currentPlayer.Move(board);
                            (currentPlayer, secondPlayer) = (secondPlayer, currentPlayer);
                            gameResult = board.GetGameResult();
                        }
                        if (gameResult == GameResult.FirstPlayerWins)
                        {
                            ++mctsBeamPlayerWins;
                            ++mctsBeamPlayerWinsWhenStart;
                        }
                        else if (gameResult == GameResult.SecondPlayerWins)
                            ++reflexiveMctsPlayerWins;
                        else
                            ++draws;

                        // Mcts beam player starts
                        reflexiveMctsPlayer = new ReflexiveMctsPlayer(PlayerIdEnum.FirstPlayer, TimeSpan.FromSeconds(maxMoveTimeInSeconds[j]), seed);
                        mctsBeamPlayer = new MctsBeamSearchPlayer(PlayerIdEnum.SecondPlayer, TimeSpan.FromSeconds(maxMoveTimeInSeconds[i]), seed);
                        board = new();
                        (currentPlayer, secondPlayer) = (reflexiveMctsPlayer, mctsBeamPlayer);
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
                            ++mctsBeamPlayerWins;
                        else
                            ++draws;
                    }

                    lock (lockObject)
                    {
                        writer.WriteLine("<------------------------------------------------>");
                        writer.WriteLine($"Mcts beam uct with {maxMoveTimeInSeconds[i]} seconds - Reflexive mcts search with {maxMoveTimeInSeconds[j]} seconds");
                        writer.WriteLine("Mcts beam player wins: " + mctsBeamPlayerWins);
                        writer.WriteLine("Mcts beam player wins when start: " + mctsBeamPlayerWinsWhenStart);
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
