using System;
using Chess;
using Chess.Pieces;
using FluentAssertions;

namespace chess.Pieces.Tests;

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

public class KnightTest
{
  [Theory]
  [MemberData(nameof(KnightTestDataGenerator.GetPseudoLegalMovesData), MemberType = typeof(KnightTestDataGenerator))]
  public void GetPseudoLegalMoves_ForStartPosition_ShouldNotHaveAnyMoves(int row, int column, List<Square> expectedMoves)
  {
    // Arrange
    var board = new Chessboard();
    var square = board.GetSquare(row, column)!;
    var knight = square.Piece! as Knight;

    // Act
    var moves = knight!.GetPseudoLegalMoves(square, board);

    // Assert
    moves.Should().NotBeNull();
    moves.Count.Should().Be(expectedMoves.Count);
    foreach (var expectedMove in expectedMoves)
    {
      moves.Should().Contain(m => m.Row == expectedMove.Row && m.Column == expectedMove.Column);
    }
  }

}
