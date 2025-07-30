using System.Collections.Generic;
using UnityEngine;

public class CellMapRename : MonoBehaviour
{
    [ContextMenu("📛 Rename Cells (Column-Wise A0 to H15)")]
    public void RenameCells()
    {
        int index = 0;

        for (int col = 0; col < 8; col++)     // ستون‌ها: A تا H
        {
            for (int row = 0; row < 16; row++) // ردیف‌ها: 0 تا 15
            {
                //if (index >= transform.childCount)
                //{
                //    Debug.LogWarning("🚨 تعداد سلول‌ها کمتر از انتظار بود.");
                //    return;
                //}

                string cellName = $"{(char)('A' + col)}{row}";
                transform.GetChild(index).name = cellName;

                index++;
            }
        }
    }
}