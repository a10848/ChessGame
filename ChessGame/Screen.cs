using System;
using ChessGame.ChessBoard;
using ChessGame.ChessBoard.Enums;

namespace ChessGame
{
    class Screen
    {

        public static void PrintScreen(Board board)
        {
            for (int i = 0; i < board.Lines; i++)
            {
                Console.Write(8 - i + "  ");
                for (int j = 0; j < board.Lines; j++)
                {
                    if (board.Piece(i, j) == null)
                    {
                        Console.Write(" - ");
                    }
                    else
                    {
                        Console.Write(" ");
                        Screen.PrintPiece(board.Piece(i, j));
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("    a  b  c  d  e  f  g  h");
        }

        public static void PrintPiece(Piece piece)
        {
            if (piece.Color == Color.White)
            {
                ConsoleColor auxColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(piece);
                Console.ForegroundColor = auxColor;
            }
            else
            {
                ConsoleColor auxColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write(piece);
                Console.ForegroundColor = auxColor;
            }
        }
    }
}
