using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JungleMCTS.GameBoard;
using JungleMCTS.GameBoard.GameFields;

namespace JungleMCTS.UI
{
    internal static class FieldUI
    {
        static public void DrawField(Bitmap bitmap, GameField? field, Position position)
        {
            Brush brush = Brushes.Black;
            Pen pen = new Pen(Brushes.Black);
            int fieldHight = bitmap.Height / Board.BoardLength;
            int fieldWidht = bitmap.Width / Board.BoardWidth;
            var positionOnScreen =
                new Position(
                    (Board.BoardLength - 1 - position.X) * fieldHight,
                    position.Y * fieldWidht);
            if (field == null){
                return;
            }
            if (field is Cave){
                brush = Brushes.Black;
            }
            else if (field is DefaultField){
                brush = Brushes.Wheat;
            }
            else if (field is Lake){
                brush = Brushes.Blue;
            }
            else if (field is Trap) {
                brush = Brushes.Brown;
            }
            Graphics g = Graphics.FromImage(bitmap);
            g.FillRectangle(brush,
                positionOnScreen.Y,
                positionOnScreen.X,
                fieldHight,
                fieldWidht);
            g.DrawRectangle(pen,
                positionOnScreen.Y,
                positionOnScreen.X,
                fieldHight,
                fieldWidht);
        }
    }
}
