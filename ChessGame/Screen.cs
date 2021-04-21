using System;
using System.Collections.Generic;
using System.Text;
using ChessGame.ChessBoard;

namespace ChessGame
{
    class Screen
    {

        public static void PrintScreen(Board board)
        {
            for (int i = 0; i < board.Lines; i++)
            {
                for (int j = 0; j < board.Lines; j++)
                {
                    if (board.piece(i, j) == null)
                    {
                        Console.Write("_ ");
                    }
                    else
                    {
                        Console.Write(board.piece(i, j) + " ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
