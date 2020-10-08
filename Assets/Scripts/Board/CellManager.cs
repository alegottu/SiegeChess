using UnityEngine;
using System.Collections.Generic;

public class CellManager : MonoBehaviour
{
    public static Dictionary<Vector2Int, Cell> cells = new Dictionary<Vector2Int, Cell>(); //find each cell by its (column, row) coordinates
    public static Dictionary<Cell, Vector2Int> cellPositions = new Dictionary<Cell, Vector2Int>();

    [SerializeField] private GameObject cellPrefab = null;
    [SerializeField] private Color[] cellColors = null;
    [SerializeField] private Transform startingPos = null;
    [SerializeField] private float cellDiameter = 1;

    private Dictionary<int, string> columns = new Dictionary<int, string>()
    {
        { 0, "A" }, { 1, "B" }, { 2, "C" }, { 3, "D" }, { 4, "E" }, { 5, "F" }, { 6, "G" }, { 7, "H" }
    };

    private void Awake()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                GameObject newCellObj = Instantiate(cellPrefab, startingPos.position + Vector3.right * cellDiameter * j, Quaternion.identity);
                newCellObj.transform.parent = gameObject.transform;
                newCellObj.GetComponent<SpriteRenderer>().color = j % 2 == 0 ? cellColors[0] : cellColors[1];
                newCellObj.name = columns[j] + (i + 1).ToString();

                Cell newCell = newCellObj.GetComponent<Cell>();
                cells[new Vector2Int(j, i)] = newCell;
                cellPositions[newCell] = new Vector2Int(j, i);
            }
            startingPos.position += Vector3.up * cellDiameter;
        }
    }
}
