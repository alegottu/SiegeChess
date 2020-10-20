using UnityEngine;

public class Rook : Piece
{
    protected override void CreatePath()
    {
        foreach (Vector2Int movement in movements)
        {
            AddPathDirection(movement, currentCellPos);
        }

        base.CreatePath();
    }
}
