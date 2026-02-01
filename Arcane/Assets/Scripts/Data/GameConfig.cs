using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Arcane/GameConfig")]
public class GameConfig : ScriptableObject
{
    [Header("Board Settings")]
    [Tooltip("ボードの縦横のサイズ")]
    public int BoardSize = 7;
}
