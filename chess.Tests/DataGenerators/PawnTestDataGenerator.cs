using Chess;

namespace chess.DataGenerators.Tests;

public class PawnTestDataGenerator
{
  public static IEnumerable<object[]> GetPseudoLegalMovesData()
  {
    var rows = new (int row, int direction)[] { (1, 1), (6, -1) };
    foreach (var (row, dir) in rows)
    {
      for (int col = 0; col < 8; col++)
      {
        yield return new object[]
        {
          row, col, new List<Square>
          {
            new Square(row + 1 * dir, col), // Single move forward
            new Square(row + 2 * dir, col)  // Double move forward
          }
        };
      }
    }
  }
}
