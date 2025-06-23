using System.Text.RegularExpressions;
using Chess.Pieces;

namespace Chess;

public class Chessboard
{
  public List<Piece> Pieces { get; set; }
  public Square[] Squares { get; set; }
  public Chessboard()
  {
    Pieces = new List<Piece>();
    Squares = new Square[64];
    for (int r = 0; r < 8; r++)
    {
      for (int c = 0; c < 8; c++)
      {
        if (r == 0 || r == 7)
        {
          switch (c)
          {
            case 0:
            case 7:
              Squares[r * 8 + c] = new Square(r, c, new Rook(r == 0));
              Pieces.Add(Squares[r * 8 + c].Piece!);
              break;
            case 1:
            case 6:
              Squares[r * 8 + c] = new Square(r, c, new Knight(r == 0));
              Pieces.Add(Squares[r * 8 + c].Piece!);
              break;
            case 2:
            case 5:
              Squares[r * 8 + c] = new Square(r, c, new Bishop(r == 0));
              Pieces.Add(Squares[r * 8 + c].Piece!);
              break;
            case 3:
              Squares[r * 8 + c] = new Square(r, c, new Queen(r == 0));
              Pieces.Add(Squares[r * 8 + c].Piece!);
              break;
            default:
              Squares[r * 8 + c] = new Square(r, c, new King(r == 0));
              Pieces.Add(Squares[r * 8 + c].Piece!);
              break;
          }
        }
        else if (r == 1)
        {
          Squares[r * 8 + c] = new Square(r, c, new Pawn(true));
          Pieces.Add(Squares[r * 8 + c].Piece!);
        }
        else if (r == 6)
        {
          Squares[r * 8 + c] = new Square(r, c, new Pawn(false));
          Pieces.Add(Squares[r * 8 + c].Piece!);
        }
        else
        {
          Squares[r * 8 + c] = new Square(r, c);
        }
      }
    }
  }

