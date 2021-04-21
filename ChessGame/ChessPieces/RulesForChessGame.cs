using System;
using System.Collections.Generic;
using System.Text;
using ChessBoard;
using ChessBoard.Enums;
using ChessPieces;

namespace ChessPieces
{
    class RulesForChessGame
    {
        public Board Board { get; private set; }
        private int round;
        private Color player;
        public bool Finished { get; private set; }

        public RulesForChessGame()
        {
            Board = new Board(8, 8);
            round = 1;
            player = Color.White;
            PositioningPieces();
            Finished = false;
        }

        public void PieceMovement(Position initial, Position final)
        {
            Piece piece = Board.RemovePiece(initial);
            piece.IncrementQtyMovements();
            Piece capturedPiece = Board.RemovePiece(final);
            Board.AddPiece(piece, final);
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
