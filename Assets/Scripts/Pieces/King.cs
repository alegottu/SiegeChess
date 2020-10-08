using UnityEngine;
using System.Collections.Generic;

public class King : Piece
{
    public bool inCheck = false;
    public List<Cell> checkedCells = new List<Cell>();

    [SerializeField] private Color _inCheck = new Color();

    protected override void CreatePath()
    {
        base.CreatePath();

        
    }

    private void Update()
    {
        if (inCheck)
        {
            currentCell.spriteRender.color = _inCheck;
        }
        else
        {
            currentCell.spriteRender.color = currentCell.standard;
        }
    }
}
