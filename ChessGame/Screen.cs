using System;
using ChessBoard;
using ChessBoard.Enums;
using ChessPieces;

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

        public static ChessPiecesPosition ReadChessPosition()
        {
            string readPosition = Console.ReadLine();
            char column = readPosition[0];
            int line = int.Parse(readPosition[1] + "");
            return new ChessPiecesPosition(column, line);
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
