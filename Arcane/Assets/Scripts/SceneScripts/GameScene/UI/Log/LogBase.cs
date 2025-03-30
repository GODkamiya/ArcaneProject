using UnityEngine;

public abstract class LogBase
{
    private bool isEnemyLog;
    public LogBase(bool isEnemyLog){
        this.isEnemyLog = isEnemyLog;
    }

    public abstract string GetLogMessage();

    public string Get
}
