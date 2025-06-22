using Chess;
using Chess.Pieces;
using FluentAssertions;

namespace chess.Tests;

public class ChessboardTest
{
  [Fact]
  public void Chessboard_ForCorrectPieceCount()
  {
    // Arrange
    var board = new Chessboard();

    // Act
    var piecesCount = board.Pieces.Count;

    // Assert
    piecesCount.Should().Be(32);
  }

  [Fact]
  public void Chessboard_ForCorrectSquareCount()
  {
    // Arrange
    var board = new Chessboard();

    // Act
    var squaresCount = board.Squares.Length;

    // Assert
    squaresCount.Should().Be(64);
  }

  [Fact]
  public void Chessboard_ForCorrectSquareNames()
  {
    // Arrange
    var board = new Chessboard();

    // Act & Assert
    for (int row = 0; row < 8; row++)
    {
      for (int col = 0; col < 8; col++)
      {
        var square = board.GetSquare(row, col);
        square.Should().NotBeNull();
        square.Name.Should().Be($"{(char)('a' + col)}{row + 1}");
      }
    }
  }

  [Theory]
  [InlineData(0, 0, true, "R")]
  [InlineData(0, 7, true, "R")]
  [InlineData(7, 0, false, "R")]
  [InlineData(7, 7, false, "R")]
  [InlineData(0, 1, true, "N")]
  [InlineData(0, 6, true, "N")]
  [InlineData(7, 1, false, "N")]
  [InlineData(7, 6, false, "N")]
  [InlineData(0, 2, true, "B")]
  [InlineData(0, 5, true, "B")]
  [InlineData(7, 2, false, "B")]
  [InlineData(7, 5, false, "B")]
  [InlineData(7, 3, false, "Q")]
  [InlineData(0, 3, true, "Q")]
  [InlineData(7, 4, false, "K")]
  [InlineData(0, 4, true, "K")]
  [InlineData(1, 0, true, "P")]
  [InlineData(1, 1, true, "P")]
  [InlineData(1, 2, true, "P")]
  [InlineData(1, 3, true, "P")]
  [InlineData(1, 4, true, "P")]
  [InlineData(1, 5, true, "P")]
  [InlineData(1, 6, true, "P")]
  [InlineData(1, 7, true, "P")]
  [InlineData(6, 0, false, "P")]
  [InlineData(6, 1, false, "P")]
  [InlineData(6, 2, false, "P")]
  [InlineData(6, 3, false, "P")]
  [InlineData(6, 4, false, "P")]
  [InlineData(6, 5, false, "P")]
  [InlineData(6, 6, false, "P")]
  [InlineData(6, 7, false, "P")]
  public void Chessboard_ForCorrectPiecePlacement(int row, int col, bool white, string expectedPiece)
  {
    // Arrange
    var piece = expectedPiece switch
    {
      "R" => typeof(Rook),
      "N" => typeof(Knight),
      "B" => typeof(Bishop),
      "Q" => typeof(Queen),
      "K" => typeof(King),
      "P" => typeof(Pawn),
      _ => null
    };

    // Act
    var board = new Chessboard();

    // Assert
    var square = board.GetSquare(row, col);
    square.Should().NotBeNull();

    square.Piece.Should().NotBeNull();
    square.Piece.GetType().Should().Be(piece);
    square.Piece.ToString().Should().Be(white ? expectedPiece : expectedPiece.ToLower());
    square.Piece.White.Should().Be(white);
  }
}
