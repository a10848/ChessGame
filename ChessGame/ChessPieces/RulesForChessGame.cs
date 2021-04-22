using System;
using System.Collections.Generic;
using System.Text;
using ChessBoard;
using ChessBoard.Enums;
using ChessBoard.Exceptions;
using ChessPieces;

namespace ChessPieces
{
    class RulesForChessGame
    {
        public Board Board { get; private set; }
        public int Round { get; private set; }
        public Color Player { get; private set; }
        public bool Finished { get; private set; }
        private HashSet<Piece> pieces;
        private HashSet<Piece> capturedPieces;
        public bool Check { get; private set; }

        public RulesForChessGame()
        {
            Board = new Board(8, 8);
            Round = 1;
            Player = Color.White;
            Finished = false;
            Check = false;
            pieces = new HashSet<Piece>();
            capturedPieces = new HashSet<Piece>();
            PositioningPieces();
        }

        /// <summary>
        /// this piece makes a movement for any piece
        /// </summary>
        /// <param name="origin">choose the piece to be played</param>
        /// <param name="final">choose de final destiny for the piece</param>
        /// <returns>returns a piece if the movement cannot be made</returns>
        public Piece PieceMovement(Position origin, Position final)
        {
            Piece piece = Board.RemovePiece(origin);

            piece.IncrementQtyMovements();
            Piece capturedPiece = Board.RemovePiece(final);
            Board.AddPiece(piece, final);
            if (capturedPiece != null)
            {
                capturedPieces.Add(capturedPiece);
            }
            return capturedPiece;
        }

        /// <summary>
        /// this function undo the movement thas was made previously
        /// </summary>
        /// <param name="origin">origin place from the piece being moved</param>
        /// <param name="final">final place for the piece being moved</param>
        /// <param name="capturedPiece">piece thats being captured</param>
        public void UndoMovement(Position origin, Position final, Piece capturedPiece)
        {
            Piece piece = Board.RemovePiece(final);
            piece.DeIncrementQtyMovements();
            if (capturedPiece != null)
            {
                Board.AddPiece(capturedPiece, final);
                capturedPieces.Remove(capturedPiece);
            }
            Board.AddPiece(piece, origin);
        }

        /// <summary>
        /// this function makes a move
        /// </summary>
        /// <param name="origin">piece being moved</param>
        /// <param name="final">final destity for the piece</param>
        public void MakeAMovement(Position origin, Position final)
        {
            Piece capturedPiece = PieceMovement(origin, final);

            if (IsInCheck(Player))
            {
                UndoMovement(origin, final, capturedPiece);
                throw new BoardException("You cannot put yourself in check!");
            }

            if (IsInCheck(Opponent(Player)))
            {
                Check = true;
            }
            else
            {
                Check = false;
            }

            if (TestCheckmate(Opponent(Player)))
            {
                Finished = true;
            }
            else
            {
                Round++;
                ChangePlayer();
            }
        }

        public void ChangePlayer()
        {
            if (Player == Color.White)
            {
                Player = Color.Black;
            }
            else
            {
                Player = Color.White;
            }
        }

        public HashSet<Piece> CapturedPieces(Color color)
        {
            HashSet<Piece> auxPieces = new HashSet<Piece>();

            foreach (Piece piece in capturedPieces)
            {
                if (piece.Color == color)
                {
                    auxPieces.Add(piece);
                }
            }

            return auxPieces;
        }
        public HashSet<Piece> PiecesInPlay(Color color)
        {
            HashSet<Piece> auxPieces = new HashSet<Piece>();

            foreach (Piece piece in pieces)
            {
                if (piece.Color == color)
                {
                    auxPieces.Add(piece);
                }
            }

            auxPieces.ExceptWith(CapturedPieces(color));

            return auxPieces;
        }

        /// <summary>
        /// this function its use to return the color of the opponent.
        /// </summary>
        /// <param name="color">color of the chess piece</param>
        /// <returns>returns color of the opponent</returns>
        private Color Opponent(Color color)
        {
            if (color == Color.White)
            {
                return Color.Black;
            }
            else
            {
                return Color.White;
            }
        }

        /// <summary>
        /// this function returns which piece in play is the king of determined color
        /// </summary>
        /// <param name="color">color of the piece</param>
        /// <returns>returns the king or nothig if theres none</returns>
        private Piece King(Color color)
        {
            foreach (Piece piece in PiecesInPlay(color))
            {
                if (piece is King)
                {
                    return piece;
                }
            }
            return null;
        }

        /// <summary>
        /// this function is checking for all the pieces of the opponent if theres any movement that can hit the king
        /// </summary>
        /// <param name="color">color of the piece</param>
        /// <returns>returns if the king is in check or not</returns>
        public bool IsInCheck(Color color)
        {
            Piece king = King(color);
            if (king == null)
            {
                throw new BoardException("Theres no " + color + " King on the board!");
            }

            foreach (Piece piece in PiecesInPlay(Opponent(color)))
            {
                bool[,] movements = piece.PossibleMovements();
                if (movements[king.Position.Line, king.Position.Column])
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// this function is testing if the player is in checkmate or not
        /// </summary>
        /// <param name="color">color of the piece</param>
        /// <returns>returns if theres a checkmate or not</returns>
        public bool TestCheckmate(Color color)
        {
            if (!IsInCheck(color))
            {
                return false;
            }

            foreach (Piece piece in PiecesInPlay(color))
            {
                bool[,] movements = piece.PossibleMovements();

                for (int i = 0; i < Board.Lines; i++)
                {
                    for (int j = 0; j < Board.Columns; j++)
                    {
                        if (movements[i, j])
                        {
                            Position origin = piece.Position;
                            Position final = new Position(i, j);
                            Piece capturedPiece = PieceMovement(origin, final);
                            bool testCheck = IsInCheck(color);
                            UndoMovement(origin, final, capturedPiece);
                            if (!testCheck)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void ValidateOriginPosition(Position position)
        {
            if (Board.Piece(position) == null)
            {
                throw new BoardException("This position does not have a piece to moove!");
            }
            if (Player != Board.Piece(position).Color)
            {
                throw new BoardException("This piece does not belong to you!");
            }
            if (!Board.Piece(position).ThereArePossibleMovements())
            {
                throw new BoardException("This piece cannot moove!");
            }
        }

        public void ValidateFinalPosition(Position origin, Position final)
        {
            if (!Board.Piece(origin).CanBeMoved(final))
            {
                throw new BoardException("This piece cannot be mooved to this position!");
            }
        }

        public void AddNewPiece(char column, int line, Piece piece)
        {
            Board.AddPiece(piece, new ChessPiecesPosition(column, line).ToPosition());
            pieces.Add(piece);
        }

        private void PositioningPieces()
        {
            AddNewPiece('c', 1, new Tower(Board, Color.White));
            AddNewPiece('h', 7, new Tower(Board, Color.White));
            AddNewPiece('d', 1, new King(Board, Color.White));
            AddNewPiece('b', 8, new Tower(Board, Color.Black));
            AddNewPiece('a', 8, new King(Board, Color.Black));
        }
    }
}
