using System;
using UnityEngine;

[Serializable]
public class DrawLog : LogBase
{
    public DrawLog(bool is1P) : base(is1P)
    {
    }

    public override string GetLogMessage()
    {
        return $"{GetSubjective()}がコマを1枚ドローしました。";
    }
}
