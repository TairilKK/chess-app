using chess.Tests.DataGenerators;
using Chess;
using Chess.Pieces;
using FluentAssertions;

namespace chess.Tests;

public class ChessboardTest
{
  // Chessboard constructor tests
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
  ////////////////////////////////

  // PlacePiece tests
  [Theory]
  [MemberData(nameof(ChessboardTestDataGenerator.PlacePieceValidDataGenerator), MemberType = typeof(ChessboardTestDataGenerator))]
  public void PlacePiece_ForValidSquare_ShouldPlacePiece(int row, int col, Piece piece)
  {
    // Arrange
    var board = new Chessboard();
    var square = board.GetSquare(row, col)!;

    // Act
    board.PlacePiece(square, piece);

    // Assert
    square.Piece.Should().NotBeNull();
    square.Piece.GetType().Should().Be(piece.GetType());
    square.Piece.Should().Be(piece);
  }
  [Fact]
  public void PlacePiece_ForNonValidSquare_ShouldThrowArgumentsException()
  {
    // Arrange
    var board = new Chessboard();
    var random = new Random();
    for (int n = 0; n < 100; n++)
    {
      var square = new Square(random.Next(0, 8), random.Next(0, 8));
      Piece piece = random.Next(0, 6) switch
      {
        0 => new Rook(random.Next(0, 2) == 0),
        1 => new Knight(random.Next(0, 2) == 0),
        2 => new Bishop(random.Next(0, 2) == 0),
        3 => new Queen(random.Next(0, 2) == 0),
        4 => new Pawn(random.Next(0, 2) == 0),
        _ => new King(random.Next(0, 2) == 0)
      };

      // Act
      Action act = () => board.PlacePiece(square, piece);

      // Assert
      act.Should().Throw<ArgumentException>()
        .WithMessage("Square is not part of the chessboard.");
    }
  }
  [Fact]
  public void PlacePiece_ForPieceAlredyOnBoard_ShouldThrowArgumentsException()
  {
    // Arrange
    var board = new Chessboard();
    var random = new Random();
    foreach (var piece in board.Pieces)
    {
      Square square = board.Squares.Where(s => s.Piece == piece).First();

      // Act
      Action act = () => board.PlacePiece(square, piece);

      // Assert
      act.Should().Throw<ArgumentException>()
        .WithMessage("Piece is already on the board.");
    }
  }

  [Theory]
  [MemberData(nameof(ChessboardTestDataGenerator.PlacePieceValidDataGenerator), MemberType = typeof(ChessboardTestDataGenerator))]
  public void PlacePiece_ForValidCoordinates_ShouldPlacePiece(int row, int col, Piece piece)
  {
    // Arrange
    var board = new Chessboard();
    var square = board.GetSquare(row, col)!;

    // Act
    board.PlacePiece((row, col), piece);

    // Assert
    square.Piece.Should().NotBeNull();
    square.Piece.GetType().Should().Be(piece.GetType());
    square.Piece.Should().Be(piece);
  }
  [Theory]
  [InlineData(-1, 0)]
  [InlineData(-17, 0)]
  [InlineData(8, 0)]
  [InlineData(98, 0)]
  [InlineData(0, -1)]
  [InlineData(0, -16)]
  [InlineData(0, 8)]
  [InlineData(0, 989)]
  [InlineData(8, 8)]
  [InlineData(-8, 18)]
  public void PlacePiece_ForNonValidCoordinates_ShouldThrowArgumentsException(int row, int col)
  {
    // Arrange
    var board = new Chessboard();

    // Act
    Action act = () => board.PlacePiece((row, col), new Pawn(true));

    // Assert
    act.Should().Throw<ArgumentException>()
      .WithMessage("Position is out of bounds. Use row and column between 0 and 7.");
  }
  [Theory]
  [MemberData(nameof(ChessboardTestDataGenerator.PlacePieceValidDataGenerator), MemberType = typeof(ChessboardTestDataGenerator))]
  public void PlacePiece_ForValidName_ShouldPlacePiece(int row, int col, Piece piece)
  {
    // Arrange
    var board = new Chessboard();
    var square = board.GetSquare(row, col)!;
    var name = square.Name;

    // Act
    board.PlacePiece(name, piece);

    // Assert
    square.Piece.Should().NotBeNull();
    square.Piece.GetType().Should().Be(piece.GetType());
    square.Piece.Should().Be(piece);
  }
  [Theory]
  [InlineData("abc")]
  [InlineData("z1")]
  [InlineData("l1")]
  [InlineData("a10")]
  [InlineData("a9")]
  [InlineData("a0")]
  public void PlacePiece_ForNonValidName_ShouldThrowArgumentsException(string name)
  {
    // Arrange
    var board = new Chessboard();

    // Act
    Action act = () => board.PlacePiece(name, new Pawn(true));

    // Assert
    act.Should().Throw<ArgumentException>()
      .WithMessage("Invalid position format. Use 'a1' to 'h8'.");
  }
  ////////////////////////////////

