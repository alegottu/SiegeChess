using UnityEngine;

public class CellManager : MonoBehaviour
{
    public static Cell[] cells = new Cell[64];

    [SerializeField] private GameObject cellPrefab = null;
    [SerializeField] private Color[] cellColors = null;
    [SerializeField] private Transform startingPos = null;
    [SerializeField] private float cellDiameter = 1;

    private int index = 0;

    private void Awake()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                GameObject newCell = Instantiate(cellPrefab, startingPos.position + Vector3.right * cellDiameter * j, Quaternion.identity);
                newCell.transform.parent = gameObject.transform;
                newCell.GetComponent<SpriteRenderer>().color = j % 2 == 0 ? cellColors[0] : cellColors[1];

                int name = index + 1;
                newCell.name = name.ToString();

                cells[index] = newCell.GetComponent<Cell>();
                index++;
            }
            startingPos.position += Vector3.up * cellDiameter;
        }
    }
}
