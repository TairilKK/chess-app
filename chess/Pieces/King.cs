
namespace Chess.Pieces;

public class King : Piece
{
  public King(bool white) : base(white)
  {
  }

  //TODO: Queen side and King side castling
  public override List<Square> GetPseudoLegalMoves(Square currentSquare, Chessboard board)
  {
    int r = currentSquare.Row;
    int c = currentSquare.Column;
    List<Square> moves = new List<Square>();
    var directions = new (int, int)[] {
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
    return White ? "K" : "k";
  }
}
