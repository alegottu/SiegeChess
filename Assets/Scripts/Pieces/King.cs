using UnityEngine;
using System.Collections.Generic;
using System;

public class King : Piece
{
    public static event Action<King> OnCheck;

    public bool inCheck = false;
    public Piece revealer = null;

    [SerializeField] private Color _inCheck = new Color();

    private List<List<Cell>> dangerousPaths = new List<List<Cell>>();
    private Piece dangerousPiece = null;

    protected override void OnEnable()
    {
        base.OnEnable();

        onAnyPieceMove += OnAnyPieceMoveEventHandler;
        OnCheck += OnCheckEventHandler;
    }

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

        foreach (List<Cell> path in isWhite ? PathManager.blackPaths : PathManager.whitePaths)
        {
            List<Cell> blockedCells = new List<Cell>();
            foreach (Cell cell in possibleCells)
            {
                if (path.Contains(cell))
                {
                    blockedCells.Add(cell);
                }
            }
            possibleCells.RemoveAll(x => blockedCells.Contains(x));
        }
    }

    private void OnAnyPieceMoveEventHandler(Piece piece)
    {
        if (inCheck)
        {
            if (piece.currentCell.Equals(dangerousPiece.currentCell))
            {
                dangerousPaths.Remove(dangerousPiece.possibleCells);
                dangerousPiece = null;
                ToggleCheck();
            }
            else
            {
                List<List<Cell>> clearedPaths = new List<List<Cell>>();
                foreach (List<Cell> path in dangerousPaths)
                {
                    if (path.Contains(piece.currentCell) || !path.Contains(currentCell))
                    {
                        clearedPaths.Add(path);
                        ToggleCheck();
                    }
                }
                dangerousPaths.RemoveAll(x => clearedPaths.Contains(x));
            }

            return;
        }

        foreach (List<Cell> path in isWhite ? PathManager.blackPaths : PathManager.whitePaths)
        {
            if (path.Contains(currentCell))
            {
                dangerousPaths.Add(path);
                dangerousPiece = piece;
            }
        }

        if (dangerousPaths.Count > 0)
        {
            ToggleCheck();
            OnCheck?.Invoke(this);
            // if there are two or more separate dangerous paths the king must move
        }
    }

    private void ToggleCheck()
    {
        inCheck = !inCheck;

        if (inCheck)
        {
            currentCell.spriteRender.color = _inCheck;
        }
        else
        {
            currentCell.spriteRender.color = currentCell.standard;
        }
    }

    private void OnCheckEventHandler(King king)
    { 
        if (isWhite != king.isWhite)
        {
            return;
        }
        else if (king != this)
        {
            //Game Over
        }
    }
}