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

  public Square? GetSquare(int row, int column)
  {
    if (row < 0 || row > 7 || column < 0 || column > 7)
    {
      return (Square?)null;
    }
    return Squares[row * 8 + column];
  }
  private void DisplayLine()
  {
    Console.Write("  +");
    for (int i = 0; i < 8; i++)
    {
      Console.Write("---+");
    }
  }
  private void DisplayColumnNames(bool fromWhite = true)
  {
    Console.Write("   ");
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
      Console.Write($" {(char)('a' + i)}  ");
    }
  }
  private void DisplayRow(int row, bool fromWhite = true)
  {
    Console.Write($"{row + 1} |");
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
      Console.Write($" {Squares[row * 8 + col]} |");
      col += diff;
    }
  }
  // TODO: Change to Tostring()
  public void Display(bool fromWhite = true)
  {

    for (int r = 0; r < 8; r++)
    {
      var row = fromWhite ? 7 - r : r;
      DisplayLine();
      Console.WriteLine();
      DisplayRow(row, fromWhite);
      Console.WriteLine();
    }

    DisplayLine();
    Console.WriteLine();
    DisplayColumnNames(fromWhite);
    Console.WriteLine();
  }
  private void DisplayPadding()
  {
    Console.Write("   ");
  }
  public void DisplayBothSides()
  {
    for (int r = 0; r < 8; r++)
    {
      DisplayLine();
      DisplayPadding();
      DisplayLine();
      Console.WriteLine();
      DisplayRow(7 - r);
      DisplayPadding();
      DisplayRow(r);
      Console.WriteLine();
    }

    DisplayLine();
    DisplayPadding();
    DisplayLine();
    Console.WriteLine();
    DisplayColumnNames();
    DisplayPadding();
    DisplayColumnNames(false);
    Console.WriteLine();
  }
}
