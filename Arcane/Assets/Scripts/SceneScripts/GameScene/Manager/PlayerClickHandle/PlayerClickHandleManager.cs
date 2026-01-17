using UnityEngine;
using VContainer;

public class PlayerClickHandleManager : MonoBehaviour
{
    private PlayerClickHandleController _controller;

    [Inject]
    public void Construct(PlayerClickHandleController controller)
    {
        _controller = controller;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHithit))
            {
                ClickObject(raycastHithit);
            }
        }
    }

    void ClickObject(RaycastHit clickedObject)
    {
        GameObject hitObject = clickedObject.collider.gameObject;
        _controller.OnClick(hitObject);
    }
}
