using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager singleton { get; private set;}
    [SerializeField]
    GameObject drawOrSummonPanel;

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
}
