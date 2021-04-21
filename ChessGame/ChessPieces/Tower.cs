using ChessBoard;
using ChessBoard.Enums;

namespace ChessPieces
{
    class Tower : Piece
    {
        public Tower(Board board, Color color) : base(color, board)
        {

        }

        public override string ToString()
        {
            return "T";
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
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                board[pos.Line, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                {
                    break;
                }
                pos.Line -= 1;
            }

            // south
            pos.DefineValues(Position.Line + 1, Position.Column);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                board[pos.Line, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                {
                    break;
                }
                pos.Line += 1;
            }

            // east
            pos.DefineValues(Position.Line, Position.Column + 1);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                board[pos.Line, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                {
                    break;
                }
                pos.Column += 1;
            }

            // west
            pos.DefineValues(Position.Line, Position.Column - 1);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                board[pos.Line, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                {
                    break;
                }
                pos.Column -= 1;
            }

            return board;
        }
    }
}
