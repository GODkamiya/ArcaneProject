using System;
using UnityEngine;

public class PlayerClickHandleManager : MonoBehaviour
{
    public event Action<GameObject> onClick;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHithit = new RaycastHit();
            if (Physics.Raycast(ray, out raycastHithit))
            {
                ClickObject(raycastHithit);
            }
        }
    }

    void ClickObject(RaycastHit clickedObject)
    {
        GameObject hitObject = clickedObject.collider.gameObject;
        onClick?.Invoke(hitObject);
    }
}
