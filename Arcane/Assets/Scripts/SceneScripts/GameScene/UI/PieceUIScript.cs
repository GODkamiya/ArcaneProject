using UnityEngine;
using UnityEngine.EventSystems;

public class PieceUIScript : MonoBehaviour,IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0.6f; // 半透明にする
            canvasGroup.blocksRaycasts = false; // ドラッグ中にレイキャストを無効にする
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (canvas != null)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 1f; // 元に戻す
            canvasGroup.blocksRaycasts = true; // レイキャストを有効に戻す
        }
    }
}