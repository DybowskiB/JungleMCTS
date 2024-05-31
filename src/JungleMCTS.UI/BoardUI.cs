using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using JungleMCTS;
using JungleMCTS.GameBoard;

namespace JungleMCTS.UI
{
    static class BoardUI
    {
        static public void DrawBoard(PictureBox pictureBox, Board board)
        {
            var bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
            pictureBox.Image = bitmap;
            for (int i = 0; i < Board.BoardLength; i++)
            {
                for (int j = 0; j < Board.BoardWidth; j++)
                {
                    FieldUI.DrawField(bitmap, board.Fields[i, j], new Position(i,j));
                    PieceUI.DrawPiece(bitmap, board.Pieces[i, j], new Position(i,j));
                }
            }
            
        }

   
    }
}
