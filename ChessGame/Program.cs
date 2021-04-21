using System;
using ChessGame.ChessBoard;
using ChessGame.ChessPieces;
using ChessGame.ChessBoard.Enums;
using ChessGame.ChessBoard.Exceptions;

namespace ChessGame
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Board board = new Board(8, 8);

                board.AddPiece(new Tower(board, Color.White), new Position(0, 0));
                board.AddPiece(new King(board, Color.Black), new Position(0, 4));
                board.AddPiece(new Tower(board, Color.White), new Position(1, 3));
                board.AddPiece(new King(board, Color.Black), new Position(2, 4));
                board.AddPiece(new Tower(board, Color.White), new Position(3, 3));
                board.AddPiece(new King(board, Color.Black), new Position(5, 4));

                Screen.PrintScreen(board);

                /*ChessPiecesPosition position = new ChessPiecesPosition('a', 1);

                Console.WriteLine(position);
                Console.WriteLine(position.ToPosition());*/

            }
            catch (BoardException error)
            {
                Console.WriteLine("Board error: " + error.Message);
            }
        }
    }
}
