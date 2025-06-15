using Chess;

var chessboard = new Chessboard();

Console.WriteLine("Chessboard from white:");
chessboard.Display();


Console.WriteLine("Chessboard from black:");
chessboard.Display(false);

Console.WriteLine("Chessboard from both sides:");
chessboard.DisplayBothSides();
