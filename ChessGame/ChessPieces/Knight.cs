using ChessBoard;
using ChessBoard.Enums;

namespace ChessPieces
{
    class Knight : Piece
    {
        public Knight(Board board, Color color) : base(color, board)
        {

        }

        public override string ToString()
        {
            return "H";
        }

        private bool CanMove(Position position)
        {
            Piece piece = Board.Piece(position);
            return piece == null || piece.Color != this.Color;
        }

        public override bool[,] PossibleMovements()
        {
            bool[,] board = new bool[Board.Lines, Board.Columns];

            Position pos = new Position(0, 0);

            pos.DefineValues(Position.Line - 1, Position.Column - 2);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                board[pos.Line, pos.Column] = true;
            }

            pos.DefineValues(Position.Line - 2, Position.Column - 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                board[pos.Line, pos.Column] = true;
            }

            pos.DefineValues(Position.Line - 2, Position.Column + 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                board[pos.Line, pos.Column] = true;
            }

            pos.DefineValues(Position.Line - 1, Position.Column + 2);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                board[pos.Line, pos.Column] = true;
            }

            pos.DefineValues(Position.Line + 1, Position.Column + 2);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                board[pos.Line, pos.Column] = true;
            }

            pos.DefineValues(Position.Line + 2, Position.Column + 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                board[pos.Line, pos.Column] = true;
            }

            pos.DefineValues(Position.Line + 2, Position.Column - 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                board[pos.Line, pos.Column] = true;
            }

            pos.DefineValues(Position.Line + 1, Position.Column - 2);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                board[pos.Line, pos.Column] = true;
            }

            return board;
        }
    }
}
