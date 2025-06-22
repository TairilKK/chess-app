using chess.DataGenerators.Tests;
using Chess;
using Chess.Pieces;
using FluentAssertions;

namespace chess.Pieces.Tests;

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
