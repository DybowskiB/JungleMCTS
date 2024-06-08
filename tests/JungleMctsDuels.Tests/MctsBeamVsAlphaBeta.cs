using JungleMCTS.Enums;
using JungleMCTS.GameBoard;
using JungleMCTS.Players.AutoPlayers.MctsPlayers;
using JungleMCTS.Players.AutoPlayers;
using JungleMCTS.Players;

namespace JungleMctsDuels.Tests
{
    public class MctsBeamVsAlphaBeta
    {
        public static void Run()
        {
            List<int> maxMoveTimeInSeconds = [2, 4, 6, 8, 10];
            int iteration = 0;

            object lockObject = new();

            using StreamWriter writer = new("mctsBeam-alphaBeta-duelResults.txt");

            Parallel.For(0, maxMoveTimeInSeconds.Count, i =>
            {
                Parallel.For(0, maxMoveTimeInSeconds.Count, j =>
                {
                    int localIteration = Interlocked.Increment(ref iteration);

                    lock (lockObject)
                    {
                        Console.WriteLine($"\n Pending mctsBeam vs Alpha-beta {localIteration} / 25 iteration...\n");
                    }

                    int mctsBeamPlayerWins = 0;
                    int mctsBeamPlayerWinsWhenStart = 0;
                    int draws = 0;
                    int alphaBetaPlayerWins = 0;
                    int alphaBetaPlayerWinsWhenStart = 0;

                    for (int seed = 1; seed <= 5; ++seed)
                    {
                        lock (lockObject)
                        {
                            Console.WriteLine($"\n Processing mctsBeam vs Alpha-beta seed {seed} for iteration {localIteration}... \n");
                        }

                        // Mcts beam player starts
                        AutoPlayer mctsBeamPlayer = new MctsBeamSearchPlayer(PlayerIdEnum.FirstPlayer, TimeSpan.FromSeconds(maxMoveTimeInSeconds[i]), seed);
                        AutoPlayer alphaBetaPlayer = new AlphaBetaPlayer(PlayerIdEnum.SecondPlayer, TimeSpan.FromSeconds(maxMoveTimeInSeconds[j]));
                        Board board = new();
                        var (currentPlayer, secondPlayer) = (mctsBeamPlayer, alphaBetaPlayer);
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
                            ++alphaBetaPlayerWins;
                        else
                            ++draws;

                        // Alpha beta player starts
                        alphaBetaPlayer = new AlphaBetaPlayer(PlayerIdEnum.FirstPlayer, TimeSpan.FromSeconds(maxMoveTimeInSeconds[j]));
                        mctsBeamPlayer = new MctsBeamSearchPlayer(PlayerIdEnum.SecondPlayer, TimeSpan.FromSeconds(maxMoveTimeInSeconds[i]), seed);
                        board = new();
                        (currentPlayer, secondPlayer) = (alphaBetaPlayer, mctsBeamPlayer);
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
                            ++mctsBeamPlayerWins;
                        else
                            ++draws;
                    }

                    lock (lockObject)
                    {
                        writer.WriteLine("<------------------------------------------------>");
                        writer.WriteLine($"Mcts beam with {maxMoveTimeInSeconds[i]} seconds - alpha beta with {maxMoveTimeInSeconds[j]} seconds");
                        writer.WriteLine("Mcts beam player wins: " + mctsBeamPlayerWins);
                        writer.WriteLine("Mcts beam player wins when start: " + mctsBeamPlayerWinsWhenStart);
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
