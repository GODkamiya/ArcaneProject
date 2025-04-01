using System;
using UnityEngine;

/// <summary>
/// 召喚のログ
/// </summary>
[Serializable]
public class SummonLog : LogBase
{
    private string pieceName;

    /// <param name="pieceName">召喚したコマの名前</param>
    public SummonLog(bool is1P,string pieceName) : base(is1P)
    {
        this.pieceName = pieceName;
    }

    public override string GetLogMessage()
    {
        return $"{GetSubjective()}が{pieceName}を召喚しました。";
    }
}
