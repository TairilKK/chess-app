using System.Text.RegularExpressions;
using Chess;

var chessboard = new Chessboard();

string pgn = @"
1. e2-e4 c7-c5 2. Ng1-f3 d7-d6 3. d2-d4 c5xd4 4. Nf3xd4 Ng8-f6 5. Nb1-c3 a7-a6
";

var matches = Regex.Matches(pgn, @"[a-h][1-8]-[a-h][1-8]");
var moves = matches.Select(m => m.Value).ToList();

Console.WriteLine(chessboard);
foreach (var move in moves)
{
  var parts = move.ToString().Split('-');
  var from = parts[0];
  var to = parts[1];
  chessboard.Move(from, to);
  Console.WriteLine($"Moved from {from} to {to}");
  Console.WriteLine(chessboard);
}
