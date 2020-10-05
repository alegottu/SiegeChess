using UnityEngine;
using System.Collections.Generic;

public abstract class Piece : MonoBehaviour
{
    public Cell currentCell = null;

    [SerializeField] protected bool isWhite = false;
    [SerializeField] protected SpriteRenderer sprite = null;
    [SerializeField] protected Color selected = new Color();

    protected bool allowMove = false;
    protected List<Cell> possibleCells = new List<Cell>();

    protected virtual void OnEnable()
    {
        ClickSelect.OnSelectedObjectChange += SelectedObjectChangeEventHandler;
    }

    protected virtual void SelectedObjectChangeEventHandler(GameObject clickedObject)
    {
        if (clickedObject.Equals(currentCell.gameObject))
        {
            CreatePath();
        }

        if (allowMove && clickedObject.TryGetComponent(out Cell cell) && possibleCells.Contains(cell))
        {
            if (cell.currentPiece)
            {
                Destroy(cell.currentPiece.gameObject);
            }

            currentCell = cell;
            cell.currentPiece = this;
            RemovePath();
        }
    }

    public abstract void Spawn();

    protected virtual void CreatePath()
    {
        print(true);
        allowMove = true;
    }

    protected void RemovePath()
    {
        allowMove = false;
        possibleCells = new List<Cell>();
    }
}
