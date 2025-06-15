namespace Chess.Pieces;

public class Knight : Piece
{
    public Knight(bool white) : base(white)
    {
    }

    public override string ToString()
    {
        return White ? "N" : "n";
    }
}
