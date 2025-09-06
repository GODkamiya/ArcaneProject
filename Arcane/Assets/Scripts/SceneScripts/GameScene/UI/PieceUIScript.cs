using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;

public class PieceUIScript : MonoBehaviour
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    public PieceType pieceType { get; set; }

    private DescriptionPanelController _descriptionPanelController;

    [Inject]
    public void Inject(DescriptionPanelController descriptionPanelController)
    {
        _descriptionPanelController = descriptionPanelController;
    }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnClick()
    {
        PlayerClickHandler.singleton.ClickHand(pieceType);
        _descriptionPanelController.SetPieceInfo(PieceSpawner.singleton.GetPiecePrefab(pieceType).GetComponent<PieceObject>());
    }
}