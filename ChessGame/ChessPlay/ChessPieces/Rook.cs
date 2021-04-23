using ChessBoard;
using ChessBoard.Enums;

namespace ChessPlay.ChessPieces
{
    class Rook : Piece
    {
        public Rook(Board tab, Color cor) : base(tab, cor)
        {
        }

        public override string ToString()
        {
            return "R";
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

            // acima
            pos.DefineValue(position.line - 1, position.column);
            while (chessBoard.ValidPosition(pos) && CanMove(pos))
            {
                movements[pos.line, pos.column] = true;
                if (chessBoard.Piece(pos) != null && chessBoard.Piece(pos).color != color)
                {
                    break;
                }
                pos.line = pos.line - 1;
            }

            // abaixo
            pos.DefineValue(position.line + 1, position.column);
            while (chessBoard.ValidPosition(pos) && CanMove(pos))
            {
                movements[pos.line, pos.column] = true;
                if (chessBoard.Piece(pos) != null && chessBoard.Piece(pos).color != color)
                {
                    break;
                }
                pos.line = pos.line + 1;
            }

            // direita
            pos.DefineValue(position.line, position.column + 1);
            while (chessBoard.ValidPosition(pos) && CanMove(pos))
            {
                movements[pos.line, pos.column] = true;
                if (chessBoard.Piece(pos) != null && chessBoard.Piece(pos).color != color)
                {
                    break;
                }
                pos.column = pos.column + 1;
            }

            // esquerda
            pos.DefineValue(position.line, position.column - 1);
            while (chessBoard.ValidPosition(pos) && CanMove(pos))
            {
                movements[pos.line, pos.column] = true;
                if (chessBoard.Piece(pos) != null && chessBoard.Piece(pos).color != color)
                {
                    break;
                }
                pos.column = pos.column - 1;
            }

            return movements;
        }
    }
}