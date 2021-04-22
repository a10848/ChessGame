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

        public void DeIncrementQtyMovements()
        {
            QtyMovements--;
        }

        public bool ThereArePossibleMovements()
        {
            bool[,] movements = PossibleMovements();

            for (int i = 0; i < Board.Lines; i++)
            {
                for (int j = 0; j < Board.Lines; j++)
                {
                    if (movements[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool PossibleMove(Position position)
        {
            return PossibleMovements()[position.Line, position.Column];
        }

        public abstract bool[,] PossibleMovements();
    }
}
