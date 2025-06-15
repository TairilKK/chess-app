namespace Chess.Pieces;

public class Queen : Piece
{
    public Queen(bool white) : base(white)
    {
    }

    public override string ToString()
    {
        return White ? "Q" : "q";
    }
}
