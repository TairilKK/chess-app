using Chess;
using Chess.Pieces;
using FluentAssertions;

namespace chess.Pieces.Tests;

public class BishopTest
{
  [Theory]
  [InlineData(0, 2)]
  [InlineData(0, 5)]
  [InlineData(7, 2)]
  [InlineData(7, 5)]
  public void GetPseudoLegalMoves_ForStartPosition_ShouldNotHaveAnyMoves(int row, int column)
  {
    // Arrange
    var board = new Chessboard();
    var square = board.GetSquare(row, column)!;
    var bishop = square.Piece! as Bishop;

    // Act
    var moves = bishop!.GetPseudoLegalMoves(square, board);

    // Assert
    moves.Should().NotBeNull();
    moves.Should().BeEmpty();
  }

}
