namespace Chess.Pieces;

public class Pawn : Piece
{
    public Pawn(bool white) : base(white)
    {
    }

    public override string ToString()
    {
        return White ? "P" : "p";
    }

}
