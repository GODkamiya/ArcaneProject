using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public abstract class TargetFilter
{
    /// <summary>
    /// ターゲットの条件を書く場所
    /// </summary>
    /// <returns></returns>
    public abstract bool filterCondition(GameObject target);
}
