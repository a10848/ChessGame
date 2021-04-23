using ChessBoard.Enums;

namespace ChessBoard

{
    abstract class Piece
    {

        public Position position { get; set; }
        public Color color { get; protected set; }
        public int movementsQty { get; protected set; }
        public Board chessBoard { get; protected set; }

        public Piece(Board chessBoard, Color color)
        {
            position = null;
            this.chessBoard = chessBoard;
            this.color = color;
            movementsQty = 0;
        }

        public void IncreaseNumberOfMovements()
        {
            movementsQty++;
        }

        public void DecreaseNumberOfMovements()
        {
            movementsQty--;
        }

        public bool IsTherePossibleMovements()
        {
            bool[,] movements = PossibleMovements();
            for (int i = 0; i < chessBoard.line; i++)
            {
                for (int j = 0; j < chessBoard.column; j++)
                {
                    if (movements[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool PossibleMovement(Position position)
        {
            return PossibleMovements()[position.line, position.column];
        }

        public abstract bool[,] PossibleMovements();
    }
}