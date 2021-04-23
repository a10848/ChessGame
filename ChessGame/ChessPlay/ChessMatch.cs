using System.Collections.Generic;
using ChessBoard;
using ChessBoard.Enums;
using ChessBoard.Exceptions;
using ChessPlay.ChessPieces;

namespace ChessPlay
{
    class ChessMatch
    {

        public Board chessBoard { get; private set; }
        public int round { get; private set; }
        public Color currentPlayer { get; private set; }
        public bool finished { get; private set; }
        private HashSet<Piece> pieces;
        private HashSet<Piece> capturedPieces;
        public bool check { get; private set; }
        public Piece enPassantVulnerability { get; private set; }

        public ChessMatch()
        {
            chessBoard = new Board(8, 8);
            round = 1;
            currentPlayer = Color.White;
            finished = false;
            check = false;
            enPassantVulnerability = null;
            pieces = new HashSet<Piece>();
            capturedPieces = new HashSet<Piece>();
            PlacePieces();
        }

        public Piece PerformMovement(Position origin, Position destiny)
        {
            Piece piece = chessBoard.RemoveAPiece(origin);
            piece.IncreaseNumberOfMovements();
            Piece capturedPiece = chessBoard.RemoveAPiece(destiny);
            chessBoard.PlaceAPiece(piece, destiny);
            if (capturedPiece != null)
            {
                capturedPieces.Add(capturedPiece);
            }

            // #casteling small
            if (piece is King && destiny.column == origin.column + 2)
            {
                Position originRook = new Position(origin.line, origin.column + 3);
                Position destinyRook = new Position(origin.line, origin.column + 1);
                Piece rook = chessBoard.RemoveAPiece(originRook);
                rook.IncreaseNumberOfMovements();
                chessBoard.PlaceAPiece(rook, destinyRook);
            }

            // #casteling big
            if (piece is King && destiny.column == origin.column - 2)
            {
                Position originRook = new Position(origin.line, origin.column - 4);
                Position destinyRook = new Position(origin.line, origin.column - 1);
                Piece rook = chessBoard.RemoveAPiece(originRook);
                rook.IncreaseNumberOfMovements();
                chessBoard.PlaceAPiece(rook, destinyRook);
            }

            // #en passant
            if (piece is Pawn)
            {
                if (origin.column != destiny.column && capturedPiece == null)
                {
                    Position positionPawn;
                    if (piece.color == Color.White)
                    {
                        positionPawn = new Position(destiny.line + 1, destiny.column);
                    }
                    else
                    {
                        positionPawn = new Position(destiny.line - 1, destiny.column);
                    }
                    capturedPiece = chessBoard.RemoveAPiece(positionPawn);
                    capturedPieces.Add(capturedPiece);
                }
            }

            return capturedPiece;
        }

        public void UnduMovement(Position origin, Position destiny, Piece capturedPiece)
        {
            Piece piece = chessBoard.RemoveAPiece(destiny);
            piece.DecreaseNumberOfMovements();
            if (capturedPiece != null)
            {
                chessBoard.PlaceAPiece(capturedPiece, destiny);
                capturedPieces.Remove(capturedPiece);
            }
            chessBoard.PlaceAPiece(piece, origin);

            // #casteling small
            if (piece is King && destiny.column == origin.column + 2)
            {
                Position originRook = new Position(origin.line, origin.column + 3);
                Position destinyRook = new Position(origin.line, origin.column + 1);
                Piece rook = chessBoard.RemoveAPiece(destinyRook);
                rook.DecreaseNumberOfMovements();
                chessBoard.PlaceAPiece(rook, originRook);
            }

            // #casteling big
            if (piece is King && destiny.column == origin.column - 2)
            {
                Position originRook = new Position(origin.line, origin.column - 4);
                Position destinyRook = new Position(origin.line, origin.column - 1);
                Piece rook = chessBoard.RemoveAPiece(destinyRook);
                rook.DecreaseNumberOfMovements();
                chessBoard.PlaceAPiece(rook, originRook);
            }

            // #en passant
            if (piece is Pawn)
            {
                if (origin.column != destiny.column && capturedPiece == enPassantVulnerability)
                {
                    Piece pawn = chessBoard.RemoveAPiece(destiny);
                    Position positionPawn;
                    if (piece.color == Color.White)
                    {
                        positionPawn = new Position(3, destiny.column);
                    }
                    else
                    {
                        positionPawn = new Position(4, destiny.column);
                    }
                    chessBoard.PlaceAPiece(pawn, positionPawn);
                }
            }
        }

        public void MoveMade(Position origin, Position destiny)
        {
            Piece capturedPiece = PerformMovement(origin, destiny);

            if (IsInCheck(currentPlayer))
            {
                UnduMovement(origin, destiny, capturedPiece);
                throw new BoardException("You can't put yourself in check!");
            }

            Piece piece = chessBoard.Piece(destiny);

            // #promotion
            if (piece is Pawn)
            {
                if ((piece.color == Color.White && destiny.line == 0) || (piece.color == Color.Black && destiny.line == 7))
                {
                    piece = chessBoard.RemoveAPiece(destiny);
                    pieces.Remove(piece);
                    Piece queen = new Queen(chessBoard, piece.color);
                    chessBoard.PlaceAPiece(queen, destiny);
                    pieces.Add(queen);
                }
            }

            if (IsInCheck(Opponent(currentPlayer)))
            {
                check = true;
            }
            else
            {
                check = false;
            }

            if (TestCheckmate(Opponent(currentPlayer)))
            {
                finished = true;
            }
            else
            {
                round++;
                ChangePlayer();
            }

            // #en passant
            if (piece is Pawn && (destiny.line == origin.line - 2 || destiny.line == origin.line + 2))
            {
                enPassantVulnerability = piece;
            }
            else
            {
                enPassantVulnerability = null;
            }

        }

