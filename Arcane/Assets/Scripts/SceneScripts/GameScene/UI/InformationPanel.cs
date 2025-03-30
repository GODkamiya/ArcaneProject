using UnityEngine;

/// <summary>
/// ゲーム画面左側に表示されている情報パネルの全体を統括するクラス
/// </summary>
public class InformationPanel : MonoBehaviour
{
    public static InformationPanel singleton;
    void Awake() => singleton = this;

    [SerializeField]
    private GameObject descriptionPanel,logPanel;

    /// <summary>
    /// 説明ボタンが押されたときの挙動
    /// 説明パネルを表示し、ログパネルを閉じる
    /// </summary>
    public void OnClickDescriptionButton(){
        descriptionPanel.SetActive(true);
        logPanel.SetActive(false);
    }

    /// <summary>
    /// ログボタンが押されたときの挙動
    /// ログパネルを表示し、説明パネルを閉じる
    /// </summary>
    public void OnClickLogPanel(){
        descriptionPanel.SetActive(false);
        logPanel.SetActive(true);
    }

    public LogPanel GetLogPanel() => logPanel.GetComponent<LogPanel>();
}
