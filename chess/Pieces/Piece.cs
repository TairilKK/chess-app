namespace Chess.Pieces;

public abstract class Piece
{
  public bool White;
  public Piece(bool white)
  {
    White = white;
  }
  public abstract List<Square> GetPseudoLegalMoves(Square currentSquare, Chessboard board);
}
