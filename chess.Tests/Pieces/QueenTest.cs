using System;
using Chess;
using Chess.Pieces;
using FluentAssertions;

namespace chess.Pieces.Tests;

public class QueenTest
{
  [Theory]
  [InlineData(0, 3)]
  [InlineData(7, 3)]
  public void GetPseudoLegalMoves_ForStartPosition_ShouldNotHaveAnyMoves(int row, int column)
  {
    // Arrange
    var board = new Chessboard();
    var square = board.GetSquare(row, column)!;
    var queen = square.Piece! as Queen;

    // Act
    var moves = queen!.GetPseudoLegalMoves(square, board);

    // Assert
    moves.Should().NotBeNull();
    moves.Should().BeEmpty();
  }
}
