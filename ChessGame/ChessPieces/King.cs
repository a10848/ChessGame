using ChessBoard;
using ChessBoard.Enums;

namespace ChessPieces
{
    class King : Piece
    {
        public King(Board board, Color color) : base(color, board)
        {

        }

        public override string ToString()
        {
            return "K";
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

            // north
            pos.DefineValues(Position.Line - 1, Position.Column);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                board[pos.Line, pos.Column] = true;
            }

            // south
            pos.DefineValues(Position.Line + 1, Position.Column);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                board[pos.Line, pos.Column] = true;
            }

            // east
            pos.DefineValues(Position.Line, Position.Column + 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                board[pos.Line, pos.Column] = true;
            }

            // west
            pos.DefineValues(Position.Line, Position.Column - 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                board[pos.Line, pos.Column] = true;
            }

            // northeast
            pos.DefineValues(Position.Line - 1, Position.Column + 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                board[pos.Line, pos.Column] = true;
            }

            // southeast
            pos.DefineValues(Position.Line + 1, Position.Column + 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                board[pos.Line, pos.Column] = true;
            }

            // northweast
            pos.DefineValues(Position.Line - 1, Position.Column - 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                board[pos.Line, pos.Column] = true;
            }

            // southweast
            pos.DefineValues(Position.Line + 1, Position.Column - 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                board[pos.Line, pos.Column] = true;
            }

            return board;
        }
    }
}
