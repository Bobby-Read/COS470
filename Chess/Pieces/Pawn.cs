using System;
using System.Collections.Generic;
using System.Text;

namespace Chess.Pieces
{
    public class Pawn : Piece // Aka nobbly one down front
    {
        private Vector Front => new Vector(0, Color == Color.White ? 1 : -1);

        public override Piece[][,] GetMoves(Piece[,] board)
        {
            var boards = new List<Piece[,]>();
            // First move, can go two (bot not capture, nor jump)
            var forward = Location + Front;
            var forwardTwo = forward + Front;
            if (!HasMoved
                && board[forward.X, forward.Y] == null
                && board[forwardTwo.X, forwardTwo.Y] == null)
            {
                // Can jump two!'
                if(TryMove<Pawn>(board, forwardTwo, out var newBoard))
                {
                    boards.Add(newBoard);
                }
            }
            // Can move one forward (but not capture)
            if (board[forward.X, forward.Y] == null)
            {
                if(!IsOnBoard(forwardTwo))
                {
                    if(TryMove<Knight>(board, forward, out var newKnight))
                    {
                        boards.Add(newKnight);
                    }
                }
                if(TryMove<Pawn>(board, forward, out var newBoard)
                {
                    boards.Add(newBoard);
                }
            }
            // can capture diagonally one space left or right
            var forwardLeft = Location + Front + new Vector(1, 0);
            if(IsOnBoard(forwardLeft)
                && board[forwardLeft.X, forwardLeft.Y] != null
                && board[forwardLeft.X, forwardLeft.Y].Color != Color)
            {
                if(TryMove<Pawn>(board, forwardLeft, out var newBoard))
                {
                    boards.Add(newBoard);
                }
            }
            // "Promotion", If you get to the end, turn into any peice, except kings or pawns
            // king can't be in check after move
            return boards.ToArray();
        }
    }
}
