using JungleMCTS.Enums;
using JungleMCTS.GameBoard;
using JungleMCTS.GamePiece;
using JungleMCTS.Players;
using JungleMCTS.Players.AutoPlayers;
using JungleMCTS.Players.AutoPlayers.MctsPlayers;
using JungleMCTS.UI;
using System.Numerics;

namespace JungleMCTS
{
    public partial class MainWindow : Form
    {
        private readonly BoardUI boardUI = new(new Board());
        private Player player1;
        private Player player2;
        private Piece? chosenPiece;
        private bool isWaitingForHumanMove = false;
        private PlayerIdEnum whichPlayerToMove;

        public MainWindow()
        {
            InitializeComponent();
            FieldUI.FieldHeight = pictureBox1.Height / Board.BoardLength;
            FieldUI.FieldWidth = pictureBox1.Width / Board.BoardWidth;
            boardUI.DrawBoard(pictureBox1);
            var choosePlayerWindow = new ChoosePlayerWindow(Start);
            choosePlayerWindow.Show();
        }


        public void Start(string firstPlayer, string secondPlayer)
        {
            int secondsForMove = 3;
            if (firstPlayer  == "MCTS UCT")
            {
                player1 = new MctsUctPlayer(PlayerIdEnum.FirstPlayer, TimeSpan.FromSeconds(secondsForMove));
            }
            else if (firstPlayer == "MCTS Beam")
            {
                player1 = new MctsBeamSearchPlayer(PlayerIdEnum.FirstPlayer, TimeSpan.FromSeconds(secondsForMove));
            }
            else if (firstPlayer == "MCTS Reflexive")
            {
                player1 = new ReflexiveMctsPlayer(PlayerIdEnum.FirstPlayer, TimeSpan.FromSeconds(secondsForMove));
            }
            else if(firstPlayer == "Human")
            {
                player1 = new HumanPlayer(PlayerIdEnum.FirstPlayer);
            }
            else if(firstPlayer == "AlphaBeta")
            {
                player1 = new AlphaBetaPlayer(PlayerIdEnum.FirstPlayer, new TimeSpan());
            }

            if (secondPlayer == "MCTS UCT")
            {
                player2 = new MctsUctPlayer(PlayerIdEnum.SecondPlayer, TimeSpan.FromSeconds(secondsForMove));
            }
            else if (secondPlayer == "MCTS Beam")
            {
                player2 = new MctsBeamSearchPlayer(PlayerIdEnum.SecondPlayer, TimeSpan.FromSeconds(secondsForMove));
            }
            else if (secondPlayer == "MCTS Reflexive")
            {
                player2 = new ReflexiveMctsPlayer(PlayerIdEnum.SecondPlayer, TimeSpan.FromSeconds(secondsForMove));
            }
            else if (secondPlayer == "Human")
            {
                player2 = new HumanPlayer(PlayerIdEnum.SecondPlayer);
            }
            else if (secondPlayer == "AlphaBeta")
            {
                player2 = new AlphaBetaPlayer(PlayerIdEnum.SecondPlayer, new TimeSpan());
            }
            FirstPlayerMove();
        }

        void FirstPlayerMove()
        {
            if (player1 is not HumanPlayer)
            {
                ((AutoPlayer)player1).Move(boardUI.board);
                boardUI.DrawBoard(pictureBox1);
                Refresh();
                if (CheckGameEnds())
                {
                    isWaitingForHumanMove = false;
                    return;
                }
                SecondPlayerMove();
            }
            else
            {
                HumanPlayerMove(PlayerIdEnum.FirstPlayer);
            }
        }

        void SecondPlayerMove()
        {
            if (player2 is not HumanPlayer)
            {
                ((AutoPlayer)player2).Move(boardUI.board);
                boardUI.DrawBoard(pictureBox1);
                Refresh();
                if (CheckGameEnds())
                {
                    isWaitingForHumanMove = false;
                    return;
                }
                FirstPlayerMove();
            }
            else
            {
                HumanPlayerMove(PlayerIdEnum.SecondPlayer);
            }
        }

        void HumanPlayerMove(PlayerIdEnum playerId)
        {
            whichPlayerToMove = playerId;
            isWaitingForHumanMove = true;
            pictureBox1.Cursor = Cursors.Hand;
        }


        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (!isWaitingForHumanMove) { return; }
            if (chosenPiece == null)
            {
                chosenPiece = boardUI.ChoosePiece(new Vector2(e.X, e.Y));
                if (chosenPiece != null && chosenPiece.PlayerIdEnum == whichPlayerToMove)
                {
                    boardUI.DrawChoosenPiece(chosenPiece, pictureBox1);
                    boardUI.DrawPossibleMoves(chosenPiece, pictureBox1);
                }
                else
                {
                    chosenPiece = null;
                }
            }
            else
            {
                if(boardUI.MakeMove(chosenPiece, new Vector2(e.X, e.Y), whichPlayerToMove))
                {
                    chosenPiece = null;
                    isWaitingForHumanMove= false;
                    pictureBox1.Cursor = Cursors.WaitCursor;
                    boardUI.DrawBoard(pictureBox1);
                    Refresh();
                    if(CheckGameEnds())
                    {
                        pictureBox1.Cursor= Cursors.Default;
                        return;
                    }
                    if (whichPlayerToMove == PlayerIdEnum.FirstPlayer)
                    {
                        SecondPlayerMove();
                    }
                    else
                    {
                        FirstPlayerMove();
                    }
                }
                else
                {
                    chosenPiece = null;
                    boardUI.DrawBoard(pictureBox1);
                    pictureBox1_MouseClick(sender, e);
                }
            }
        }

        bool CheckGameEnds()
        {
            var gameResult = boardUI.board.GetGameResult();
            if(gameResult == GameResult.None)
            {
                return false;
            }
            else if(gameResult == GameResult.FirstPlayerWins)
            {
                MessageBox.Show("The first player won");
                return true;
            }
            else if(gameResult == GameResult.SecondPlayerWins)
            {
                MessageBox.Show("The second player won");
                return true;
            }
            else
            {
                MessageBox.Show("The game ended in a draw");
                return true;
            }
        }
    }
}
