using JungleMCTS.GameBoard;
using JungleMCTS.GameBoard.GameFields;

namespace JungleMCTS.UI
{
    internal static class FieldUI
    {
        public static readonly int FieldHeight = 80;
        public static readonly int FieldWidth = 80;

        static public void DrawField(Bitmap bitmap, GameField? field, Position position)
        {
            Brush brush = Brushes.Black;
            Pen pen = new Pen(Brushes.Black);
            var positionOnScreen =
                new Position(
                    (Board.BoardLength - 1 - position.X) * FieldHeight,
                    position.Y * FieldWidth);
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
                FieldHeight,
                FieldWidth);
            g.DrawRectangle(pen,
                positionOnScreen.Y,
                positionOnScreen.X,
                FieldHeight,
                FieldWidth);
        }
    }
}
