using UnityEngine;

public class Knight : Piece
{
    protected override void CreatePath()
    {
        foreach (Vector2Int movement in movements)
        {
            Vector2Int nextPos = currentCellPos + movement;

            if (InBounds(nextPos))
            {
                Cell nextCell = CellManager.cells[nextPos];
                if (!nextCell.currentPiece)
                {
                    possibleCells.Add(nextCell);
                }
                else if (isWhite != nextCell.currentPiece.isWhite)
                {
                    possibleCells.Add(nextCell);
                }
            }
        }
    }
}
