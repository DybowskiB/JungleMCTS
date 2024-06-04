using JungleMCTS.Enums;
using JungleMCTS.GameBoard;
using JungleMCTS.GamePiece;
using JungleMCTS.Players;
using JungleMCTS.Players.AutoPlayers.MctsPlayers;
using JungleMCTS.UI;
using System.Drawing;
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
            if (firstPlayer  == "MCTS UCT")
            {
                player1 = new MctsUctPlayer(PlayerIdEnum.FirstPlayer, new TimeSpan(1000));
            }
            else if(firstPlayer == "Human")
            {
                player1 = new HumanPlayer(PlayerIdEnum.FirstPlayer);
            }

            if (secondPlayer == "MCTS UCT")
            {
                player2 = new MctsUctPlayer(PlayerIdEnum.SecondPlayer, new TimeSpan(1000));
            }
            else
            {
                player2 = new HumanPlayer(PlayerIdEnum.SecondPlayer);
            }
            FirstPlayerMove();
        }

        void FirstPlayerMove()
        {
            if (player1 is not HumanPlayer)
            {
                ((AutoPlayer)player1).Move(boardUI.board);
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
                    boardUI.DrawBoard(pictureBox1);
                    CheckGameEnds();
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

        void CheckGameEnds()
        {
            var gameResult = boardUI.board.GetGameResult();
            if(gameResult == GameResult.None)
            {
                return;
            }
            if(gameResult == GameResult.FirstPlayerWins)
            {
                MessageBox.Show("The first player won");
            }
            else if(gameResult == GameResult.SecondPlayerWins)
            {
                MessageBox.Show("The second player won");
            }
            else
            {
                MessageBox.Show("The game ended in a draw");
            }
        }
    }
}
