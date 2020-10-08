using UnityEngine;

public class Knight : Piece
{
    protected override void CreatePath()
    {
        base.CreatePath();

        foreach (Vector2Int movement in movements)
        {
            Vector2Int nextPos = currentCellPos + movement;
            bool inBoundsX = nextPos.x >= 0 && nextPos.x < 8;
            bool inBoundsY = nextPos.y >= 0 && nextPos.y < 8;

            if (inBoundsX && inBoundsY)
            {
                possibleCells.Add(CellManager.cells[nextPos]);
            }
        }
    }
}
