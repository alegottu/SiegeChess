using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PieceManager : MonoBehaviour
{
    [SerializeField] private GameObject[] pieces = null;
    [SerializeField] private Sprite[] alternateSprites = null;

    private List<int> _availableXPositions = new List<int>();
    private List<int> availableXPositions = null;
    private int[] piecePositions = null; // kings first, rooks second, knights third, bishops last

    private void Awake()
    {
        _availableXPositions = Enumerable.Range(0, 7).ToList();

        for (int i = 0; i < 2; i++)
        {
            piecePositions = new int[8];
            availableXPositions = new List<int>(_availableXPositions);

            for (int j = 0; j < 8; j += 2)
            {
                SetPiecePositions(j);
            }

            for (int x = 0; x < 8; x++)
            {
                Cell currentCell = CellManager.cells[i == 0 ? new Vector2Int(piecePositions[x], 0) : new Vector2Int(piecePositions[x], 7)];
                GameObject pieceObj = Instantiate(pieces[x / 2], currentCell.transform.position, Quaternion.identity);
                Piece piece = pieceObj.GetComponent<Piece>();
                piece.currentCell = currentCell;

                if (i == 0)
                {
                    piece.spriteRender.sprite = alternateSprites[x / 2];
                    piece.isWhite = true;
                }
            }
        }
    }

    private void SetPiecePositions(int index)
    {
        int firstX = index == 0 ? Random.Range(0, 4) : availableXPositions[0];
        int secondX = 7 - firstX;

        availableXPositions.Remove(firstX); piecePositions[index] = firstX;
        availableXPositions.Remove(secondX); piecePositions[index + 1] = secondX;
    }
}
