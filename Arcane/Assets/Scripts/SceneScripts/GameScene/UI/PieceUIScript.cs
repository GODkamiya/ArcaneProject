using UnityEngine;
using UnityEngine.EventSystems;

public class PieceUIScript : MonoBehaviour
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    public PieceType pieceType{ get; set; }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnClick(){
        GameManager.singleton.SetSelectedPieceFromHand(pieceType);
    }
}