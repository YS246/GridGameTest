using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CellInfoUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coordinateText;
    [SerializeField] private TextMeshProUGUI cellTypeText;

    public void UpdateCellInfo(CellInfo info)
    {
        coordinateText.text = info.coordinate.ToString();

        cellTypeText.text = info.cellType.ToString();
    }

    public void CancelCellInfo()
    {
        coordinateText.text = string.Empty;

        cellTypeText.text = string.Empty;
    }
}
