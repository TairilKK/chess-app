using Chess.Pieces;

namespace Chess;

public class Square
{
    public int Index;
    public int Row;
    public int Column;
    public bool White;
    private string Name;
    public Piece? Piece;

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
