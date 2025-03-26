using UnityEngine;

public class PlayerClickHandler : MonoBehaviour
{
    public static PlayerClickHandler singleton;
    public IClickAction clickAction{ get; set; }

    /// <summary>
    /// クリックしたコマの詳細を見るために
    /// </summary>
    [SerializeField]
    private DiscriptionPanel discriptionPanel;

    private void Awake()
    {
        singleton = this;
    }
    // Update is called once per frame
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
        if (hitObject.tag == "Board")
        {
            BoardBlock bb = hitObject.GetComponent<BoardBlock>();
            clickAction.OnClickBoard(bb);
        }
        else if (hitObject.tag == "Piece")
        {
            discriptionPanel.SetTargetPiece(hitObject.GetComponent<PieceObject>());
            clickAction.OnClickPiece(hitObject);
        }
    }
    public void ClickHand(PieceType pieceType)
    {
        if(clickAction is IClickHand clickHand)clickHand.OnClickHand(pieceType);
    }
}
