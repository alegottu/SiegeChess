using UnityEngine;
using System;

public class Bishop : Piece
{
    protected override void CreatePath()
    {
        base.CreatePath();

        for (int i = 0; i < 64; i++)
        {
            int difference = Mathf.Abs(Convert.ToInt32(currentCell.name) - i);
            if (difference % 7 == 0|| difference % 9 == 0)
            {
                possibleCells.Add(CellManager.cells[i]);
            }
        }
    }
}
