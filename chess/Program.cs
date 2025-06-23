using System.Text.RegularExpressions;
using Chess;

#pragma warning disable CS8321 // remove declared but never used
void DisplayGame()
{
  var chessboard = new Chessboard();

  string pgn = "1. e2-e4 c7-c5 2. Ng1-f3 d7-d6 3. d2-d4 c5xd4 4. Nf3xd4 Ng8-f6 5. Nb1-c3 a7-a6";

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
}
#pragma warning restore CS8321


var board = new Chessboard();

Console.WriteLine(board);

Console.WriteLine("Enter moves in the format 'e2-e4' or 'e2e4'. Type 'e', 'exit', 'q','quit' to quit.");
while (true)
{
  var input = Console.ReadLine();
  if (input == null || input.Trim().ToLower() is "e" or "exit" or "q" or "quit")
  {
    break;
  }
  if (Regex.IsMatch(input, @"^[a-h][1-8]-[a-h][1-8]$"))
  {
    try
    {
      var parts = input.Split('-');
      var from = parts[0];
      var to = parts[1];
      board.Move(from, to);
      Console.WriteLine($"Moved from {from} to {to}");
      Console.WriteLine(board.WhiteSideToString());

    }
    catch (Exception ex)
    {
      Console.WriteLine($"Error: {ex.Message}");
    }
  }
  else if (Regex.IsMatch(input, @"^[a-h][1-8][a-h][1-8]$"))
  {
    try
    {
      var from = input[0..2];
      var to = input[2..4];
      board.Move(from, to);
      Console.WriteLine($"Moved from {from} to {to}");
      Console.WriteLine(board.WhiteSideToString());

    }
    catch (Exception ex)
    {
      Console.WriteLine($"Error: {ex.Message}");
    }
  }
  else
  {
    Console.WriteLine("Invalid move format. Use 'e2-e4' or 'e2e4' format.");
  }
}

Console.WriteLine("Exiting the game.");
