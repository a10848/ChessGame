using ChessBoard;
using ChessBoard.Enums;

namespace ChessPlay
{

    class Queen : Piece
    {

        public Queen(Board chessBoard, Color color) : base(chessBoard, color)
        {
        }

        public override string ToString()
        {
            return "Q";
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

            // esquerda
            pos.DefineValue(position.line, position.column - 1);
            while (chessBoard.ValidPosition(pos) && CanMove(pos))
            {
                movements[pos.line, pos.column] = true;
                if (chessBoard.Piece(pos) != null && chessBoard.Piece(pos).color != color)
                {
                    break;
                }
                pos.DefineValue(pos.line, pos.column - 1);
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
                pos.DefineValue(pos.line, pos.column + 1);
            }

            // acima
            pos.DefineValue(position.line - 1, position.column);
            while (chessBoard.ValidPosition(pos) && CanMove(pos))
            {
                movements[pos.line, pos.column] = true;
                if (chessBoard.Piece(pos) != null && chessBoard.Piece(pos).color != color)
                {
                    break;
                }
                pos.DefineValue(pos.line - 1, pos.column);
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
                pos.DefineValue(pos.line + 1, pos.column);
            }

            // NO
            pos.DefineValue(position.line - 1, position.column - 1);
            while (chessBoard.ValidPosition(pos) && CanMove(pos))
            {
                movements[pos.line, pos.column] = true;
                if (chessBoard.Piece(pos) != null && chessBoard.Piece(pos).color != color)
                {
                    break;
                }
                pos.DefineValue(pos.line - 1, pos.column - 1);
            }

            // NE
            pos.DefineValue(position.line - 1, position.column + 1);
            while (chessBoard.ValidPosition(pos) && CanMove(pos))
            {
                movements[pos.line, pos.column] = true;
                if (chessBoard.Piece(pos) != null && chessBoard.Piece(pos).color != color)
                {
                    break;
                }
                pos.DefineValue(pos.line - 1, pos.column + 1);
            }

            // SE
            pos.DefineValue(position.line + 1, position.column + 1);
            while (chessBoard.ValidPosition(pos) && CanMove(pos))
            {
                movements[pos.line, pos.column] = true;
                if (chessBoard.Piece(pos) != null && chessBoard.Piece(pos).color != color)
                {
                    break;
                }
                pos.DefineValue(pos.line + 1, pos.column + 1);
            }

            // SO
            pos.DefineValue(position.line + 1, position.column - 1);
            while (chessBoard.ValidPosition(pos) && CanMove(pos))
            {
                movements[pos.line, pos.column] = true;
                if (chessBoard.Piece(pos) != null && chessBoard.Piece(pos).color != color)
                {
                    break;
                }
                pos.DefineValue(pos.line + 1, pos.column - 1);
            }

            return movements;
        }
    }
}