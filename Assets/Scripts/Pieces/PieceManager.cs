using UnityEngine;
using System.Collections.Generic;

public class PieceManager : MonoBehaviour
{
    public static List<Piece> whitePieces = new List<Piece>();
    public static List<Piece> blackPieces = new List<Piece>();

    [SerializeField] private GameObject[] piecePrefabs = null;
    [SerializeField] private Sprite[] alternateSprites = null;

    private void Awake()
    {
        foreach (GameObject piece in piecePrefabs)
        {
            piece.GetComponent<Piece>().Spawn();
        }
    }
}
