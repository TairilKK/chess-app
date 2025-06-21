
namespace Chess.Pieces;

public class Queen : Piece
{
  public Queen(bool white) : base(white)
  {
  }

  public override List<Square> GetPseudoLegalMoves(Square currentSquare, Chessboard board)
  {
    int r = currentSquare.Row;
    int c = currentSquare.Column;
    List<Square> moves = new List<Square>();

    var directions = new (int, int)[]
    {
      (1, 0),   // Down
      (-1, 0),  // Up
      (0, 1),   // Right
      (0, -1),  // Left
      (1, 1),   // Down-Right
      (1, -1),  // Down-Left
      (-1, 1),  // Up-Right
      (-1, -1)  // Up-Left
    };

    foreach (var (dr, dc) in directions)
    {
      for (int i = 1; i < 8; i++)
      {
        Square? targetSquare = board.GetSquare(r + dr * i, c + dc * i);
        if (targetSquare is not null)
        {
          if (targetSquare.Piece is null)
          {
            moves.Add(targetSquare);
          }
          else if (targetSquare.Piece.White != White)
          {
            moves.Add(targetSquare);
            break; // Can capture but not move further
          }
          else
          {
            break; // Blocked by own piece
          }
        }
      }
    }

    return moves;
  }

  public override string ToString()
  {
    return White ? "Q" : "q";
  }
}
