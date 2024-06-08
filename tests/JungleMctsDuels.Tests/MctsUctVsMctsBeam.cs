using JungleMCTS.Enums;
using JungleMCTS.GameBoard;
using JungleMCTS.Players;
using JungleMCTS.Players.AutoPlayers.MctsPlayers;

namespace JungleMctsDuels.Tests
{
    public static class MctsUctVsMctsBeam
    {
        public static void Run()
        {
            List<int> maxMoveTimeInSeconds = [ 2, 4, 6, 8, 10 ];
            int iteration = 0;

            object lockObject = new();

            using StreamWriter writer = new("mctsUct-mctsBeam-duelResults.txt");

            Parallel.For(0, maxMoveTimeInSeconds.Count, i =>
            {
                Parallel.For(0, maxMoveTimeInSeconds.Count, j =>
                {
                    int localIteration = Interlocked.Increment(ref iteration);

                    lock (lockObject)
                    {
                        Console.WriteLine($"\n Pending mctsUct vs mctsBeam {localIteration} / 25 iteration...\n");
                    }

                    int mctsUctPlayerWins = 0;
                    int mctsUctPlayerWinsWhenStart = 0;
                    int draws = 0;
                    int mctsBeamPlayerWins = 0;
                    int mctsBeamPlayerWinsWhenStart = 0;

                    for (int seed = 1; seed <= 5; ++seed)
                    {
                        lock (lockObject)
                        {
                            Console.WriteLine($"\n Processing mctsUct vs mctsBeam seed {seed} for iteration {localIteration}... \n");
                        }

                        // Mcts player starts
                        AutoPlayer mctsUctPlayer = new MctsUctPlayer(PlayerIdEnum.FirstPlayer, TimeSpan.FromSeconds(maxMoveTimeInSeconds[i]), seed);
                        AutoPlayer mctsUctBeamPlayer = new MctsBeamSearchPlayer(PlayerIdEnum.SecondPlayer, TimeSpan.FromSeconds(maxMoveTimeInSeconds[j]), seed);
                        Board board = new();
                        var (currentPlayer, secondPlayer) = (mctsUctPlayer, mctsUctBeamPlayer);
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
                            ++mctsBeamPlayerWins;
                        else
                            ++draws;

                        // Mcts beam player starts
                        mctsUctBeamPlayer = new MctsBeamSearchPlayer(PlayerIdEnum.FirstPlayer, TimeSpan.FromSeconds(maxMoveTimeInSeconds[j]), seed);
                        mctsUctPlayer = new MctsUctPlayer(PlayerIdEnum.SecondPlayer, TimeSpan.FromSeconds(maxMoveTimeInSeconds[i]), seed);
                        board = new();
                        (currentPlayer, secondPlayer) = (mctsUctBeamPlayer, mctsUctPlayer);
                        gameResult = board.GetGameResult();
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
                            ++mctsUctPlayerWins;
                        else
                            ++draws;
                    }

                    lock (lockObject)
                    {
                        writer.WriteLine("<------------------------------------------------>");
                        writer.WriteLine($"Mcts uct with {maxMoveTimeInSeconds[i]} seconds - Mcts beam search with {maxMoveTimeInSeconds[j]} seconds");
                        writer.WriteLine("Mcts uct player wins: " + mctsUctPlayerWins);
                        writer.WriteLine("Mcts uct player wins when start: " + mctsUctPlayerWinsWhenStart);
                        writer.WriteLine("Draw: " + draws);
                        writer.WriteLine("Mcts beam player wins: " + mctsBeamPlayerWins);
                        writer.WriteLine("Mcts beam player wins when start: " + mctsBeamPlayerWinsWhenStart);
                        writer.WriteLine();
                    }
                });
            });
        }
    }
}
