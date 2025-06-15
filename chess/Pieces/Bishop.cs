namespace Chess.Pieces;

public class Bishop : Piece
{
    public Bishop(bool white) : base(white)
    {
    }

    public override string ToString()
    {
        return White ? "B" : "b";
    }
}
