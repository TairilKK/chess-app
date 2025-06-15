namespace Chess.Pieces;

public class King : Piece
{
    public King(bool white) : base(white)
    {
    }

    public override string ToString()
    {
        return White ? "K" : "k";
    }
}
