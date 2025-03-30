using System;
using UnityEngine;

[Serializable]
public abstract class LogBase
{
    private bool is1P;
    public LogBase(bool is1P){
        this.is1P = is1P; 
    }

    public abstract string GetLogMessage();

    /// <summary>
    /// このログの発生者側の呼称を取得する。
    /// 自分が起こしたログならば「自分」、相手が起こしたログならば「相手」
    /// </summary>
    /// <returns></returns>
    public string GetSubjective(){
        return is1P == GameManager.singleton.GetIs1P()? "自分":"相手";
    }
    /// <summary>
    /// このログの発生者からみた相手側の呼称を取得する。
    /// 自分が起こしたログならば「相手」、自分が起こしたログならば「自分」
    /// </summary>
    /// <returns></returns>
    public string GetObjective(){
        return is1P == GameManager.singleton.GetIs1P()? "相手":"自分";
    }
}
