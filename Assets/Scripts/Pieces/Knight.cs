using UnityEngine;

public class Knight : Piece
{
    protected override void CreatePath()
    {
        base.CreatePath();

        foreach (Vector2Int movement in movements)
        {
            Vector2Int nextPos = currentCellPos + movement;

            if (nextPos.x * nextPos.y < 64 && nextPos.x * nextPos.y >= 0)
            {
                possibleCells.Add(CellManager.cells[nextPos]);
            }
        }
    }
}
