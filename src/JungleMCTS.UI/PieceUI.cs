using JungleMCTS.GameBoard;
using JungleMCTS.GameBoard.GameFields;
using JungleMCTS.GamePiece;
using JungleMCTS.GamePiece.Pieces;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JungleMCTS.UI
{
    internal class PieceUI
    {
        static public void DrawPiece(Bitmap bitmap, Piece? piece, Position position)
        {
            Brush brush = Brushes.Black;
            Pen pen = new Pen(Brushes.Black, 5);
            int fieldHight = bitmap.Height / Board.BoardLength;
            int fieldWidht = bitmap.Width / Board.BoardWidth;
            if (piece == null)
            {
                return;
            }
            if(piece.PlayerIdEnum == Enums.PlayerIdEnum.FirstPlayer)
            {
                brush = Brushes.White;
            }
            else
            {
                brush = Brushes.Gray;
            }
            string name = "";
            if(piece is Mouse)
            {
                name = "M";
            }
            else if(piece is Cat)
            {
                name = "C";
            }
            else if(piece is Dog) 
            {
                name = "D";
            }
            else if(piece is Wolf)
            {
                name = "W";
            }
            else if (piece is Panther)
            {
                name = "P";
            }
            else if (piece is Tiger)
            {
                name = "T";
            }
            else if (piece is Lion)
            {
                name = "L";
            }
            else if (piece is Elephant)
            {
                name = "E";
            }




            Graphics g = Graphics.FromImage(bitmap);
            g.FillEllipse(brush,
                position.Y * fieldHight + (int)(fieldHight * 0.1),
                position.X * fieldWidht + (int)(fieldWidht * 0.1),
                (int)(fieldHight * 0.8),
                (int)(fieldWidht * 0.8));
            g.DrawEllipse(pen,
                position.Y * fieldHight + (int)(fieldHight * 0.1),
                position.X * fieldWidht + (int)(fieldWidht * 0.1),
                (int)(fieldHight * 0.8),
                (int)(fieldWidht * 0.8));

            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;

            g.DrawString(name,
                new Font("Consolas", 16),
                Brushes.Black,
                new RectangleF(
                    position.Y * fieldHight,
                    position.X * fieldWidht,
                    fieldHight,
                    fieldWidht),
                format);
        }
    }
}
