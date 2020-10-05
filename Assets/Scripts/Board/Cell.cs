using UnityEngine;

public class Cell : MonoBehaviour
{
    [HideInInspector] public Vector2 bounds = Vector2.zero;
    [HideInInspector] public Piece currentPiece = null;

    [SerializeField] private Collider2D _bounds = null;

    private void Awake()
    {
        bounds = _bounds.bounds.size;
    }
}