  // * Implement the constructor that accepts a FEN string
  public Chessboard(string fen)
  {
    throw new NotImplementedException("Constructor with FEN string is not implemented yet.");
  }
  // ! Special moves
  // TODO: Implement en passant
  // TODO: Implement promotion
  private void Move(Square from, Square to)
  {
    if (!Squares.Contains(from) || !Squares.Contains(to))
    {
      throw new ArgumentException("Both squares must be part of the chessboard.");
    }
    var piece = from.Piece ?? throw new ArgumentException("No piece to move from the specified square.");
    var moves = piece.GetPseudoLegalMoves(from, this);
    if (!moves.Contains(to))
    {
      throw new ArgumentException("Invalid move for the piece.");
    }

    // Castling
    if (piece is King king && Math.Abs(to.Column - from.Column) == 2)
    {
      // Kingside
      if (to.Column > from.Column)
      {
        var rookFrom = GetSquare(from.Row, 7)!;
        var rookTo = GetSquare(from.Row, 5)!;
        rookTo.Piece = rookFrom.Piece;
        rookFrom.Piece = null;
        if (rookTo.Piece is Rook rook) rook.HasMoved = true;
      }
      // Queenside
      else if (to.Column < from.Column)
      {
        var rookFrom = GetSquare(from.Row, 0)!;
        var rookTo = GetSquare(from.Row, 3)!;
        rookTo.Piece = rookFrom.Piece;
        rookFrom.Piece = null;
        if (rookTo.Piece is Rook rook) rook.HasMoved = true;
      }
      king.HasMoved = true;
    }

    // Capture
    if (to.Piece is not null)
    {
      Pieces.Remove(to.Piece);
    }
    to.Piece = piece;
    from.Piece = null;
  }
  private void Move((int row, int col) from, (int row, int col) to)
  {
    if (from.row < 0 || from.row > 7 || from.col < 0 || from.col > 7 ||
       to.row < 0 || to.row > 7 || to.col < 0 || to.col > 7)
    {
      throw new ArgumentException("Position is out of bounds. Use row and column between 0 and 7.");
    }

    var fromSquare = GetSquare(from.row, from.col)!;
    var toSquare = GetSquare(to.row, to.col)!;

    Move(fromSquare, toSquare);
  }
  public void Move(string from, string to)
  {
    Regex regex = new Regex(@"^[a-h][1-8]$");
    if (!regex.IsMatch(from) || !regex.IsMatch(to))
    {
      throw new ArgumentException("Invalid position format. Use 'a1' to 'h8'.");
    }

    var fromCoordinates = (from[1] - '1', from[0] - 'a');
    var toCoordinates = (to[1] - '1', to[0] - 'a');

    Move(fromCoordinates, toCoordinates);
  }
  public void RemovePiece(Square square)
  {
    if (!Squares.Contains(square))
    {
      throw new ArgumentException("Square is not part of the chessboard.");
    }
    var piece = square.Piece;
    square.Piece = null;
    Pieces.Remove(piece!);
  }
  public void RemovePiece((int row, int col) position)
  {
    var square = GetSquare(position.row, position.col);
    if (square is null)
    {
      throw new ArgumentException("Position is out of bounds. Use row and column between 0 and 7.");
    }
    RemovePiece(square!);
  }
  public void RemovePiece(string position)
  {
    Regex regex = new Regex(@"^[a-h][1-8]$");
    if (!regex.IsMatch(position))
    {
      throw new ArgumentException("Invalid position format. Use 'a1' to 'h8'.");
    }

    int row = position[1] - '1';
    int column = position[0] - 'a';
    RemovePiece((row, column));
  }
  public void PlacePiece(Square square, Piece piece)
  {
    if (!Squares.Contains(square))
    {
      throw new ArgumentException("Square is not part of the chessboard.");
    }
    if (Pieces.Contains(piece))
    {
      throw new ArgumentException("Piece is already on the board.");
    }
    square.Piece = piece;
  }
  public void PlacePiece((int row, int col) position, Piece piece)
  {
    var square = GetSquare(position.row, position.col);
    if (square is null)
    {
      throw new ArgumentException("Position is out of bounds. Use row and column between 0 and 7.");
    }
    PlacePiece(square!, piece);
  }
  public void PlacePiece(string position, Piece piece)
  {
    Regex regex = new Regex(@"^[a-h][1-8]$");
    if (!regex.IsMatch(position))
    {
      throw new ArgumentException("Invalid position format. Use 'a1' to 'h8'.");
    }

    int row = position[1] - '1';
    int column = position[0] - 'a';
    PlacePiece((row, column), piece);
  }
  public Square? GetSquare(string position)
  {
    Regex regex = new Regex(@"^[a-h][1-8]$");
    if (!regex.IsMatch(position))
    {
      throw new ArgumentException("Invalid position format. Use 'a1' to 'h8'.");
    }

    int row = position[1] - '1';
    int column = position[0] - 'a';

    return GetSquare(row, column);
  }
  public Square? GetSquare(int row, int column)
  {
    if (row < 0 || row > 7 || column < 0 || column > 7)
    {
      return null;
    }
    return Squares[row * 8 + column];
  }
  private string LineToString()
  {
    var line = "  +";
    for (int i = 0; i < 8; i++)
    {
      line += "---+";
    }
    return line;
  }
  private string ColumnNamesToString(bool fromWhite = true)
  {
    var columnNames = "   ";
    var start = 0;
    var end = 8;
    var diff = 1;

    if (!fromWhite)
    {
      start = 7;
      end = -1;
      diff = -1;
    }

    for (int i = start; i != end; i += diff)
    {
      columnNames += $" {(char)('a' + i)}  ";
    }

    return columnNames;
  }
  private string RowToString(int row, bool fromWhite = true)
  {
    var rowString = $"{row + 1} |";
    var start = 0;
    var end = 8;
    var diff = 1;

    if (!fromWhite)
    {
      start = 7;
      end = -1;
      diff = -1;
    }

    var col = start;

    for (; col != end;)
    {
      rowString += $" {Squares[row * 8 + col]} |";
      col += diff;
    }

    return rowString;
  }
  private string Display(bool fromWhite = true)
  {
    var boardString = "";

    for (int r = 0; r < 8; r++)
    {
      var row = fromWhite ? 7 - r : r;
      boardString += LineToString();
      boardString += "\n";

      boardString += RowToString(row, fromWhite);
      boardString += "\n";
    }

    boardString += LineToString();
    boardString += "\n";
    boardString += ColumnNamesToString(fromWhite);
    boardString += "\n";

    return boardString;
  }
  public string BlackSideToString()
  {
    return Display(false);
  }
  public string WhiteSideToString()
  {
    return Display(true);
  }
  private string Padding()
  {
    return "   ";
  }
  public override string ToString()
  {
    var bothBoardString = "";

    for (int r = 0; r < 8; r++)
    {
      bothBoardString += LineToString();
      bothBoardString += Padding();
      bothBoardString += LineToString();
      bothBoardString += '\n';

      bothBoardString += RowToString(7 - r);
      bothBoardString += Padding();
      bothBoardString += RowToString(r, false);
      bothBoardString += '\n';
    }

    bothBoardString += LineToString();
    bothBoardString += Padding();
    bothBoardString += LineToString();
    bothBoardString += '\n';
    bothBoardString += ColumnNamesToString();
    bothBoardString += Padding();
    bothBoardString += ColumnNamesToString(false);
    bothBoardString += '\n';

    return bothBoardString;
  }
}
