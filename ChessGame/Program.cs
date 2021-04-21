using System;
using ChessBoard;
using ChessPieces;
using ChessBoard.Enums;
using ChessBoard.Exceptions;

namespace ChessGame
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                RulesForChessGame chessGame = new RulesForChessGame();

                while (!chessGame.Finished)
                {
                    Console.Clear();
                    Screen.PrintScreen(chessGame.Board);

                    Console.WriteLine();
                    Console.Write("From: ");
                    Position from = Screen.ReadChessPosition().ToPosition();
                    Console.Write("To: ");
                    Position to = Screen.ReadChessPosition().ToPosition();

                    chessGame.PieceMovement(from, to);
                }

            }
            catch (BoardException error)
            {
                Console.WriteLine("Board error: " + error.Message);
            }
        }
    }
}
