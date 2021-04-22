using System;
using System.Collections.Generic;
using ChessBoard;
using ChessBoard.Enums;
using ChessPieces;

namespace ChessGame
{
    class Screen
    {
        public static void PrintChessPlay(RulesForChessGame chessGame)
        {
            PrintScreen(chessGame.Board);
            Console.WriteLine();
            PrintCapturedPieces(chessGame);
            Console.WriteLine();
            Console.WriteLine("Round: " + chessGame.Round);
            Console.WriteLine("Payer turn: " + chessGame.Player);
            if (chessGame.Check)
            {
                Console.WriteLine("Check to the king!");
            }
        }

        public static void PrintCapturedPieces(RulesForChessGame chessGame)
        {
            Console.WriteLine("Captured pieces:");
            Console.Write("White: ");
            ConsoleColor auxColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            PrintAssembly(chessGame.CapturedPieces(Color.White));
            Console.ForegroundColor = auxColor;
            Console.WriteLine();
            Console.Write("Black: ");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            PrintAssembly(chessGame.CapturedPieces(Color.Black));
            Console.ForegroundColor = auxColor;
            Console.WriteLine();
        }

        public static void PrintAssembly(HashSet<Piece> pieces)
        {
            Console.Write("[");
            foreach (Piece piece in pieces)
            {
                Console.Write(piece + " ");
            }
            Console.Write("]");
        }

        public static void PrintScreen(Board board)
        {
            for (int i = 0; i < board.Lines; i++)
            {
                Console.Write(8 - i + "  ");
                for (int j = 0; j < board.Lines; j++)
                {
                    Screen.PrintPiece(board.Piece(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("    a  b  c  d  e  f  g  h");
        }

        public static void PrintScreen(Board board, bool[,] possiblePositions)
        {
            ConsoleColor originalBackground = Console.BackgroundColor;
            ConsoleColor newBackground = ConsoleColor.DarkGray;


            for (int i = 0; i < board.Lines; i++)
            {
                Console.Write(8 - i + "  ");
                for (int j = 0; j < board.Lines; j++)
                {
                    if (possiblePositions[i, j])
                    {
                        Console.BackgroundColor = newBackground;
                    }
                    else
                    {
                        Console.BackgroundColor = originalBackground;
                    }
                    Screen.PrintPiece(board.Piece(i, j));
                    Console.BackgroundColor = originalBackground;
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("    a  b  c  d  e  f  g  h");
            Console.BackgroundColor = originalBackground;
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
            if (piece == null)
            {
                Console.Write(" - ");
            }
            else
            {
                Console.Write(" ");
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
                Console.Write(" ");
            }
        }
    }
}
