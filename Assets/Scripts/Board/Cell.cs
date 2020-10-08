using UnityEngine;

public class Cell : MonoBehaviour
{
    [HideInInspector] public Vector2 bounds = Vector2.zero;
    [HideInInspector] public Piece currentPiece = null;
    [HideInInspector] public Color standard = new Color();
    public SpriteRenderer spriteRender = null;

    [SerializeField] private Collider2D _bounds = null;
    [SerializeField] private Color selected = new Color();

    private void Start()
    {
        bounds = _bounds.bounds.size;
        standard = spriteRender.color;
    }

    private void OnEnable()
    {
        ClickSelecter.OnSelectedObjectChange += SelectedObjectChangeEventHandler;
    }

    private void SelectedObjectChangeEventHandler(GameObject clickedObject)
    {
        if (clickedObject.Equals(gameObject))
        {
            spriteRender.color = selected;
        }
        else if (spriteRender.color.Equals(selected))
        {
            spriteRender.color = standard;
        }
    }

    private void OnDisable()
    {
        ClickSelecter.OnSelectedObjectChange += SelectedObjectChangeEventHandler;
    }
}
