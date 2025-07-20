using UnityEngine;

public class Cell : MonoBehaviour
{
    [HideInInspector] public Vector2 bounds = Vector2.zero;
    [HideInInspector] public Piece currentPiece = null;
    [HideInInspector] public Color standard = new Color();
    public SpriteRenderer spriteRender = null;

    [SerializeField] private Collider2D _bounds = null;
    [SerializeField] private Color selected = new Color();
	[SerializeField] private Color possible = new Color();

    private void Start()
    {
        bounds = _bounds.bounds.size;
        standard = spriteRender.color;
    }

    public void Select()
    {
		spriteRender.color = selected;
    }
	
	public void Deselect()
	{
		spriteRender.color = standard;
	}

	public void MarkPossible()
	{
		spriteRender.color = possible;
	}
}
