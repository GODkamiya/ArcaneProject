using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionPanelViewObject : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI pieceNameText;

    [SerializeField]
    TextMeshProUGUI pieceDescriptionText;

    [SerializeField]
    public GameObject standardButton;

    [SerializeField]
    public GameObject reverseButton;

    // 色の定義
    private readonly Color focusColor = new Color(1f, 1f, 1f, 1f);
    private readonly Color unfocusColor = new Color(170f / 255f, 170f / 255f, 170f / 255f, 1f);

    /// <summary>
    /// コマの名前を表示する
    /// </summary>
    public void ShowPieceName(string pieceName)
    {
        pieceNameText.text = pieceName;
    }

    /// <summary>
    /// コマの技の説明を表示する
    /// </summary>
    public void ShowPieceDescription(string pieceDescription, bool isReverse)
    {
        pieceDescriptionText.text = pieceDescription;
        LightOn(isReverse);
    }

    /// <summary>
    /// ボタンの表示を切り替える
    /// </summary>
    private void LightOn(bool isReverse)
    {
        if (isReverse)
        {
            reverseButton.transform.SetAsLastSibling();
            ColorReverseButton(true);
            ColorStandardButton(false);
        }
        else
        {
            standardButton.transform.SetAsLastSibling();
            ColorStandardButton(true);
            ColorReverseButton(false);
        }
    }

    /// <summary>
    /// 正位置ボタンの色を変更する
    /// </summary>
    /// <param name="isFocus">現在フォーカスがあたっているかどうか</param>
    private void ColorStandardButton(bool isFocus)
    {
        standardButton.GetComponent<Image>().color = isFocus ? focusColor : unfocusColor;
        standardButton.GetComponentInChildren<TextMeshProUGUI>().color = isFocus ? focusColor : unfocusColor;
    }

    /// <summary>
    /// 逆位置ボタンの色を変更する
    /// </summary>
    /// <param name="isFocus">現在フォーカスがあたっているかどうか</param>
    private void ColorReverseButton(bool isFocus)
    {
        reverseButton.GetComponent<Image>().color = isFocus ? focusColor : unfocusColor;
        reverseButton.GetComponentInChildren<TextMeshProUGUI>().color = isFocus ? focusColor : unfocusColor;
    }
}
