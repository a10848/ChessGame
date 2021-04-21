using ChessBoard.Enums;

namespace ChessBoard
{
    abstract class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int QtyMovements { get; protected set; }
        public Board Board { get; protected set; }

        public Piece(Color color, Board board)
        {
            Position = null;
            Color = color;
            Board = board;
            QtyMovements = 0;
        }

        public void IncrementQtyMovements()
        {
            QtyMovements++;
        }

        public abstract bool[,] PossibleMovements();
    }
}
