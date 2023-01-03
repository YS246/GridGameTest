using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePointer : MonoBehaviour
{
    [SerializeField] private Camera mainCam;

    private GameObject currentHoverObj;
    private GameObject currentSelectedObj;

    private void Update()
    {
        UpdatePointer();
    }

    public void UpdatePointer()
    {
        RaycastHit hit;

        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

        bool clicked = Input.GetMouseButtonDown(0);

        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.transform.gameObject;

            if (currentHoverObj != hitObject)
            {
                SwitchHoverObj(hitObject);
            }

            if (clicked)
            {
                if (hitObject.TryGetComponent(out IGamePointerDownHandler downable))
                {
                    downable.OnGamePointerDown();
                }

                SwitchSelection(hitObject);
            }
        }
        else
        {
            TryExitCurrentHover();

            if (clicked)
            {
                TryDeselectCurrentSelected();
            }
        }
    }

    private void TryExitCurrentHover()
    {
        if (currentHoverObj != null)
        {
            if (currentHoverObj.TryGetComponent(out IGamePointerExitHandler currentExit))
            {
                currentExit.OnGamePointerExit();

                currentHoverObj = null;
            }
        }
    }

    private void SwitchHoverObj(GameObject hitObject)
    {
        TryExitCurrentHover();

        currentHoverObj = hitObject;

        if (hitObject.TryGetComponent(out IGamePointerEnterHandler enterable))
        {
            enterable.OnGamePointerEnter();
        }
    }

    private void TryDeselectCurrentSelected()
    {
        if (currentSelectedObj != null)
        {
            if (currentSelectedObj.TryGetComponent(out IGamePointerSelectable currentSelectable))
            {
                currentSelectable.OnDeselect();

                currentSelectedObj = null;
            }
        }
    }

    private void SwitchSelection(GameObject hitObject)
    {
        if (currentSelectedObj != hitObject)
        {
            TryDeselectCurrentSelected();
        }

        if (hitObject.TryGetComponent(out IGamePointerSelectable selectable))
        {
            selectable.OnSelect();

            currentSelectedObj = hitObject;
        }
    }
}
