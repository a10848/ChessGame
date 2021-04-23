using ChessBoard;

namespace ChessPlay
{
    class PiecesPosition
    {
        public char column { get; set; }
        public int line { get; set; }

        public PiecesPosition(char column, int line)
        {
            this.column = column;
            this.line = line;
        }

        public Position ToPosition()
        {
            return new Position(8 - line, column - 'a');
        }

        public override string ToString()
        {
            return "" + column + line;
        }
    }
}