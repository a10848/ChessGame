using ChessBoard;
using ChessBoard.Enums;

namespace ChessPlay.ChessPieces
{
    class Knight : Piece
    {
        public Knight(Board chessBoard, Color color) : base(chessBoard, color)
        {
        }

        public override string ToString()
        {
            return "H";
        }

        private bool CanMove(Position position)
        {
            Piece piece = chessBoard.Piece(position);
            return piece == null || piece.color != color;
        }

        public override bool[,] PossibleMovements()
        {
            bool[,] movements = new bool[chessBoard.line, chessBoard.column];
            Position pos = new Position(0, 0);

            pos.DefineValue(position.line - 1, position.column - 2);
            if (chessBoard.ValidPosition(pos) && CanMove(pos))
            {
                movements[pos.line, pos.column] = true;
            }
            pos.DefineValue(position.line - 2, position.column - 1);
            if (chessBoard.ValidPosition(pos) && CanMove(pos))
            {
                movements[pos.line, pos.column] = true;
            }
            pos.DefineValue(position.line - 2, position.column + 1);
            if (chessBoard.ValidPosition(pos) && CanMove(pos))
            {
                movements[pos.line, pos.column] = true;
            }
            pos.DefineValue(position.line - 1, position.column + 2);
            if (chessBoard.ValidPosition(pos) && CanMove(pos))
            {
                movements[pos.line, pos.column] = true;
            }
            pos.DefineValue(position.line + 1, position.column + 2);
            if (chessBoard.ValidPosition(pos) && CanMove(pos))
            {
                movements[pos.line, pos.column] = true;
            }
            pos.DefineValue(position.line + 2, position.column + 1);
            if (chessBoard.ValidPosition(pos) && CanMove(pos))
            {
                movements[pos.line, pos.column] = true;
            }
            pos.DefineValue(position.line + 2, position.column - 1);
            if (chessBoard.ValidPosition(pos) && CanMove(pos))
            {
                movements[pos.line, pos.column] = true;
            }
            pos.DefineValue(position.line + 1, position.column - 2);
            if (chessBoard.ValidPosition(pos) && CanMove(pos))
            {
                movements[pos.line, pos.column] = true;
            }

            return movements;
        }
    }
}