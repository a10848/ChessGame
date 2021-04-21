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
    }
}
