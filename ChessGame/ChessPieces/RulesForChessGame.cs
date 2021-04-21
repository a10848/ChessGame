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

        public RulesForChessGame()
        {
            Board = new Board(8, 8);
            Round = 1;
            Player = Color.White;
            PositioningPieces();
            Finished = false;
        }

        public void PieceMovement(Position origin, Position final)
        {
            Piece piece = Board.RemovePiece(origin);
            piece.IncrementQtyMovements();
            Piece capturedPiece = Board.RemovePiece(final);
            Board.AddPiece(piece, final);
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

        private void PositioningPieces()
        {
            Board.AddPiece(new Tower(Board, Color.White), new ChessPiecesPosition('a', 1).ToPosition());
            Board.AddPiece(new Tower(Board, Color.White), new ChessPiecesPosition('h', 1).ToPosition());
            Board.AddPiece(new Tower(Board, Color.Black), new ChessPiecesPosition('a', 8).ToPosition());
            Board.AddPiece(new Tower(Board, Color.Black), new ChessPiecesPosition('h', 8).ToPosition());
            Board.AddPiece(new King(Board, Color.White), new ChessPiecesPosition('e', 1).ToPosition());
            Board.AddPiece(new King(Board, Color.Black), new ChessPiecesPosition('e', 8).ToPosition());
        }
    }
}
