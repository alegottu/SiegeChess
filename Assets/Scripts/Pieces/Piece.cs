using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public abstract class Piece : MonoBehaviour
{
    [HideInInspector] public Cell currentCell = null;
    public SpriteRenderer spriteRender = null;
    [HideInInspector] public bool isWhite = false;

    [SerializeField] protected Vector2Int[] movements = null;

    protected bool allowMove = false;
    protected Vector2Int currentCellPos = Vector2Int.zero;
    protected List<Cell> possibleCells = new List<Cell>();

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

            if (possibleCells.Count < 1)
            {
                CreatePath();
            }
        }
        else if (allowMove && clickedObject.TryGetComponent(out Cell cell) && possibleCells.Contains(cell))
        {
            Move(cell);
        }
    }

    protected virtual void CreatePath()
    {
        // change possible sprites to have an outline
    }

    protected void StorePath()
    {
        if (isWhite)
        {
            PathManager.whitePaths.AddRange(possibleCells);
            PathManager.whitePaths = PathManager.whitePaths.Distinct().ToList();
        }
        else
        {
            PathManager.blackPaths.AddRange(possibleCells);
            PathManager.blackPaths = PathManager.blackPaths.Distinct().ToList();
        }
    }

    protected virtual void RemovePath()
    {
        possibleCells = new List<Cell>();

        // change possible sprites back to default
    }

    protected void AddPathDirection(Vector2Int movement, Vector2Int currentPos)
    {
        Vector2Int nextPos = currentPos + movement;

        while (nextPos.x * nextPos.y < 64 && nextPos.x * nextPos.y >= 0)
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

        CreatePath(); // to see if the king is in check
        StorePath();
        RemovePath();
        allowMove = false;
    }

    private void OnDisable()
    {
        ClickSelecter.OnSelectedObjectChange -= SelectedObjectChangeEventHandler;
    }
}
