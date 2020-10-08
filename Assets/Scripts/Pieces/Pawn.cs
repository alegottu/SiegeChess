using UnityEngine;

public class Pawn : Piece
{
    private bool firstTurn = true;

    protected override void CreatePath()
    {
        base.CreatePath();

        Vector2Int nextPos = isWhite ? currentCellPos + movements[0] : currentCellPos - movements[0];
        Cell nextCell = CellManager.cells[nextPos];
        if (!nextCell.currentPiece)
        {
            possibleCells.Add(nextCell);
        }

        if (firstTurn)
        {
            possibleCells.Add(isWhite ? CellManager.cells[currentCellPos + movements[0] * 2] : CellManager.cells[currentCellPos - movements[0] * 2]);
        }

        Cell rightCell = CellManager.cells[nextPos + Vector2Int.right];
        if (rightCell.currentPiece && isWhite != rightCell.currentPiece.isWhite)
        {
            possibleCells.Add(rightCell);
        }

        Cell leftCell = CellManager.cells[nextPos + Vector2Int.left];
        if (leftCell.currentPiece && isWhite != rightCell.currentPiece.isWhite)
        {
            possibleCells.Add(leftCell);
        }
    }

    protected override void RemovePath()
    {
        base.RemovePath();

        firstTurn = false;
    }
}
