using Chess.Pieces;

namespace chess.Tests.DataGenerators;

public class ChessboardTestDataGenerator
{
  public static IEnumerable<object[]> PlacePieceValidDataGenerator()
  {
    yield return new object[] { 2, 0, new Pawn(false) }; // Black Pawn
    yield return new object[] { 2, 0, new Knight(false) }; // Black Pawn
    yield return new object[] { 2, 0, new Bishop(false) }; // Black Pawn
    yield return new object[] { 2, 0, new Rook(true) }; // White Pawn
    yield return new object[] { 2, 0, new Queen(true) }; // White Queen
    yield return new object[] { 2, 0, new King(true) }; // White King

    yield return new object[] { 3, 0, new Pawn(false) }; // Black Pawn
    yield return new object[] { 4, 0, new Knight(false) }; // Black Pawn
    yield return new object[] { 5, 0, new Bishop(false) }; // Black Pawn
    yield return new object[] { 6, 0, new Rook(true) }; // White Pawn
    yield return new object[] { 3, 2, new Queen(true) }; // White Queen
    yield return new object[] { 4, 6, new King(true) }; // White King
  }
  public static IEnumerable<object[]> PlacePieceNonValidDataGenerator()
  {
    yield return new object[] { 8, 0, new Pawn(false) }; // Black Pawn
    yield return new object[] { -2, 0, new Knight(false) }; // Black Pawn
    yield return new object[] { 2, 8, new Bishop(false) }; // Black Pawn
    yield return new object[] { 2, -2, new Rook(true) }; // White Pawn
    yield return new object[] { 8, 8, new Queen(true) }; // White Queen
    yield return new object[] { -2, -2, new King(true) }; // White King
  }
}
