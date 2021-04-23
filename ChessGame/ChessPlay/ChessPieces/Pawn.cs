using ChessBoard;
using ChessBoard.Enums;

namespace ChessPlay.ChessPieces
{
    class Pawn : Piece
    {
        private ChessMatch match;

        public Pawn(Board chessBoard, Color color, ChessMatch match) : base(chessBoard, color)
        {
            this.match = match;
        }

        public override string ToString()
        {
            return "P";
        }

        private bool OpponentFound(Position position)
        {
            Piece piece = chessBoard.Piece(position);
            return piece != null && piece.color != color;
        }

        private bool Free(Position position)
        {
            return chessBoard.Piece(position) == null;
        }

        public override bool[,] PossibleMovements()
        {
            bool[,] movements = new bool[chessBoard.line, chessBoard.column];

            Position pos = new Position(0, 0);

            if (color == Color.White)
            {
                pos.DefineValue(position.line - 1, position.column);
                if (chessBoard.ValidPosition(pos) && Free(pos))
                {
                    movements[pos.line, pos.column] = true;
                }
                pos.DefineValue(position.line - 2, position.column);
                Position pos2 = new Position(position.line - 1, position.column);
                if (chessBoard.ValidPosition(pos2) && Free(pos2) && chessBoard.ValidPosition(pos) && Free(pos) && movementsQty == 0)
                {
                    movements[pos.line, pos.column] = true;
                }
                pos.DefineValue(position.line - 1, position.column - 1);
                if (chessBoard.ValidPosition(pos) && OpponentFound(pos))
                {
                    movements[pos.line, pos.column] = true;
                }
                pos.DefineValue(position.line - 1, position.column + 1);
                if (chessBoard.ValidPosition(pos) && OpponentFound(pos))
                {
                    movements[pos.line, pos.column] = true;
                }

                // #en passant
                if (position.line == 3)
                {
                    Position left = new Position(position.line, position.column - 1);
                    if (chessBoard.ValidPosition(left) && OpponentFound(left) && chessBoard.Piece(left) == match.enPassantVulnerability)
                    {
                        movements[left.line - 1, left.column] = true;
                    }
                    Position right = new Position(position.line, position.column + 1);
                    if (chessBoard.ValidPosition(right) && OpponentFound(right) && chessBoard.Piece(right) == match.enPassantVulnerability)
                    {
                        movements[right.line - 1, right.column] = true;
                    }
                }
            }
            else
            {
                pos.DefineValue(position.line + 1, position.column);
                if (chessBoard.ValidPosition(pos) && Free(pos))
                {
                    movements[pos.line, pos.column] = true;
                }
                pos.DefineValue(position.line + 2, position.column);
                Position pos2 = new Position(position.line + 1, position.column);
                if (chessBoard.ValidPosition(pos2) && Free(pos2) && chessBoard.ValidPosition(pos) && Free(pos) && movementsQty == 0)
                {
                    movements[pos.line, pos.column] = true;
                }
                pos.DefineValue(position.line + 1, position.column - 1);
                if (chessBoard.ValidPosition(pos) && OpponentFound(pos))
                {
                    movements[pos.line, pos.column] = true;
                }
                pos.DefineValue(position.line + 1, position.column + 1);
                if (chessBoard.ValidPosition(pos) && OpponentFound(pos))
                {
                    movements[pos.line, pos.column] = true;
                }

                // #en passant
                if (position.line == 4)
                {
                    Position left = new Position(position.line, position.column - 1);
                    if (chessBoard.ValidPosition(left) && OpponentFound(left) && chessBoard.Piece(left) == match.enPassantVulnerability)
                    {
                        movements[left.line + 1, left.column] = true;
                    }
                    Position right = new Position(position.line, position.column + 1);
                    if (chessBoard.ValidPosition(right) && OpponentFound(right) && chessBoard.Piece(right) == match.enPassantVulnerability)
                    {
                        movements[right.line + 1, right.column] = true;
                    }
                }
            }

            return movements;
        }
    }
}