using JungleMCTS.GameBoard;
using JungleMCTS.UI;

namespace JungleMCTS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            var Board = new Board();
            BoardUI.DrawBoard(pictureBox1, Board);

        }
    }
}
