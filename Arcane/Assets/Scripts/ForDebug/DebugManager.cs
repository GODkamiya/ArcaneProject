using Fusion;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Matching画面から遷移しなくてもマッチングできるために作成されたデバッグ用のマネージャ
/// </summary>
public class DebugManager : MonoBehaviour
{
    [SerializeField] private NetworkRunner _networkRunnerPrefab;
    private NetworkRunner _runnerInstance = null;

    void Start(){
        StartGame();
    }

    /// <summary>
    ///  フィールドに入力された情報を基に、マッチングを開始する
    /// </summary>
    public async void StartGame()
    {
        // NetworkRunnerを立ち上げる
        _runnerInstance = FindFirstObjectByType<NetworkRunner>();
        if(_runnerInstance == null){
            Debug.Log("デバッグモードを実行します");
            _runnerInstance = Instantiate(_networkRunnerPrefab);
            _runnerInstance.ProvideInput = true;

            var scene = SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex);
            var sceneInfo = new NetworkSceneInfo();
            if (scene.IsValid) {
                sceneInfo.AddSceneRef(scene, LoadSceneMode.Additive);
            }

            // オンラインの設定を行う
            var startGameArgs = new StartGameArgs(){
                GameMode = GameMode.Shared,
                SessionName = "dbgs",
                Scene = scene,
            };

            await _runnerInstance.StartGame(startGameArgs);
        }

    }
}
