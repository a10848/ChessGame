using ChessGame.ChessBoard.Exceptions;

namespace ChessGame.ChessBoard
{
    class Board
    {
        public int Lines { get; set; }
        public int Columns { get; set; }
        private Piece[,] pieces;

        public Board(int lines, int columns)
        {
            Lines = lines;
            Columns = columns;
            this.pieces = new Piece[lines, columns];
        }

        public Piece Piece(int line, int column)
        {
            return pieces[line, column];
        }
        public Piece Piece(Position position)
        {
            return pieces[position.Line, position.Column];
        }
        public bool ExistingPiece(Position position)
        {
            PositionValidation(position);
            return Piece(position) != null;
        }

        public void AddPiece(Piece piece, Position position)
        {
            if (ExistingPiece(position))
            {
                throw new BoardException("This position already has a piece!");
            }
            pieces[position.Line, position.Column] = piece;
            piece.Position = position;
        }

        public bool ValidPosition(Position position)
        {
            if(position.Line<0 || position.Line>= Lines || position.Column < 0 || position.Column >= Columns)
            {
                return false;
            }
            return true;
        }

        public void PositionValidation(Position position)
        {
            if (!ValidPosition(position))
            {
                throw new BoardException("Invalid board position!");
            }
        }
    }
}