  // RemovePiece tests
  [Fact]
  public void RemovePiece_ForValidSquare_ShouldRemovePiece()
  {
    // Arrange
    var board = new Chessboard();

    foreach (var square in board.Squares)
    {
      if (square.Piece is not null)
      {
        // Act
        board.RemovePiece(square);

        // Assert
        board.Pieces.Should().NotContain(square.Piece);
        square.Piece.Should().BeNull();
      }
    }
    board.Pieces.Should().BeEmpty();
  }
  [Fact]
  public void RemovePiece_ForNonValidSquare_ShouldThrowException()
  {
    // Arrange
    var board = new Chessboard();

    for (int n = 0; n < 100; n++)
    {
      var square = new Square(new Random().Next(0, 8), new Random().Next(0, 8));
      Piece piece = new Pawn(new Random().Next(0, 2) == 0);

      // Act
      Action act = () => board.RemovePiece(square);

      // Assert
      act.Should().Throw<ArgumentException>()
        .WithMessage("Square is not part of the chessboard.");
    }
  }
  [Fact]
  public void RemovePiece_ForValidCoordinates_ShouldRemovePiece()
  {
    // Arrange
    var board = new Chessboard();

    foreach (var square in board.Squares)
    {
      if (square.Piece is not null)
      {
        // Act
        board.RemovePiece((square.Row, square.Column));

        // Assert
        board.Pieces.Should().NotContain(square.Piece);
        square.Piece.Should().BeNull();
      }
    }
    board.Pieces.Should().BeEmpty();
  }

  [Theory]
  [InlineData(-1, 0)]
  [InlineData(-17, 0)]
  [InlineData(8, 0)]
  [InlineData(98, 0)]
  [InlineData(0, -1)]
  [InlineData(0, -16)]
  [InlineData(0, 8)]
  [InlineData(0, 989)]
  [InlineData(8, 8)]
  [InlineData(-8, 18)]
  [InlineData(8, -8)]
  public void RemovePiece_ForNonValidCoordinates_ShouldThrowException(int row, int col)
  {
    // Arrange
    var board = new Chessboard();

    for (int n = 0; n < 100; n++)
    {
      // Act
      Action act = () => board.RemovePiece((row, col));

      // Assert
      act.Should().Throw<ArgumentException>()
        .WithMessage("Position is out of bounds. Use row and column between 0 and 7.");
    }
  }
  ////////////////////////////////

  // Move tests
  [Theory]
  [InlineData("e2", "e4")]
  [InlineData("d2", "d4")]
  [InlineData("g1", "f3")]
  [InlineData("b1", "c3")]
  public void Move_ForValidMove_ShouldMovePiece(string from, string to)
  {
    // Arrange
    var board = new Chessboard();
    var piece = board.GetSquare(from)!.Piece;

    // Act
    board.Move(from, to);

    // Assert
    var fromSquare = board.GetSquare(from);
    var toSquare = board.GetSquare(to);

    fromSquare!.Piece.Should().BeNull();
    toSquare!.Piece.Should().NotBeNull();
    toSquare.Piece.Should().Be(piece);
  }

  [Theory]
  [InlineData("e2", "e5")]
  [InlineData("b1", "b3")]
  [InlineData("a1", "a5")]
  [InlineData("e1", "e2")]
  public void Move_ForNonValidMove_ShouldThrowArgumentException(string from, string to)
  {
    // Arrange
    var board = new Chessboard();
    var piece = board.GetSquare(from)!.Piece;

    // Act
    Action act = () => board.Move(from, to);

    // Assert
    act.Should().Throw<ArgumentException>()
      .WithMessage("Invalid move for the piece.");
  }

  // Special moves tests
  // Castling tests
  [Fact]
  public void Move_ForQueenSideCastling_ShouldCastle()
  {
    // Arrange
    var board = new Chessboard();
    board.RemovePiece("d1"); // Remove white queen
    board.RemovePiece("c1"); // Remove white bishop
    board.RemovePiece("b1"); // Remove white knight

    board.RemovePiece("d8"); // Remove black queen
    board.RemovePiece("c8"); // Remove black bishop
    board.RemovePiece("b8"); // Remove black knight

    // Act
    board.Move("e1", "c1"); // White queen side castling
    board.Move("e8", "c8"); // Black queen side castling

    // Assert
    board.GetSquare("c1")!.Piece.Should().BeOfType<King>();
    board.GetSquare("d1")!.Piece.Should().BeOfType<Rook>();

    board.GetSquare("c8")!.Piece.Should().BeOfType<King>();
    board.GetSquare("d8")!.Piece.Should().BeOfType<Rook>();
  }

  [Fact]
  public void Move_ForKingSideCastling_ShouldCastle()
  {
    // Arrange
    var board = new Chessboard();
    board.RemovePiece("f1"); // Remove white bishop
    board.RemovePiece("g1"); // Remove white knight

    board.RemovePiece("f8"); // Remove black bishop
    board.RemovePiece("g8"); // Remove black knight

    // Act
    board.Move("e1", "g1"); // White king side castling
    board.Move("e8", "g8"); // Black king side castling

    // Assert
    board.GetSquare("g1")!.Piece.Should().BeOfType<King>();
    board.GetSquare("f1")!.Piece.Should().BeOfType<Rook>();

    board.GetSquare("g8")!.Piece.Should().BeOfType<King>();
    board.GetSquare("f8")!.Piece.Should().BeOfType<Rook>();
  }
}
