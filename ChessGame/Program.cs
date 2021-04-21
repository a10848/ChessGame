using System;
using ChessGame.ChessBoard;
using ChessGame.ChessPieces;
using ChessGame.ChessBoard.Enums;

namespace ChessGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(8, 8);

            board.AddPiece(new Tower(board, Color.Black), new Position(0, 0));
            board.AddPiece(new Tower(board, Color.Black), new Position(1, 3));
            board.AddPiece(new King(board, Color.Black), new Position(2, 4));

            Screen.PrintScreen(board);
        }
    }
}
