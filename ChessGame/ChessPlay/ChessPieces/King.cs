using ChessBoard;
using ChessBoard.Enums;

namespace ChessPlay.ChessPieces
{
    class King : Piece
    {
        private ChessMatch match;

        public King(Board chessBoard, Color color, ChessMatch match) : base(chessBoard, color)
        {
            this.match = match;
        }

        public override string ToString()
        {
            return "K";
        }

        private bool CanMove(Position position)
        {
            Piece piece = chessBoard.Piece(position);
            return piece == null || piece.color != color;
        }

        private bool TestCastling(Position position)
        {
            Piece piece = chessBoard.Piece(position);
            return piece != null && piece is Rook && piece.color == color && piece.movementsQty == 0;
        }

        public override bool[,] PossibleMovements()
        {
            bool[,] movements = new bool[chessBoard.line, chessBoard.column];
            Position pos = new Position(0, 0);

            // acima
            pos.DefineValue(position.line - 1, position.column);
            if (chessBoard.ValidPosition(pos) && CanMove(pos))
            {
                movements[pos.line, pos.column] = true;
            }
            // ne
            pos.DefineValue(position.line - 1, position.column + 1);
            if (chessBoard.ValidPosition(pos) && CanMove(pos))
            {
                movements[pos.line, pos.column] = true;
            }
            // direita
            pos.DefineValue(position.line, position.column + 1);
            if (chessBoard.ValidPosition(pos) && CanMove(pos))
            {
                movements[pos.line, pos.column] = true;
            }
            // se
            pos.DefineValue(position.line + 1, position.column + 1);
            if (chessBoard.ValidPosition(pos) && CanMove(pos))
            {
                movements[pos.line, pos.column] = true;
            }
            // abaixo
            pos.DefineValue(position.line + 1, position.column);
            if (chessBoard.ValidPosition(pos) && CanMove(pos))
            {
                movements[pos.line, pos.column] = true;
            }
            // so
            pos.DefineValue(position.line + 1, position.column - 1);
            if (chessBoard.ValidPosition(pos) && CanMove(pos))
            {
                movements[pos.line, pos.column] = true;
            }
            // esquerda
            pos.DefineValue(position.line, position.column - 1);
            if (chessBoard.ValidPosition(pos) && CanMove(pos))
            {
                movements[pos.line, pos.column] = true;
            }
            // no
            pos.DefineValue(position.line - 1, position.column - 1);
            if (chessBoard.ValidPosition(pos) && CanMove(pos))
            {
                movements[pos.line, pos.column] = true;
            }

            // #jogadaespecial roque
            if (movementsQty == 0 && !match.check)
            {
                // #jogadaespecial roque pequeno
                Position positionRook1 = new Position(position.line, position.column + 3);
                if (TestCastling(positionRook1))
                {
                    Position position1 = new Position(position.line, position.column + 1);
                    Position position2 = new Position(position.line, position.column + 2);
                    if (chessBoard.Piece(position1) == null && chessBoard.Piece(position2) == null)
                    {
                        movements[position.line, position.column + 2] = true;
                    }
                }
                // #jogadaespecial roque grande
                Position positionRook2 = new Position(position.line, position.column - 4);
                if (TestCastling(positionRook2))
                {
                    Position position1 = new Position(position.line, position.column - 1);
                    Position position2 = new Position(position.line, position.column - 2);
                    Position position3 = new Position(position.line, position.column - 3);
                    if (chessBoard.Piece(position1) == null && chessBoard.Piece(position2) == null && chessBoard.Piece(position3) == null)
                    {
                        movements[position.line, position.column - 2] = true;
                    }
                }
            }

            return movements;
        }
    }
}