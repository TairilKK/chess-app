
namespace Chess.Pieces;

public class Rook : Piece
{
  public bool HasMoved { get; set; } = false;
  public Rook(bool white) : base(white)
  {
  }

  public override List<Square> GetPseudoLegalMoves(Square currentSquare, Chessboard board)
  {
    List<Square> moves = new List<Square>();
    int r = currentSquare.Row;
    int c = currentSquare.Column;

    var directions = new (int, int)[] {
      (1, 0),
      (-1, 0),
      (0, 1),
      (0, -1)
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
    return White ? "R" : "r";
  }
}
