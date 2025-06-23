
namespace Chess.Pieces;

public class Pawn : Piece
{
  bool IsFirstMove = true;
  public Pawn(bool white) : base(white)
  {
  }

  // TODO: En passant logic
  public override List<Square> GetPseudoLegalMoves(Square currentSquare, Chessboard board)
  {
    int direction = White ? 1 : -1;

    int r = currentSquare.Row;
    int c = currentSquare.Column;
    List<Square> moves = new List<Square>();

    // Forward move
    Square? forwardSquare = board.GetSquare(r + direction, c);
    if (forwardSquare is not null && forwardSquare.Piece is null)
    {
      moves.Add(forwardSquare);
      // Double move on first move
      if (IsFirstMove)
      {
        Square? doubleForwardSquare = board.GetSquare(r + 2 * direction, c);
        if (doubleForwardSquare is not null && doubleForwardSquare.Piece is null)
        {
          moves.Add(doubleForwardSquare);
        }
      }
    }

    // Capture moves
    Square? captureLeftSquare = board.GetSquare(r + direction, c - 1);
    if (captureLeftSquare is not null && captureLeftSquare.Piece is not null && captureLeftSquare.Piece.White != White)
    {
      moves.Add(captureLeftSquare);
    }

    Square? captureRightSquare = board.GetSquare(r + direction, c + 1);
    if (captureRightSquare is not null && captureRightSquare.Piece is not null && captureRightSquare.Piece.White != White)
    {
      moves.Add(captureRightSquare);
    }

    return moves;
  }

  public override string ToString()
  {
    return White ? "P" : "p";
  }

}
