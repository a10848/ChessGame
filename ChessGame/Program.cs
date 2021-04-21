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
                    try
                    {
                        Console.Clear();
                        Screen.PrintChessPlay(chessGame);

                        Console.WriteLine();
                        Console.Write("Piece to move: ");
                        Position from = Screen.ReadChessPosition().ToPosition();
                        chessGame.ValidateOriginPosition(from);

                        bool[,] possiblePositions = chessGame.Board.Piece(from).PossibleMovements();

                        Console.Clear();
                        Screen.PrintScreen(chessGame.Board, possiblePositions);

                        Console.WriteLine();
                        Console.WriteLine("Round: " + chessGame.Round);
                        Console.WriteLine("Payer turn: " + chessGame.Player);

                        Console.WriteLine();
                        Console.Write("Move to: ");
                        Position to = Screen.ReadChessPosition().ToPosition();
                        chessGame.ValidateFinalPosition(from, to);

                        chessGame.MakeAMovement(from, to);
                    }
                    catch (BoardException error)
                    {
                        Console.WriteLine("Board error: " + error.Message);
                        Console.ReadKey();
                    }
                }

            }
            catch (BoardException error)
            {
                Console.WriteLine("Board error: " + error.Message);
            }
        }
    }
}
