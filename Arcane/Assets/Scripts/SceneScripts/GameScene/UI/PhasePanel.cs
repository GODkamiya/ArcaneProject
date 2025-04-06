
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PhasePanel : MonoBehaviour
{
    public static PhasePanel singleton;

    // UIパーツ
    // 自分/相手の表示パネル
    [SerializeField]
    private GameObject allyPanel,enemyPanel;
    // ターン数表示テキスト
    [SerializeField]
    private TextMeshProUGUI turnCountText;
    // フェーズ表示テキスト
    [SerializeField]
    private TextMeshProUGUI phaseText;

    // 現在が自分のターンかどうか
    private bool isMyTurn = true;

    // 現在が何ターン目か
    private int nowTurn = 0;

    void Awake()
    {
        singleton = this;
    }

    /// <summary>
    /// UIを機能させるために最初に呼ばれる処理 初期化機構
    /// </summary>
    public void Initialize(bool isFirstMyTurn){
        nowTurn = 0;
        isMyTurn = !isFirstMyTurn;
    }

    /// <summary>
    /// 現在のターン表示を自分/相手で切り替える
    /// </summary>
    public void SwitchPlayer(){
        isMyTurn = !isMyTurn;
        nowTurn++;
        Rebuild("召喚/ドロー");
    }

    /// <summary>
    /// フェーズ名を変更する
    /// </summary>
    public void ChangePhase(string phaseTitle){
        Rebuild(phaseTitle);
    }

    /// <summary>
    /// 現在の状態を基にUIを再描画する。
    /// </summary>
    private void Rebuild(string phaseTitle){
        ChangeNameView(allyPanel,isMyTurn);
        ChangeNameView(enemyPanel,!isMyTurn);
        turnCountText.text = $"{nowTurn}ターン目";
        phaseText.text = phaseTitle;
        GetComponent<Image>().color = isMyTurn ? new Color(0,1,0,100f/255f):new Color(1,0,0,100f/255f);
    }

    /// <summary>
    /// 自分/相手の部分の表示を切り替える
    /// </summary>
    private void ChangeNameView(GameObject targetPanel,bool isOn){
        targetPanel.GetComponentInChildren<TextMeshProUGUI>().color = new Color(0,0,0,isOn?1:0.5f);
        targetPanel.GetComponent<Image>().color = new Color(0,0,0,isOn?0:0.5f);
    }
}
