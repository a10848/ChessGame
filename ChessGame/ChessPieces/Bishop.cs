using ChessBoard;
using ChessBoard.Enums;

namespace ChessPieces
{
    class Bishop : Piece
    {
        public Bishop(Board board, Color color) : base(color, board)
        {

        }

        public override string ToString()
        {
            return "B";
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

            // northwest
            pos.DefineValues(Position.Line - 1, Position.Column - 1);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                board[pos.Line, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                {
                    break;
                }
                pos.DefineValues(Position.Line - 1, Position.Column - 1);
            }

            // northest
            pos.DefineValues(Position.Line - 1, Position.Column + 1);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                board[pos.Line, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                {
                    break;
                }
                pos.DefineValues(Position.Line - 1, Position.Column + 1);
            }

            // southwest
            pos.DefineValues(Position.Line + 1, Position.Column - 1);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                board[pos.Line, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                {
                    break;
                }
                pos.DefineValues(Position.Line + 1, Position.Column - 1);
            }
            // southest
            pos.DefineValues(Position.Line + 1, Position.Column + 1);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                board[pos.Line, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                {
                    break;
                }
                pos.DefineValues(Position.Line + 1, Position.Column + 1);
            }

            return board;
        }
    }
}
