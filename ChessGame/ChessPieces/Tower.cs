using ChessGame.ChessBoard;
using ChessGame.ChessBoard.Enums;

namespace ChessGame.ChessPieces
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
    }
}
