using System;
using System.Runtime.ConstrainedExecution;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiscriptionPanel : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI pieceNameLabel;

    [SerializeField]
    TextMeshProUGUI uprightDetailLabel, reverseDetailLabel;

    private PieceObject targetPiece;

    /// <summary>
    /// 描画する対象のコマを設定する。
    /// </summary>
    /// <param name="pieceObject"></param>
    public void SetTargetPiece(PieceObject pieceObject){
        // 過去の購読を解除する
        if(targetPiece!=null){
            targetPiece.onChangeInformation -= Render;
        }

        // 新たな購読を開始する
        targetPiece = pieceObject;
        targetPiece.onChangeInformation += Render;
        Render();
    }

    /// <summary>
    /// 対象にしているコマの内容をUIに描画する。
    /// </summary>
    public void Render(){
        // 名前の描画
        pieceNameLabel.text = targetPiece.GetName();
        
        // 正位置効果の描画
        var uprightDetailText = targetPiece.GetUprightEffectDescription();
        uprightDetailLabel.text = uprightDetailText==""?"なし。":uprightDetailText;
        
        // 逆位置効果の描画
        var reverseDetailText = targetPiece.GetReverseEffectDescription();
        reverseDetailLabel.text = reverseDetailText==""?"なし。":reverseDetailText;
        reverseDetailLabel.transform.parent.GetComponent<Image>().color = targetPiece.isReverse? new Color(1f,1f,1f,100f/255f):new Color(0f,0f,0f,100f/255f);
    }
}
