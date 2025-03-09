using UnityEngine;

public abstract class ActivePieceObject : PieceObject
{
    public bool canActive = true;
    public abstract void ActiveEffect();

    /// <summary>
    /// そのコマが現在、技を使用可能な段階かを示す。
    /// </summary>
    public abstract bool CanSpellActiveEffect();

    public bool CanSpellActiveEffectMaster(){
        // 近くに悪魔がいる場合は、技を発動できない
        for(int i = -2; i <= 2 ; i++){
            for(int j = -2; j <= 2; j++){
                if(x+i < 0 || x + i > 9 || y + j < 0 || y + j > 9) continue;
                if(BoardManager.singleton.onlinePieces[x+i, y+ j] != null){
                    var target = BoardManager.singleton.onlinePieces[x+i,y+j].GetComponent<PieceObject>();
                    if(target.isMine)continue;
                    if(!target.isReverse)continue;
                    if(target.GetPieceType() == PieceType.Devil) return false;
                }
            }
        }

        // 節制に制御されている場合は、技を発動できない
        if(temperance != null) return false;

        return CanSpellActiveEffect();
    }

}
