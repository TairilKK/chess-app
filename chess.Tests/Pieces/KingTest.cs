using Chess;
using Chess.Pieces;
using FluentAssertions;

namespace chess.Pieces.Tests;

public class KingTest
{
  [Theory]
  [InlineData(0, 4)]
  [InlineData(7, 4)]
  public void GetPseudoLegalMoves_ForStartPosition_ShouldNotHaveAnyMoves(int row, int column)
  {
    // Arrange
    var board = new Chessboard();
    var square = board.GetSquare(row, column)!;
    var king = square.Piece! as King;

    // Act
    var moves = king!.GetPseudoLegalMoves(square, board);

    // Assert
    moves.Should().NotBeNull();
    moves.Should().BeEmpty();
  }
}
