using System;
using UnityEngine;

[Serializable]
public class MoveLog : LogBase
{
    private string pieceName;
    public MoveLog(bool is1P,string pieceName) : base(is1P)
    {
        this.pieceName = pieceName;
    }

    public override string GetLogMessage()
    {
        return $"{GetSubjective()}の{pieceName}が移動しました。";
    }
}
