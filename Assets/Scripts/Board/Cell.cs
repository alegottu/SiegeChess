using UnityEngine;

public class Cell : MonoBehaviour
{
    public Vector2 bounds = Vector2.zero;
    public SpriteRenderer sprite = null;

    [SerializeField] private Collider2D _bounds = null;

    private void Awake()
    {
        bounds = _bounds.bounds.size;
    }
}
