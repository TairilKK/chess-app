using chess.DataGenerators.Tests;
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

  [Theory]
  [MemberData(nameof(PawnTestDataGenerator.GetPseudoLegalMovesData), MemberType = typeof(PawnTestDataGenerator))]
  public void GetPseudoLegalMoves_ForStartPosition_ReturnCorrectSquares(int row, int column, List<Square> expectedMoves)
  {
    // Arrange
    var board = new Chessboard();
    var square = board.GetSquare(row, column)!;
    var pawn = square.Piece! as Pawn;

    // Act
    var moves = pawn!.GetPseudoLegalMoves(square, board);

    // Assert
    moves.Should().NotBeNull();
    moves.Count.Should().Be(expectedMoves.Count);
    foreach (var expectedMove in expectedMoves)
    {
      moves.Should().Contain(m => m.Row == expectedMove.Row && m.Column == expectedMove.Column);
    }
  }

}
