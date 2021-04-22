using ChessBoard;
using ChessBoard.Enums;

namespace ChessPieces
{
    class Pawn : Piece
    {
        public Pawn(Board board, Color color) : base(color, board)
        {

        }

        public override string ToString()
        {
            return "P";
        }

        private bool OpponentFound(Position position)
        {
            Piece piece = Board.Piece(position);
            return piece != null && piece.Color != Color;
        }

        private bool FreeToMove(Position position)
        {
            return Board.Piece(position) == null;
        }

        public override bool[,] PossibleMovements()
        {
            bool[,] board = new bool[Board.Lines, Board.Columns];

            Position pos = new Position(0, 0);

            if (Color == Color.White)
            {
                pos.DefineValues(Position.Line - 1, Position.Column);
                if (Board.ValidPosition(pos) && FreeToMove(pos))
                {
                    board[pos.Line, pos.Column] = true;
                }

                pos.DefineValues(Position.Line - 2, Position.Column);
                if (Board.ValidPosition(pos) && FreeToMove(pos) && QtyMovements == 0)
                {
                    board[pos.Line, pos.Column] = true;
                }

                pos.DefineValues(Position.Line - 1, Position.Column - 1);
                if (Board.ValidPosition(pos) && OpponentFound(pos))
                {
                    board[pos.Line, pos.Column] = true;
                }

                pos.DefineValues(Position.Line - 1, Position.Column + 1);
                if (Board.ValidPosition(pos) && OpponentFound(pos))
                {
                    board[pos.Line, pos.Column] = true;
                }
            }
            else
            {
                pos.DefineValues(Position.Line + 1, Position.Column);
                if (Board.ValidPosition(pos) && FreeToMove(pos))
                {
                    board[pos.Line, pos.Column] = true;
                }

                pos.DefineValues(Position.Line + 2, Position.Column);
                if (Board.ValidPosition(pos) && FreeToMove(pos) && QtyMovements == 0)
                {
                    board[pos.Line, pos.Column] = true;
                }

                pos.DefineValues(Position.Line + 1, Position.Column - 1);
                if (Board.ValidPosition(pos) && OpponentFound(pos))
                {
                    board[pos.Line, pos.Column] = true;
                }

                pos.DefineValues(Position.Line + 1, Position.Column + 1);
                if (Board.ValidPosition(pos) && OpponentFound(pos))
                {
                    board[pos.Line, pos.Column] = true;
                }
            }

            return board;
        }
    }
}
