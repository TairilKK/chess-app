using Chess;

var chessboard = new Chessboard();

Console.WriteLine("Chessboard from white:");
Console.WriteLine(chessboard.WhiteSideToString());


Console.WriteLine("Chessboard from black:");
Console.WriteLine(chessboard.BlackSideToString());

Console.WriteLine("Chessboard from both sides:");
Console.WriteLine(chessboard);
