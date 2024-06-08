using JungleMCTS.Enums;
using JungleMCTS.GameBoard;
using JungleMCTS.Players.AutoPlayers.MctsPlayers;
using JungleMCTS.Players;

namespace JungleMctsDuels.Tests
{
    public class MctsBeamVsMctsBeam
    {
        public static void Run()
        {
            List<int> maxMoveTimeInSeconds = [2, 4, 6, 8, 10];
            int iteration = 0;

            object lockObject = new();

            using StreamWriter writer = new("mctsBeam-mctsBeam-duelResults.txt");

            Parallel.For(0, maxMoveTimeInSeconds.Count, i =>
            {
                Parallel.For(0, maxMoveTimeInSeconds.Count, j =>
                {
                    int localIteration = Interlocked.Increment(ref iteration);

                    lock (lockObject)
                    {
                        Console.WriteLine($"\n Pending mctsBeam vs mctsBeam {localIteration} / 25 iteration...\n");
                    }

                    int mctsBeamPlayer1Wins = 0;
                    int mctsBeamPlayer1WinsWhenStart = 0;
                    int draws = 0;
                    int mctsBeamPlayer2Wins = 0;
                    int mctsBeamPlayer2WinsWhenStart = 0;

                    for (int seed = 1; seed <= 5; ++seed)
                    {
                        lock (lockObject)
                        {
                            Console.WriteLine($"\n Processing mctsBeam vs mctsBeam seed {seed} for iteration {localIteration}... \n");
                        }

                        // Mcts beam player 1 starts
                        AutoPlayer mctsBeamPlayer1 = new MctsBeamSearchPlayer(PlayerIdEnum.FirstPlayer, TimeSpan.FromSeconds(maxMoveTimeInSeconds[i]), seed);
                        AutoPlayer mctsBeamPlayer2 = new MctsBeamSearchPlayer(PlayerIdEnum.SecondPlayer, TimeSpan.FromSeconds(maxMoveTimeInSeconds[j]), seed);
                        Board board = new();
                        var (currentPlayer, secondPlayer) = (mctsBeamPlayer1, mctsBeamPlayer2);
                        var gameResult = board.GetGameResult();
                        while (gameResult == GameResult.None)
                        {
                            currentPlayer.Move(board);
                            (currentPlayer, secondPlayer) = (secondPlayer, currentPlayer);
                            gameResult = board.GetGameResult();
                        }
                        if (gameResult == GameResult.FirstPlayerWins)
                        {
                            ++mctsBeamPlayer1Wins;
                            ++mctsBeamPlayer1WinsWhenStart;
                        }
                        else if (gameResult == GameResult.SecondPlayerWins)
                            ++mctsBeamPlayer2Wins;
                        else
                            ++draws;

                        // Mcts beam player 2 starts
                        mctsBeamPlayer2 = new MctsBeamSearchPlayer(PlayerIdEnum.FirstPlayer, TimeSpan.FromSeconds(maxMoveTimeInSeconds[j]), seed);
                        mctsBeamPlayer1 = new MctsBeamSearchPlayer(PlayerIdEnum.SecondPlayer, TimeSpan.FromSeconds(maxMoveTimeInSeconds[i]), seed);
                        board = new();
                        (currentPlayer, secondPlayer) = (mctsBeamPlayer2, mctsBeamPlayer1);
                        gameResult = board.GetGameResult();
                        while (gameResult == GameResult.None)
                        {
                            currentPlayer.Move(board);
                            (currentPlayer, secondPlayer) = (secondPlayer, currentPlayer);
                            gameResult = board.GetGameResult();
                        }
                        if (gameResult == GameResult.FirstPlayerWins)
                        {
                            ++mctsBeamPlayer2Wins;
                            ++mctsBeamPlayer2WinsWhenStart;
                        }
                        else if (gameResult == GameResult.SecondPlayerWins)
                            ++mctsBeamPlayer1Wins;
                        else
                            ++draws;
                    }

                    lock (lockObject)
                    {
                        writer.WriteLine("<------------------------------------------------>");
                        writer.WriteLine($"Mcts uct with {maxMoveTimeInSeconds[i]} seconds - Mcts beam search with {maxMoveTimeInSeconds[j]} seconds");
                        writer.WriteLine("Mcts beam player 1 wins: " + mctsBeamPlayer1Wins);
                        writer.WriteLine("Mcts beam player 1 wins when start: " + mctsBeamPlayer1WinsWhenStart);
                        writer.WriteLine("Draw: " + draws);
                        writer.WriteLine("Mcts beam player 2 wins: " + mctsBeamPlayer2Wins);
                        writer.WriteLine("Mcts beam player 2 wins when start: " + mctsBeamPlayer2WinsWhenStart);
                        writer.WriteLine();
                    }
                });
            });
        }
    }
}
