using UnityEngine;
using System.Collections.Generic;
using System;

public abstract class Piece : MonoBehaviour
{
    public static event Action<Piece> onAnyPieceMove;

    [HideInInspector] public Cell currentCell = null;
    [HideInInspector] public List<Cell> possibleCells = new List<Cell>();
    public SpriteRenderer spriteRender = null;
    [HideInInspector] public bool isWhite = false;

    [SerializeField] protected Vector2Int[] movements = null;

    protected bool allowMove = false;
    protected Vector2Int currentCellPos = Vector2Int.zero;

    protected virtual void OnEnable()
    {
        ClickSelecter.OnSelectedObjectChange += SelectedObjectChangeEventHandler;
    }

    protected virtual void Start()
    {
        currentCell.currentPiece = this;
        currentCellPos = CellManager.cellPositions[currentCell];
    }

    protected virtual void SelectedObjectChangeEventHandler(GameObject clickedObject)
    {
        if (clickedObject.Equals(currentCell.gameObject))
        {
            allowMove = true;
            RemovePath(); //to ensure that an outdated path isn't being used
            CreatePath();
        }
        else if (allowMove && clickedObject.TryGetComponent(out Cell cell) && possibleCells.Contains(cell))
        {
            Move(cell);
        }
    }

    protected abstract void CreatePath();

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

    protected void RemovePath()
    {
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

    // checks for pins, reveal checks, or skewers
    protected void CheckExtendedPathDirection(Vector2Int movement, Piece continueFrom)
    {
        
    }

    protected virtual void Move(Cell cell)
    {
        if (cell.currentPiece)
        {
            Destroy(cell.currentPiece.gameObject);
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
        allowMove = false;
    }

    protected bool InBounds(Vector2Int pos)
    {
        return pos.x >= 0 && pos.x < 8 && pos.y >= 0 && pos.y < 8;
    }

    private void OnDisable()
    {
        ClickSelecter.OnSelectedObjectChange -= SelectedObjectChangeEventHandler;
    }
}