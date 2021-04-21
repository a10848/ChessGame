using System;
using System.Collections.Generic;
using System.Text;
using ChessGame.ChessBoard.Enums;

namespace ChessGame.ChessBoard
{
    class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int QtyMovements { get; protected set; }
        public Board Board { get; protected set; }

        public Piece(Position position, Color color, Board board)
        {
            Position = position;
            Color = color;
            Board = board;
            QtyMovements = 0;
        }
    }
}
