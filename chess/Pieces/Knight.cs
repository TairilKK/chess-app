
namespace Chess.Pieces;

public class Knight : Piece
{
  public Knight(bool white) : base(white)
  {
  }

  public override List<Square> GetPseudoLegalMoves(Square currentSquare, Chessboard board)
  {
    int r = currentSquare.Row;
    int c = currentSquare.Column;
    List<Square> moves = new List<Square>();
    var knightMoves = new (int, int)[]
    {
      (2, 1), (2, -1), (-2, 1), (-2, -1),
      (1, 2), (1, -2), (-1, 2), (-1, -2)
    };
    foreach (var (dr, dc) in knightMoves)
    {
      Square? targetSquare = board.GetSquare(r + dr, c + dc);
      if (targetSquare is not null)
      {
        if (targetSquare.Piece is null || targetSquare.Piece.White != White)
        {
          moves.Add(targetSquare);
        }
      }
    }

    return moves;
  }

  public override string ToString()
  {
    return White ? "N" : "n";
  }
}
