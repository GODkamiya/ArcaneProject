using System;
using UnityEngine;

/// <summary>
/// 攻撃のログ
/// </summary>
[Serializable]
public class AttackLog : LogBase
{
    private string attackerName;
    private string targetName;
    private bool isTargetControlledByEnemy;

    /// <param name="attackerName">攻撃したコマの名前</param>
    /// <param name="targetName">攻撃されたコマの名前</param>
    /// <param name="isTargetControlledByEnemy">攻撃されたコマの所持者が敵かどうか</param>
    public AttackLog(bool is1P,string attackerName,string targetName, bool isTargetControlledByEnemy) : base(is1P)
    {
        this.attackerName=attackerName;
        this.targetName=targetName;
        this.isTargetControlledByEnemy=isTargetControlledByEnemy;
    }

    public override string GetLogMessage()
    {
        return $"{GetSubjective()}の{attackerName}が{(isTargetControlledByEnemy?GetObjective():GetSubjective())}の{targetName}を倒しました。";
    }
}
