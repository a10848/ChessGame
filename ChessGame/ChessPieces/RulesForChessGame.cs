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

        public RulesForChessGame()
        {
            Board = new Board(8, 8);
            Round = 1;
            Player = Color.White;
            Finished = false;
            pieces = new HashSet<Piece>();
            capturedPieces = new HashSet<Piece>();
            PositioningPieces();
        }

        public void PieceMovement(Position origin, Position final)
        {
            Piece piece = Board.RemovePiece(origin);
            piece.IncrementQtyMovements();
            Piece capturedPiece = Board.RemovePiece(final);
            Board.AddPiece(piece, final);
            if (capturedPiece != null)
            {
                capturedPieces.Add(capturedPiece);
            }
        }

        public void MakeAMovement(Position origin, Position final)
        {
            PieceMovement(origin, final);
            Round++;
            ChangePlayer();
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
            AddNewPiece('a', 1, new Tower(Board, Color.White));
            AddNewPiece('h', 1, new Tower(Board, Color.White));
            AddNewPiece('a', 8, new Tower(Board, Color.Black));
            AddNewPiece('h', 8, new Tower(Board, Color.Black));
            AddNewPiece('e', 1, new King(Board, Color.White));
            AddNewPiece('e', 8, new King(Board, Color.Black));
        }
    }
}
