using UnityEngine;

/// <summary>
/// コマが倒された後、移動処理が終了したタイミングで呼ばれる
/// </summary>
public interface IOnAfterDeath
{
    public void OnAfterDeath(int x,int y);
}
