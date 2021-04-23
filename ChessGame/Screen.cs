using System;
using System.Collections.Generic;
using ChessBoard;
using ChessBoard.Enums;
using ChessPlay;

namespace ChessGame
{
    class Screen
    {

        public static void PrintScreen(ChessMatch match)
        {
            PrintBoard(match.chessBoard);
            Console.WriteLine();
            PrintCapturedPieces(match);
            Console.WriteLine();
            Console.WriteLine("Round: " + match.round);
            if (!match.finished)
            {
                Console.WriteLine("Player turn: " + match.currentPlayer);
                if (match.check)
                {
                    Console.WriteLine("Check!");
                }
            }
            else
            {
                Console.WriteLine("Checkmate!");
                Console.WriteLine("Winner: " + match.currentPlayer);
            }
        }

        public static void PrintCapturedPieces(ChessMatch match)
        {
            Console.WriteLine("Captured pieces:");
            Console.Write("White: ");
            ConsoleColor auxColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            PrintAllPieces(match.CapturedPieces(Color.White));
            Console.ForegroundColor = auxColor;
            Console.WriteLine();
            Console.Write("Black: ");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            PrintAllPieces(match.CapturedPieces(Color.Black));
            Console.ForegroundColor = auxColor;
            Console.WriteLine();
        }

        public static void PrintAllPieces(HashSet<Piece> pieces)
        {
            Console.Write("[ ");
            foreach (Piece piece in pieces)
            {
                Console.Write(piece + " ");
            }
            Console.Write("]");
        }

        public static void PrintBoard(Board chessBoard)
        {

            for (int i = 0; i < chessBoard.line; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < chessBoard.column; j++)
                {
                    PrintPiece(chessBoard.Piece(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("   a  b  c  d  e  f  g  h");
        }

        public static void PrintPossibleMovements(Board chessBoard, bool[,] possiblePositions, ChessMatch match)
        {
            ConsoleColor originalBackgroung = Console.BackgroundColor;
            ConsoleColor newBackground = ConsoleColor.DarkGray;

            for (int i = 0; i < chessBoard.line; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < chessBoard.column; j++)
                {
                    if (possiblePositions[i, j])
                    {
                        Console.BackgroundColor = newBackground;
                    }
                    else
                    {
                        Console.BackgroundColor = originalBackgroung;
                    }
                    PrintPiece(chessBoard.Piece(i, j));
                    Console.BackgroundColor = originalBackgroung;
                }
                Console.WriteLine();
            }
            Console.WriteLine("   a  b  c  d  e  f  g  h");
            Console.BackgroundColor = originalBackgroung;

            Console.WriteLine();
            PrintCapturedPieces(match);
            Console.WriteLine();
            Console.WriteLine("Round: " + match.round);
            if (!match.finished)
            {
                Console.WriteLine("Player turn: " + match.currentPlayer);
                if (match.check)
                {
                    Console.WriteLine("Check!");
                }
            }
            else
            {
                Console.WriteLine("Checkmate!");
                Console.WriteLine("Winner: " + match.currentPlayer);
            }
        }

        public static PiecesPosition ReadPosition()
        {
            string textRead = Console.ReadLine();
            char column = textRead[0];
            int line = int.Parse(textRead[1] + "");
            return new PiecesPosition(column, line);
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
                if (piece.color == Color.White)
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