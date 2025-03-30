using TMPro;
using UnityEngine;

public class LogPanel : MonoBehaviour
{
    /// <summary>
    /// ログのオブジェクトを収納するスペース
    /// </summary>
    [SerializeField]
    private GameObject logFolder;

    [SerializeField]
    GameObject logPrefab;

    public void AddLog(LogBase log){
        GameObject logObject = Instantiate(logPrefab);
        logObject.GetComponentInChildren<TextMeshProUGUI>().text = log.GetLogMessage();
        logObject.transform.SetParent(logFolder.transform);
    }
}
