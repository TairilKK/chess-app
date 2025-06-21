using Chess;
using Chess.Pieces;
using FluentAssertions;

namespace chess.Pieces.Tests;

public class PawnTest
{
  [Fact]
  public void Pawn_ForWhite_ReturnsCorrectPawn()
  {
    // Arrange & Act
    var pawn = new Pawn(true);

    // Assert
    pawn.White.Should().BeTrue();
    pawn.ToString().Should().Be("P");
  }

  [Fact]
  public void Pawn_ForBlack_ReturnsCorrectPawn()
  {
    // Arrange & Act
    var pawn = new Pawn(false);

    // Assert
    pawn.White.Should().BeFalse();
    pawn.ToString().Should().Be("p");
  }

  [Fact]
  public void GetPseudoLegalMoves_ForStartPosition_ReturnCorrectSquares()
  {
    // Arrange & Act
    var board = new Chessboard();
    var rows = new (int row, int direction)[] { (1, 1), (6, -1) };

    // Assert
    foreach (var (row, direction) in rows)
    {
      for (int i = 0; i < 8; i++)
      {
        var square = board.GetSquare(row, i);
        var pawn = square!.Piece!;
        var moves = pawn.GetPseudoLegalMoves(square, board);

        moves.Should().NotBeEmpty();
        moves.Should().Contain(board.GetSquare(row + 1 * direction, i)!);
        moves.Should().Contain(board.GetSquare(row + 2 * direction, i)!);
      }
    }
  }
}
