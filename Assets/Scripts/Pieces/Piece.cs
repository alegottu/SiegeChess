using UnityEngine;
using System.Collections.Generic;
using System;

public abstract class Piece : MonoBehaviour
{
    public static event Action<Piece> onAnyPieceMove;

    [HideInInspector] public Cell currentCell = null;
    [HideInInspector] public List<Cell> possibleCells = new List<Cell>();
    [HideInInspector] public Piece pinner = null;
    public SpriteRenderer spriteRender = null;
    [HideInInspector] public bool isWhite = false;

    [SerializeField] protected Vector2Int[] movements = null;

    protected Vector2Int currentCellPos = Vector2Int.zero;
    protected Piece pinnedPiece = null;

    protected virtual void Start()
    {
        currentCell.currentPiece = this;
        currentCellPos = CellManager.cellPositions[currentCell];
    }

	// When this piece is clicked
    public void Select()
    {
		CreatePath();
		DrawPath();
    }

	// When this piece is selected, but another cell was clicked
	public bool Deselect(Cell cell)
	{
		bool result = false;

		if (possibleCells.Contains(cell))
		{
			Move(cell);
			result = true;
		}
		
		// TODO: doing this here messes with path manager?
		RemovePath();
		return result;
	}

    protected virtual void CreatePath()
    {
        if (pinner)
        {
            foreach (Cell cell in possibleCells)
            {
                if (!pinner.possibleCells.Contains(cell))
                {
                    possibleCells.Remove(cell);
                }
            }
        }
    }

    protected void StorePath()
    {
        if (isWhite)
        {
            PathManager.whitePaths.Add(possibleCells);
        }
        else
        {
            PathManager.blackPaths.Add(possibleCells);
        }
    }

	private void DrawPath()
	{
		foreach (Cell cell in possibleCells)
		{
			cell.MarkPossible();
		}
	}

    protected void RemovePath()
    {
		foreach (Cell cell in possibleCells)
		{
			cell.Deselect(); // same as "unmarking" the cell
		}

        possibleCells = new List<Cell>();
    }

    protected void RemovePathFromManager()
    {
        if (isWhite)
        {
            PathManager.whitePaths.Remove(possibleCells);
        }
        else
        {
            PathManager.blackPaths.Remove(possibleCells);
        }
    }

    protected void AddPathDirection(Vector2Int movement, Vector2Int currentPos)
    {
        Vector2Int nextPos = currentPos + movement;
    
        while (InBounds(nextPos))
        {
            Cell nextCell = CellManager.cells[nextPos];

            if (!nextCell.currentPiece)
            {
                possibleCells.Add(nextCell);
            }
            else
            {
                if (isWhite != nextCell.currentPiece.isWhite)
                {
                    possibleCells.Add(nextCell);
                }

                return;
            }

            nextPos += movement;
        }
    }

    // checks for pins or reveal checks
    protected void CheckExtendedPathDirection(Vector2Int movement, Piece continueFrom)
    {
        Vector2Int nextPos = continueFrom.currentCellPos + movement;

        while (InBounds(nextPos))
        {
            Cell nextCell = CellManager.cells[nextPos];

            if (nextCell.currentPiece.TryGetComponent(out King king))
            {
                if (continueFrom.isWhite != isWhite)
                {
                    pinnedPiece = continueFrom;
                    continueFrom.pinner = this;
                }
                else
                {
                    king.revealer = continueFrom;
                }
            }
        }
    }

    protected virtual void Move(Cell cell)
    {
        if (cell.currentPiece)
        {
            Destroy(cell.currentPiece.gameObject);
        }

        if (pinnedPiece)
        {
            pinnedPiece = null;
        }

        currentCell.currentPiece = null;
        currentCell = cell;
        currentCellPos = CellManager.cellPositions[currentCell];
        cell.currentPiece = this;
        transform.position = cell.transform.position;

        RemovePathFromManager(); // to ensure old paths aren't stored
        CreatePath();
        StorePath(); // to see if the king is in check

        onAnyPieceMove?.Invoke(this);
    }

    protected bool InBounds(Vector2Int pos)
    {
        return pos.x >= 0 && pos.x < 8 && pos.y >= 0 && pos.y < 8;
    }
}
