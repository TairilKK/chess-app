using Chess.Pieces;

namespace Chess;

public sealed class Square
{
  public int Index { get; private set; }
  public int Row { get; private set; }
  public int Column { get; private set; }
  public bool White { get; private set; }
  public string Name { get; private set; }
  public Piece? Piece { get; set; }

  public Square(int row, int column, Piece? piece = null)
  {
    if (row < 0 || row > 7 || column < 0 || column > 7)
    {
      throw new ArgumentOutOfRangeException("Row and Column must be between 0 and 7.");
    }

    Row = row;
    Column = column;
    White = (row + column) % 2 != 0;
    Name = $"{(char)('a' + Column)}{Row + 1}";
    Piece = piece;
  }

  public override string ToString()
  {
    return Piece?.ToString() ?? " ";
  }
}
