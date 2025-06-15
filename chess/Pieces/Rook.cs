namespace Chess.Pieces;

public class Rook : Piece
{
    public Rook(bool white) : base(white)
    {
    }

    public override string ToString()
    {
        return White ? "R" : "r";
    }
}
