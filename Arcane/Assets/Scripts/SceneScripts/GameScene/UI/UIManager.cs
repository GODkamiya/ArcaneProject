using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager singleton { get; private set;}
    [SerializeField]
    GameObject drawOrSummonPanel;
    [SerializeField]
    GameObject initSummonPanel;
    [SerializeField]
    GameObject initSelectKingPanel;
    [SerializeField]
    GameObject gameEndPanel;

    private void Awake()
    {
        singleton = this;
    }
    public void ShowDrawOrSummonPanel()
    {
        drawOrSummonPanel.SetActive(true);
    }
    public void HideDrawOrSummonPanel()
    {
        drawOrSummonPanel.SetActive(false);
    }
    public void ShowInitSummonPanel()
    {
        initSummonPanel.SetActive(true);
    }
    public void HideInitSummonPanel()
    {
        initSummonPanel.SetActive(false);
    }
    public void ShowInitSelectKingPanel()
    {
        initSelectKingPanel.SetActive(true);
    }
    public void HideInitSelectKingPanel()
    {
        initSelectKingPanel.SetActive(false);
    }
    public void ShowGameEndPanel(bool is1pWin)
    {
        gameEndPanel.SetActive(true);
        gameEndPanel.transform.Find("WinLabel").GetComponent<TextMeshProUGUI>().text = is1pWin ? "Player1 WIN!!!" : "Player2 WIN!!!" ;
    }
    public void HideGameEndPanel()
    {
        gameEndPanel.SetActive(false);
    }
}
