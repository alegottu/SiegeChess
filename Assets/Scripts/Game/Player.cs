using System;
using UnityEngine;

public class Player : MonoBehaviour
{
	public static event Action<bool> onTurnEnd;

    [SerializeField] private Camera cam = null;
    [SerializeField] private LayerMask layersToHit = new LayerMask();
	[SerializeField] private bool isWhite = false;
	private bool turnActive;

    private Cell selectedCell = null;
	private Piece selectedPiece = null;

	private void Awake()
	{
		turnActive = isWhite;
	}

	private void OnEnable()
	{
		Player.onTurnEnd += OnTurnEndEventHandler;
	}

    private void Update()
    {
        if (turnActive && Input.GetMouseButtonDown(0))
        {
            Vector3 clickPoint = cam.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D click = Physics2D.Raycast(clickPoint, Vector2.zero, Mathf.Infinity, layersToHit);

            if (click)
            {
				Cell cell = click.collider.gameObject.GetComponent<Cell>();
				
				if (cell == selectedCell)
				{
					return;
				}

				if (selectedCell != null)
				{
					selectedCell.Deselect();
				}

				if (selectedPiece != null)
				{
					if (selectedPiece.Deselect(cell))
					{
						turnActive = false;
						onTurnEnd?.Invoke(isWhite);
						selectedPiece = null;
						return;
					}

					selectedPiece = null;
				}

				cell.Select();
				selectedCell = cell;

				if (cell.currentPiece != null && cell.currentPiece.isWhite == isWhite)
				{
					cell.currentPiece.Select();
					selectedPiece = cell.currentPiece;
				}
            }
        }
    }

	private void OnTurnEndEventHandler(bool white)
	{
		if (white != isWhite)
		{
			turnActive = true;
		}
	}

	private void OnDisable()
	{
		Player.onTurnEnd -= OnTurnEndEventHandler;
	}
}
