using JungleMCTS.Enums;
using JungleMCTS.GameBoard;
using JungleMCTS.Players.AutoPlayers.MctsPlayers;
using JungleMCTS.Players;

namespace JungleMctsDuels.Tests
{
    public class MctsUctVsMctsUct
    {
        public static void Run()
        {
            List<int> maxMoveTimeInSeconds = [2, 4, 6, 8, 10];
            int iteration = 0;

            object lockObject = new();

            using StreamWriter writer = new("mctsUct-mctsUct-duelResults.txt");

            Parallel.For(0, maxMoveTimeInSeconds.Count, i =>
            {
                Parallel.For(0, maxMoveTimeInSeconds.Count, j =>
                {
                    int localIteration = Interlocked.Increment(ref iteration);

                    lock (lockObject)
                    {
                        Console.WriteLine($"\n Pending mctsUct vs mctsUct {localIteration} / 25 iteration...\n");
                    }

                    int mctsUctPlayer1Wins = 0;
                    int mctsUctPlayer1WinsWhenStart = 0;
                    int draws = 0;
                    int mctsUctPlayer2Wins = 0;
                    int mctsUctPlayer2WinsWhenStart = 0;

                    for (int seed = 1; seed <= 5; ++seed)
                    {
                        lock (lockObject)
                        {
                            Console.WriteLine($"\n Processing mctsUct vs mctsUct seed {seed} for iteration {localIteration}... \n");
                        }

                        // Mcts player 1 starts
                        AutoPlayer mctsUctPlayer1 = new MctsUctPlayer(PlayerIdEnum.FirstPlayer, TimeSpan.FromSeconds(maxMoveTimeInSeconds[i]), seed);
                        AutoPlayer mctsUctPlayer2 = new MctsUctPlayer(PlayerIdEnum.SecondPlayer, TimeSpan.FromSeconds(maxMoveTimeInSeconds[j]), seed);
                        Board board = new();
                        var (currentPlayer, secondPlayer) = (mctsUctPlayer1, mctsUctPlayer2);
                        var gameResult = board.GetGameResult();
                        while (gameResult == GameResult.None)
                        {
                            currentPlayer.Move(board);
                            (currentPlayer, secondPlayer) = (secondPlayer, currentPlayer);
                            gameResult = board.GetGameResult();
                        }
                        if (gameResult == GameResult.FirstPlayerWins)
                        {
                            ++mctsUctPlayer1Wins;
                            ++mctsUctPlayer1WinsWhenStart;
                        }
                        else if (gameResult == GameResult.SecondPlayerWins)
                            ++mctsUctPlayer2Wins;
                        else
                            ++draws;

                        // Mcts player 2 starts
                        mctsUctPlayer2 = new MctsUctPlayer(PlayerIdEnum.FirstPlayer, TimeSpan.FromSeconds(maxMoveTimeInSeconds[j]), seed);
                        mctsUctPlayer1 = new MctsUctPlayer(PlayerIdEnum.SecondPlayer, TimeSpan.FromSeconds(maxMoveTimeInSeconds[i]), seed);
                        board = new();
                        (currentPlayer, secondPlayer) = (mctsUctPlayer2, mctsUctPlayer1);
                        gameResult = board.GetGameResult();
                        while (gameResult == GameResult.None)
                        {
                            currentPlayer.Move(board);
                            (currentPlayer, secondPlayer) = (secondPlayer, currentPlayer);
                            gameResult = board.GetGameResult();
                        }
                        if (gameResult == GameResult.FirstPlayerWins)
                        {
                            ++mctsUctPlayer2Wins;
                            ++mctsUctPlayer2WinsWhenStart;
                        }
                        else if (gameResult == GameResult.SecondPlayerWins)
                            ++mctsUctPlayer1Wins;
                        else
                            ++draws;
                    }

                    lock (lockObject)
                    {
                        writer.WriteLine("<------------------------------------------------>");
                        writer.WriteLine($"Mcts uct with {maxMoveTimeInSeconds[i]} seconds - Mcts uct with {maxMoveTimeInSeconds[j]} seconds");
                        writer.WriteLine("Mcts uct player 1 wins: " + mctsUctPlayer1Wins);
                        writer.WriteLine("Mcts uct player 1 wins when start: " + mctsUctPlayer1WinsWhenStart);
                        writer.WriteLine("Draw: " + draws);
                        writer.WriteLine("Mcts uct player 2 wins: " + mctsUctPlayer2Wins);
                        writer.WriteLine("Mcts uct player 2 wins when start: " + mctsUctPlayer2WinsWhenStart);
                        writer.WriteLine();
                    }
                });
            });
        }
    }
}
