using Chess.Pieces;

namespace Chess;

public class Chessboard
{
    public List<Piece> Pieces { get; set; }
    public Square[] Squares { get; set; }
    public Chessboard()
    {
        Pieces = new List<Piece>();
        Squares = new Square[64];
        for (int r = 0; r < 8; r++)
        {
            for (int c = 0; c < 8; c++)
            {
                if (r == 0 || r == 7)
                {
                    switch (c)
                    {
                        case 0:
                        case 7:
                            Squares[r * 8 + c] = new Square(r, c, new Rook(r == 0));
                            Pieces.Add(Squares[r * 8 + c].Piece!);
                            break;
                        case 1:
                        case 6:
                            Squares[r * 8 + c] = new Square(r, c, new Knight(r == 0));
                            Pieces.Add(Squares[r * 8 + c].Piece!);
                            break;
                        case 2:
                        case 5:
                            Squares[r * 8 + c] = new Square(r, c, new Bishop(r == 0));
                            Pieces.Add(Squares[r * 8 + c].Piece!);
                            break;
                        case 3:
                            Squares[r * 8 + c] = new Square(r, c, new Queen(r == 0));
                            Pieces.Add(Squares[r * 8 + c].Piece!);
                            break;
                        default:
                            Squares[r * 8 + c] = new Square(r, c, new King(r == 0));
                            Pieces.Add(Squares[r * 8 + c].Piece!);
                            break;
                    }
                }
                else if (r == 1)
                {
                    Squares[r * 8 + c] = new Square(r, c, new Pawn(true));
                    Pieces.Add(Squares[r * 8 + c].Piece!);
                }
                else if (r == 6)
                {
                    Squares[r * 8 + c] = new Square(r, c, new Pawn(false));
                    Pieces.Add(Squares[r * 8 + c].Piece!);
                }
                else
                {
                    Squares[r * 8 + c] = new Square(r, c);
                }
            }
        }
    }
    private void DisplayLine()
    {
        Console.Write("  +");
        for (int i = 0; i < 8; i++)
        {
            Console.Write("---+");
        }
        Console.WriteLine();
    }
    private void DisplayColumnNames()
    {
        Console.Write("   ");
        for (int i = 0; i < 8; i++)
        {
            Console.Write($" {(char)('a' + i)}  ");
        }
        Console.WriteLine();
    }
    private void DisplayRow(int r)
    {
        Console.Write($"{r + 1} |");
        for (int c = 0; c < 8; c++)
        {
            Console.Write($" {Squares[r * 8 + c]} |");
        }
        Console.WriteLine();
    }
    public void Display()
    {
        for (int r = 0; r < 8; r++)
        {
            DisplayLine();
            DisplayRow(r);
        }

        DisplayLine();
        DisplayColumnNames();
    }
}
