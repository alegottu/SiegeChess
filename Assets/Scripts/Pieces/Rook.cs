using UnityEngine;

public class Rook : Piece
{
    protected override void CreatePath()
    {
        base.CreatePath();

        foreach (Vector2Int movement in movements)
        {
            AddPathDirection(movement, currentCellPos);
        }
    }
}
