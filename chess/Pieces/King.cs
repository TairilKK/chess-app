
namespace Chess.Pieces;

public class King : Piece
{
  public bool HasMoved { get; private set; } = false;

  public King(bool white) : base(white)
  {
  }

  //TODO: Queen side and King side castling
  private bool CanCastleKingside(Chessboard board, Square currentSquare)
  {
    if (HasMoved) return false;

    // Check if the squares between the king and rook are empty
    int row = currentSquare.Row;
    int col = currentSquare.Column;

    // Check for King-side castling
    Square? kingSideRook = board.GetSquare(row, 7); // H file
    if (kingSideRook?.Piece is Rook rook && !rook.HasMoved)
    {
      for (int c = col + 1; c < 7; c++)
      {
        if (board.GetSquare(row, c)?.Piece != null) return false;
      }
      return true;
    }
    return false;
  }
  private bool CanCastleQueenside(Chessboard board, Square currentSquare)
  {
    if (HasMoved) return false;

    // Check if the squares between the king and rook are empty
    int row = currentSquare.Row;
    int col = currentSquare.Column;

    // Check for Queen-side castling
    Square? queenSideRook = board.GetSquare(row, 0); // A file
    if (queenSideRook?.Piece is Rook rook && !rook.HasMoved)
    {
      for (int c = col - 1; c > 0; c--)
      {
        if (board.GetSquare(row, c)?.Piece != null) return false;
      }
      return true;
    }
    return false;
  }
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
