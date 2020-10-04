using UnityEngine;

public abstract class Piece : MonoBehaviour
{
    public Cell currentCell = null;

    [SerializeField] protected bool isWhite = false;
    [SerializeField] protected Path path = null;

    protected bool allowMove = false;

    protected virtual void OnEnable()
    {
        ClickSelect.OnSelectedObjectChange += SelectedObjectChangeEventHandler;
    }

    protected virtual void SelectedObjectChangeEventHandler(GameObject clickedObject)
    {
        if (clickedObject.Equals(gameObject))
        {
            CreatePath();
        }

        if (clickedObject.TryGetComponent(out Cell cell))
        {
            foreach (Collider2D collider in path.colliders)
            {
                if (collider.OverlapPoint(cell.transform.position))
                {
                    cell = currentCell;
                    transform.position = cell.transform.position;
                    return;
                }
            }
        }

        if (clickedObject.TryGetComponent(out Piece piece))
        {
            currentCell = piece.currentCell;
            Destroy(piece.gameObject);
        }
    }

    protected virtual void CreatePath()
    {
        allowMove = true;

        foreach (Collider2D collider in path.colliders)
        {
            collider.enabled = true;
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out Cell cell))
        {
            //change cell sprite
        }
    }
}
