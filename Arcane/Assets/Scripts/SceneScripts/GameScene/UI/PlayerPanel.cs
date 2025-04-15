using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerPanel : MonoBehaviour
{
    private TextMeshProUGUI text;
    void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    /// <summary>
    /// プレイヤーの情報パネルに表示する文字を設定する
    /// </summary>
    /// <param name="text"></param>
    public void SetText(int deckAmount, int handAmount)
    {
        this.text.text = $"deck:{deckAmount}\nhand:{handAmount}";

    }
}