        public void ValidateOriginPosition(Position position)
        {
            if (chessBoard.Piece(position) == null)
            {
                throw new BoardException("There is no piece in the chosen position!");
            }
            if (currentPlayer != chessBoard.Piece(position).color)
            {
                throw new BoardException("The chosen piece is not yours!");
            }
            if (!chessBoard.Piece(position).IsTherePossibleMovements())
            {
                throw new BoardException("There are no possible movements for the chosen piece!");
            }
        }

        public void ValidadeDestinyPosition(Position origin, Position destiny)
        {
            if (!chessBoard.Piece(origin).PossibleMovement(destiny))
            {
                throw new BoardException("Invalid destiny position!");
            }
        }

        private void ChangePlayer()
        {
            if (currentPlayer == Color.White)
            {
                currentPlayer = Color.Black;
            }
            else
            {
                currentPlayer = Color.White;
            }
        }

        public HashSet<Piece> CapturedPieces(Color color)
        {
            HashSet<Piece> auxPieces = new HashSet<Piece>();
            foreach (Piece piece in capturedPieces)
            {
                if (piece.color == color)
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
                if (piece.color == color)
                {
                    auxPieces.Add(piece);
                }
            }
            auxPieces.ExceptWith(CapturedPieces(color));
            return auxPieces;
        }

        private Color Opponent(Color cor)
        {
            if (cor == Color.White)
            {
                return Color.Black;
            }
            else
            {
                return Color.White;
            }
        }

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

        public bool IsInCheck(Color color)
        {
            Piece king = King(color);
            if (king == null)
            {
                throw new BoardException("There isn't a " + color + " king on the board!");
            }
            foreach (Piece piece in PiecesInPlay(Opponent(color)))
            {
                bool[,] movements = piece.PossibleMovements();
                if (movements[king.position.line, king.position.column])
                {
                    return true;
                }
            }
            return false;
        }

        public bool TestCheckmate(Color color)
        {
            if (!IsInCheck(color))
            {
                return false;
            }
            foreach (Piece piece in PiecesInPlay(color))
            {
                bool[,] movements = piece.PossibleMovements();
                for (int i = 0; i < chessBoard.line; i++)
                {
                    for (int j = 0; j < chessBoard.column; j++)
                    {
                        if (movements[i, j])
                        {
                            Position origin = piece.position;
                            Position destiny = new Position(i, j);
                            Piece capturedPiece = PerformMovement(origin, destiny);
                            bool testCheck = IsInCheck(color);
                            UnduMovement(origin, destiny, capturedPiece);
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

        public void PlaceANewPiece(char column, int line, Piece piece)
        {
            chessBoard.PlaceAPiece(piece, new PiecesPosition(column, line).ToPosition());
            pieces.Add(piece);
        }

        private void PlacePieces()
        {
            PlaceANewPiece('a', 1, new Rook(chessBoard, Color.White));
            PlaceANewPiece('b', 1, new Knight(chessBoard, Color.White));
            PlaceANewPiece('c', 1, new Bishop(chessBoard, Color.White));
            PlaceANewPiece('d', 1, new Queen(chessBoard, Color.White));
            PlaceANewPiece('e', 1, new King(chessBoard, Color.White, this));
            PlaceANewPiece('f', 1, new Bishop(chessBoard, Color.White));
            PlaceANewPiece('g', 1, new Knight(chessBoard, Color.White));
            PlaceANewPiece('h', 1, new Rook(chessBoard, Color.White));
            PlaceANewPiece('a', 2, new Pawn(chessBoard, Color.White, this));
            PlaceANewPiece('b', 2, new Pawn(chessBoard, Color.White, this));
            PlaceANewPiece('c', 2, new Pawn(chessBoard, Color.White, this));
            PlaceANewPiece('d', 2, new Pawn(chessBoard, Color.White, this));
            PlaceANewPiece('e', 2, new Pawn(chessBoard, Color.White, this));
            PlaceANewPiece('f', 2, new Pawn(chessBoard, Color.White, this));
            PlaceANewPiece('g', 2, new Pawn(chessBoard, Color.White, this));
            PlaceANewPiece('h', 2, new Pawn(chessBoard, Color.White, this));

            PlaceANewPiece('a', 8, new Rook(chessBoard, Color.Black));
            PlaceANewPiece('b', 8, new Knight(chessBoard, Color.Black));
            PlaceANewPiece('c', 8, new Bishop(chessBoard, Color.Black));
            PlaceANewPiece('d', 8, new Queen(chessBoard, Color.Black));
            PlaceANewPiece('e', 8, new King(chessBoard, Color.Black, this));
            PlaceANewPiece('f', 8, new Bishop(chessBoard, Color.Black));
            PlaceANewPiece('g', 8, new Knight(chessBoard, Color.Black));
            PlaceANewPiece('h', 8, new Rook(chessBoard, Color.Black));
            PlaceANewPiece('a', 7, new Pawn(chessBoard, Color.Black, this));
            PlaceANewPiece('b', 7, new Pawn(chessBoard, Color.Black, this));
            PlaceANewPiece('c', 7, new Pawn(chessBoard, Color.Black, this));
            PlaceANewPiece('d', 7, new Pawn(chessBoard, Color.Black, this));
            PlaceANewPiece('e', 7, new Pawn(chessBoard, Color.Black, this));
            PlaceANewPiece('f', 7, new Pawn(chessBoard, Color.Black, this));
            PlaceANewPiece('g', 7, new Pawn(chessBoard, Color.Black, this));
            PlaceANewPiece('h', 7, new Pawn(chessBoard, Color.Black, this));
        }
    }
}