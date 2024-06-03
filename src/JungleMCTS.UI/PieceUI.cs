using JungleMCTS.Enums;
using JungleMCTS.GameBoard;
using JungleMCTS.GameBoard.GameFields;
using JungleMCTS.GamePiece;
using JungleMCTS.GamePiece.Pieces;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JungleMCTS.UI
{
    internal class PieceUI
    {
        Piece piece;
        public PieceUI(Piece piece)
        {
            this.piece = piece;
        }

        public void DrawPiece(Bitmap bitmap, Position position)
        {
            int fieldHight = bitmap.Height / Board.BoardLength;
            int fieldWidht = bitmap.Width / Board.BoardWidth;
            var positionOnScreen = 
                new Position(
                    (Board.BoardLength - 1 - position.X) * fieldHight,
                    position.Y * fieldWidht);
            Brush brush = Brushes.Black;
            Pen pen = new Pen(Brushes.Black, 5);
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
                name = "Mouse";
            }
            else if(piece is Cat)
            {
                name = "Cat";
            }
            else if(piece is Dog) 
            {
                name = "Dog";
            }
            else if(piece is Wolf)
            {
                name = "Wolf";
            }
            else if (piece is Cheetah)
            {
                name = "Cheetah";
            }
            else if (piece is Tiger)
            {
                name = "Tiger";
            }
            else if (piece is Lion)
            {
                name = "Lion";
            }
            else if (piece is Elephant)
            {
                name = "Elephant";
            }

            Graphics g = Graphics.FromImage(bitmap);
            g.FillEllipse(brush,
                positionOnScreen.Y + (int)(fieldHight * 0.1),
                positionOnScreen.X + (int)(fieldWidht * 0.1),
                (int)(fieldHight * 0.8),
                (int)(fieldWidht * 0.8));
            g.DrawEllipse(pen,
                positionOnScreen.Y + (int)(fieldHight * 0.1),
                positionOnScreen.X + (int)(fieldWidht * 0.1),
                (int)(fieldHight * 0.8),
                (int)(fieldWidht * 0.8));

            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;

            g.DrawString(name,
                new Font("Consolas", 9),
                Brushes.Black,
                new RectangleF(
                    positionOnScreen.Y,
                    positionOnScreen.X,
                    fieldHight,
                    fieldWidht),
                format);
        }
        public void DrawChosenPiece(Bitmap bitmap, Position position)
        {
            Graphics g = Graphics.FromImage(bitmap);
            Brush brush = Brushes.Gold;
            int fieldHight = bitmap.Height / Board.BoardLength;
            int fieldWidht = bitmap.Width / Board.BoardWidth;
            var positionOnScreen =
                new Position(
                    (Board.BoardLength - 1 - position.X) * fieldHight,
                    position.Y * fieldWidht);
            g.FillEllipse(brush,
                positionOnScreen.Y,
                positionOnScreen.X,
                fieldHight,
                fieldWidht);
            
            DrawPiece(bitmap, position);
        }
    }
}
