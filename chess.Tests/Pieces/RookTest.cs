using System;
using Chess;
using Chess.Pieces;
using FluentAssertions;

namespace chess.Pieces.Tests;

public class RookTest
{
  [Theory]
  [InlineData(0, 0)]
  [InlineData(0, 7)]
  [InlineData(7, 0)]
  [InlineData(7, 7)]
  public void GetPseudoLegalMoves_ForStartPosition_ShouldNotHaveAnyMoves(int row, int column)
  {
    // Arrange
    var board = new Chessboard();
    var square = board.GetSquare(row, column)!;
    var bishop = square.Piece! as Rook;

    // Act
    var moves = bishop!.GetPseudoLegalMoves(square, board);

    // Assert
    moves.Should().NotBeNull();
    moves.Should().BeEmpty();
  }
}
