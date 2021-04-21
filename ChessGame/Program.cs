using System;
using ChessGame.ChessBoard;
using ChessGame.ChessBoard.Enums;

namespace ChessGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Board b = new Board(8, 8);

            Console.WriteLine(b);
        }
    }
}
