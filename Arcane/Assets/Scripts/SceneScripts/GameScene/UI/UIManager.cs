using System;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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
    GameObject summonPanel;
    [SerializeField]
    GameObject abilityButton;
    [SerializeField]
    GameObject gameEndPanel;

    [SerializeField]
    GameObject wheelOfFortunePanel;

    private void Awake()
    {
        singleton = this;
    }
    public void ShowDrawOrSummonPanel()
    {
        drawOrSummonPanel.SetActive(true);
        drawOrSummonPanel.transform.Find("SummonButton").gameObject.SetActive(GameManager.singleton.GetLocalPlayerObject().HasOneCard());
    }
    public void HideDrawOrSummonPanel()
    {
        drawOrSummonPanel.SetActive(false);
    }
    public void ShowInitSummonPanel(UnityAction action)
    {
        initSummonPanel.SetActive(true);
        initSummonPanel.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
        initSummonPanel.GetComponentInChildren<Button>().onClick.AddListener(action);
    }
    public void HideInitSummonPanel()
    {
        initSummonPanel.SetActive(false);
    }
    public void ShowInitSelectKingPanel(UnityAction action)
    {
        initSelectKingPanel.SetActive(true);
        initSelectKingPanel.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
        initSelectKingPanel.GetComponentInChildren<Button>().onClick.AddListener(action);
    }
    public void HideInitSelectKingPanel()
    {
        initSelectKingPanel.SetActive(false);
    }
    public void ShowSummonPanel(UnityAction action){
        summonPanel.SetActive(true);
        summonPanel.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
        summonPanel.GetComponentInChildren<Button>().onClick.AddListener(action);
    }
    public void HideSummonPanel(){
        summonPanel.SetActive(false);
    }
    public void ShowAbilityButton(UnityAction action){
        abilityButton.SetActive(true);
        abilityButton.GetComponent<Button>().onClick.RemoveAllListeners();
        abilityButton.GetComponent<Button>().onClick.AddListener(action);
    }
    public void HideAbilityButton(){
        abilityButton.SetActive(false);
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
    public void ShowChooseOneClickPanel(ChooseOneClickAction action){
        wheelOfFortunePanel.SetActive(true);
        var button = wheelOfFortunePanel.GetComponentInChildren<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(action.OnPressButton);
    }
    public void HideChooseOneClickPanel(){
        wheelOfFortunePanel.SetActive(false);
    }
    public void ShowChooseOneTilePanel(ChooseOneTileAction action){
        wheelOfFortunePanel.SetActive(true);
        var button = wheelOfFortunePanel.GetComponentInChildren<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(action.OnPressButton);
    }
    public void HideChooseOneTilePanel(){
        wheelOfFortunePanel.SetActive(false);
    }
}
