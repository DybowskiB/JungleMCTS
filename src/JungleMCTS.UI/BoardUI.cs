using JungleMCTS.Enums;
using JungleMCTS.GameBoard;
using JungleMCTS.GamePiece;
using System.Numerics;

namespace JungleMCTS.UI
{
    internal class BoardUI
    {
        public Board board;
        public BoardUI(Board board)
        {
            this.board = board;
        }
        public void DrawBoard(PictureBox pictureBox)
        {
            var bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
            pictureBox.Image = bitmap;
            for (int i = 0; i < Board.BoardLength; i++)
            {
                for (int j = 0; j < Board.BoardWidth; j++)
                {
                    var position = new Position(i, j);
                    FieldUI.DrawField(bitmap, board.Fields[i, j], position);
                    if(board.Pieces[i, j] != null)
                    {
                        new PieceUI(board.Pieces[i, j]!).DrawPiece(bitmap, position);
                    }
                }
            }
        }

        public Piece? ChoosePiece(Vector2 PositionOnBoard)
        {
            int x = (int)PositionOnBoard.X / FieldUI.FieldHeight;
            int y = Board.BoardLength - 1 - ((int)PositionOnBoard.Y / FieldUI.FieldWidth);

            return board.Pieces[y, x];
        }

        public void DrawChoosenPiece(Piece chosenPiece, PictureBox pictureBox)
        {
            Position position = FindPiecePosition(chosenPiece);
            Bitmap bitmap = (Bitmap)(pictureBox.Image);
            new PieceUI(chosenPiece).DrawChosenPiece(bitmap, position);
            pictureBox.Image = bitmap;
        }

        public Position FindPiecePosition(Piece piece)
        {
            for (int i = 0; i < Board.BoardLength; i++)
            {
                for (int j = 0; j < Board.BoardWidth; j++)
                {
                    if(piece == board.Pieces[i, j])
                    {
                        return new Position(i, j);
                    }
                }
            }
            return null;
        }

        public bool MakeMove(Piece piece, Vector2 mousePosition, PlayerIdEnum playerId)
        {
            int x = (int)mousePosition.X / FieldUI.FieldHeight;
            int y = Board.BoardLength - 1 - ((int)mousePosition.Y / FieldUI.FieldWidth);
            var positionAfter = new Position(y, x);
            var positionBefore = FindPiecePosition(piece);

            var possibilePostions = piece.GetPossiblePositions(positionBefore, board);
            for (int i = 0; i < possibilePostions.Count; i++)
            {
                if (possibilePostions[i].Equals(positionAfter))
                {
                    board.Move(positionBefore, positionAfter);
                    return true;
                }
            }
            return false;
        }
    }
}
