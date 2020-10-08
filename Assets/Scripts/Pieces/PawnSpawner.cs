using UnityEngine;

public class PawnSpawner : MonoBehaviour
{
    [SerializeField] private GameObject pawnPrefab = null;
    [SerializeField] private Sprite alternate = null;

    private void Awake()
    {
        for (int x = 0; x < 2; x++)
        {
            int row = x == 0 ? 2 : 5;

            for (int y = 0; y < 8; y++)
            {
                Cell cell = CellManager.cells[new Vector2Int(y, row)];
                GameObject pawn = Instantiate(pawnPrefab, cell.transform.position, Quaternion.identity);
                Piece piece = pawn.GetComponent<Piece>();
                piece.currentCell = cell;

                if (x == 0) 
                {
                    piece.spriteRender.sprite = alternate;
                    piece.isWhite = true;
                }
            }
        }
    }
}
