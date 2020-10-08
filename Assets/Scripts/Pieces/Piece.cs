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

    private void Start()
    {
        currentCell.currentPiece = this;
        currentCellPos = CellManager.cellPositions[currentCell];

        CreatePath();
        StorePath();
    }

    protected virtual void SelectedObjectChangeEventHandler(GameObject clickedObject)
    {
        if (clickedObject.Equals(currentCell.gameObject))
        {
            allowMove = true;
        }
        else if (allowMove && clickedObject.TryGetComponent(out Cell cell) && possibleCells.Contains(cell))
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

            CreatePath();
            StorePath();
            allowMove = false;
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
        // change possible sprites back to default
    }

    protected void AddPathDirection(Vector2Int movement, Vector2Int currentPos)
    {
        Vector2Int nextPos = currentPos + movement;
        bool inBoundsX = nextPos.x >= 0 && nextPos.x < 8;
        bool inBoundsY = nextPos.y >= 0 && nextPos.y < 8;

        while (inBoundsX && inBoundsY)
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

                break;
            }

            nextPos += movement;
        }
    }

    private void OnDisable()
    {
        ClickSelecter.OnSelectedObjectChange -= SelectedObjectChangeEventHandler;
    }
}
