using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] private CellInfoUIController cellInfoUI;

    public void UpdateCellInfo(CellInfo info)
    {
        cellInfoUI.UpdateCellInfo(info);
    }

    public void CancelCellInfo()
    {
        cellInfoUI.CancelCellInfo();
    }
}
