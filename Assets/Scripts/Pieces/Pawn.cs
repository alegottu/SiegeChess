using UnityEngine;

public class Pawn : Piece
{
    private bool firstTurn = true;

    protected override void CreatePath()
    {
        Vector2Int nextPos = isWhite ? currentCellPos + movements[0] : currentCellPos - movements[0];
        Cell nextCell = CellManager.cells[nextPos];
        if (!nextCell.currentPiece)
        {
            possibleCells.Add(nextCell);
        }

        Cell posssibleFirst = isWhite ? CellManager.cells[currentCellPos + movements[0] * 2] : CellManager.cells[currentCellPos - movements[0] * 2];
        if (firstTurn && !posssibleFirst.currentPiece)
        {
            possibleCells.Add(posssibleFirst);
        }

        Cell rightCell = currentCellPos.x != 7 ? CellManager.cells[nextPos + Vector2Int.right] : null;
        if (rightCell && rightCell.currentPiece && isWhite != rightCell.currentPiece.isWhite)
        {
            possibleCells.Add(rightCell);
        }

        Cell leftCell = currentCellPos.x != 0 ? CellManager.cells[nextPos + Vector2Int.left] : null;
        if (leftCell && leftCell.currentPiece && isWhite != leftCell.currentPiece.isWhite)
        {
            possibleCells.Add(leftCell);
        }

        base.CreatePath();
    }

    protected override void Move(Cell cell)
    {
        base.Move(cell);

        firstTurn = false;
        if (isWhite ? currentCellPos.y == 7 : currentCellPos.x == 0)
        {
            // promote
        }
    }
}
