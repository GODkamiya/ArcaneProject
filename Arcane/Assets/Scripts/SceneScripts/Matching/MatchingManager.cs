using System.Collections;
using System.Collections.Generic;
using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// マッチング画面のUIに紐づくスクリプト
/// 基本的なマッチングの操作を行う
/// </summary>
public class MatchingManager : MonoBehaviour
{
    // テキストフィールド
    [SerializeField] private TMP_InputField _usernameField;
    [SerializeField] private TMP_InputField _roomnameField;

    [SerializeField] private NetworkRunner _networkRunnerPrefab;
    private NetworkRunner _runnerInstance = null;

    /// <summary>
    ///  フィールドに入力された情報を基に、マッチングを開始する
    /// </summary>
    public async void StartGame()
    {
        // NetworkRunnerを立ち上げる
        _runnerInstance = FindFirstObjectByType<NetworkRunner>();
        if(_runnerInstance == null){
            _runnerInstance = Instantiate(_networkRunnerPrefab);
        }

        _runnerInstance.ProvideInput = true;

        // オンラインの設定を行う
        var startGameArgs = new StartGameArgs(){
            GameMode = GameMode.Shared,
            SessionName = _roomnameField.text,
            Scene = SceneRef.FromIndex(SceneUtility.GetBuildIndexByScenePath("Scenes/GameScene")),
        };

        await _runnerInstance.StartGame(startGameArgs);
    }

}
