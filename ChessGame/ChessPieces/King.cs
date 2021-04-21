using ChessGame.ChessBoard;
using ChessGame.ChessBoard.Enums;

namespace ChessGame.ChessPieces
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
