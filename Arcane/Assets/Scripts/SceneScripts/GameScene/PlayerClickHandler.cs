using UnityEngine;

public class PlayerClickHandler : MonoBehaviour
{
    public static PlayerClickHandler singleton;
    public PieceType? selectedPieceType { get; set; }
    private bool isPieceFromHand = false;
    private bool isKingSelect = false;
    public PieceType? kingPieceType = null;
    private GameObject selectedPieceObject;
    private void Awake(){
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
        if (hitObject.tag == "Board" && selectedPieceType != null)
        {
            ClickBoard(hitObject);
        }
        else if (hitObject.tag == "Piece")
        {
            if(isKingSelect){
                kingPieceType = hitObject.GetComponent<PieceObject>().GetPieceType();
            }else{
                selectedPieceType = hitObject.GetComponent<PieceObject>().GetPieceType();
                isPieceFromHand = false;
                selectedPieceObject = hitObject;
            }
        }
    }
    void ClickBoard(GameObject hitObject)
    {
        BoardBlock bb = hitObject.GetComponent<BoardBlock>();
        if (bb.y >= 3) return;

        BoardManager.singleton.SetPiece(selectedPieceType ?? PieceType.Fool, bb.x, bb.y); // TODO nullの対処
        
        if (isPieceFromHand)
        {
            PlayerObject po = GameManager.singleton.GetLocalPlayerObject();
            po.RemoveHand(selectedPieceType ?? PieceType.Fool); // TODO nullの対処
        }
        else
        {
            BoardManager.singleton.RemoveLocalPiece(selectedPieceObject);
        }
        selectedPieceType = null;

    }
    public void SetSelectedPieceFromHand(PieceType pieceType)
    {
        selectedPieceType = pieceType;
        isPieceFromHand = true;
        selectedPieceObject = null;
    }
    public void SwitchIsSelectKing(){
        isKingSelect = true;
    }
}
