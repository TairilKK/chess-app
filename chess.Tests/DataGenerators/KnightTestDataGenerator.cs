using Chess;

namespace chess.DataGenerators.Tests;

public class KnightTestDataGenerator
{
  public static IEnumerable<object[]> GetPseudoLegalMovesData()
  {
    yield return new object[] { 0, 1, new List<Square>() { new Square(2, 0), new Square(2, 2) } };
    yield return new object[] { 0, 6, new List<Square>() { new Square(2, 7), new Square(2, 5) } };
    yield return new object[] { 7, 1, new List<Square>() { new Square(5, 0), new Square(5, 2) } };
    yield return new object[] { 7, 6, new List<Square>() { new Square(5, 7), new Square(5, 5) } };
  }
}
