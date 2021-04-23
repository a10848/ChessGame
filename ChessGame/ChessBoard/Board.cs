using ChessBoard.Exceptions;

namespace ChessBoard
{
    class Board
    {

        public int line { get; set; }
        public int column { get; set; }
        private Piece[,] pieces;

        public Board(int line, int column)
        {
            this.line = line;
            this.column = column;
            pieces = new Piece[line, column];
        }

        public Piece Piece(int line, int column)
        {
            return pieces[line, column];
        }

        public Piece Piece(Position position)
        {
            return pieces[position.line, position.column];
        }

        public bool IsThereAPiece(Position position)
        {
            ValidatePosition(position);
            return Piece(position) != null;
        }

        public void PlaceAPiece(Piece piece, Position position)
        {
            if (IsThereAPiece(position))
            {
                throw new BoardException("There is already a piece in that position!");
            }
            pieces[position.line, position.column] = piece;
            piece.position = position;
        }

        public Piece RemoveAPiece(Position position)
        {
            if (Piece(position) == null)
            {
                return null;
            }
            Piece auxPiece = Piece(position);
            auxPiece.position = null;
            pieces[position.line, position.column] = null;
            return auxPiece;
        }

        public bool ValidPosition(Position position)
        {
            if (position.line < 0 || position.line >= line || position.column < 0 || position.column >= column)
            {
                return false;
            }
            return true;
        }

        public void ValidatePosition(Position position)
        {
            if (!ValidPosition(position))
            {
                throw new BoardException("Invalid position!");
            }
        }
    }
}