using System;
using UnityEngine;

/// <summary>
/// 移動のログ
/// </summary>
[Serializable]
public class MoveLog : LogBase
{
    private string pieceName;

    /// <param name="pieceName">移動したコマの名前</param>
    public MoveLog(bool is1P,string pieceName) : base(is1P)
    {
        this.pieceName = pieceName;
    }

    public override string GetLogMessage()
    {
        return $"{GetSubjective()}の{pieceName}が移動しました。";
    }
}
