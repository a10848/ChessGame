using System;
using ChessBoard;
using ChessBoard.Exceptions;
using ChessPlay;

namespace ChessGame
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ChessMatch match = new ChessMatch();

                while (!match.finished)
                {
                    try
                    {
                        Console.Clear();
                        Screen.PrintScreen(match);

                        Console.WriteLine();
                        Console.Write("Piece: ");
                        Position origin = Screen.ReadPosition().ToPosition();
                        match.ValidateOriginPosition(origin);

                        bool[,] possiblePositions = match.chessBoard.Piece(origin).PossibleMovements();

                        Console.Clear();
                        Screen.PrintPossibleMovements(match.chessBoard, possiblePositions, match);

                        Console.WriteLine();
                        Console.Write("Move to: ");
                        Position destiny = Screen.ReadPosition().ToPosition();
                        match.ValidadeDestinyPosition(origin, destiny);

                        match.MoveMade(origin, destiny);
                    }
                    catch (BoardException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }
                Console.Clear();
                Screen.PrintScreen(match);
            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}