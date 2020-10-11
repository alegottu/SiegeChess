using UnityEngine;

public class Bishop : Piece
{
    protected override void CreatePath()
    {
        foreach (Vector2Int movement in movements)
        {
            AddPathDirection(movement, currentCellPos);
        }
    }
}
