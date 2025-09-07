using System;
using System.Collections.Generic;
using System.Data;
using UnityEditor;

/// <summary>
/// コマの種類を定義する
/// </summary>
public enum PieceType
{
    Fool, Magician, HighPriestess, Empress, Emperor, Hierophant, Lovers, Chariot, Strength,
    Hermit, WheelOfFortune, Justice, HangedMan, Death, Temperance, Devil, Tower, Star, Moon,
    Sun, Judgement, World
}

/// <summary>
/// PieceTypeに関する情報を格納するクラス
/// </summary>
public class PieceTypeExtension
{
    private static readonly Dictionary<PieceType, PieceInformation> _pieceInfo = new Dictionary<PieceType, PieceInformation>(){
        {PieceType.Fool, new PieceInformation("愚者", "<未実装>", "<未実装>") },
        {PieceType.Magician, new PieceInformation("魔術師", "カウンターを消費して、好きな逆位置のコマ1体を正位置にする。", "カウンターを消費して、好きな正位置のコマ1体を逆位置にする。") },
        {PieceType.HighPriestess, new PieceInformation("女教皇", "このコマを中心にした5×5の範囲内にいる敵コマ1体を、その範囲内の好きな位置に移動させる。", "") },
        {PieceType.Empress, new PieceInformation("女帝", "自分から王範囲にいる別の味方コマが倒れるとき、代わりにこのコマが倒れる。(敵のコマは、女帝の位置に移動する)", "範囲を縦横2の5x5範囲に拡大する。") },
        {PieceType.Emperor, new PieceInformation("皇帝", "このコマは逆位置のコマには倒されない。", "このコマは正位置のコマには倒されない。効果発動でこのコマを正位置にすることができる。") },
        {PieceType.Hierophant, new PieceInformation("教皇", "このコマと同じ列、もしくは同じ行にいる味方コマ1体の移動範囲を、このターンの終わりまで上下左右+1する。", "このコマは効果を使用できなくなる。") },
        {PieceType.Lovers, new PieceInformation("恋人", "<未実装>", "<未実装>") },
        {PieceType.Chariot, new PieceInformation("戦車", "このコマが敵コマを倒した後、その位置から移動範囲内にいる全てのコマを倒す。", "正位置の効果で倒す対象が敵のコマだけになる。") },
        {PieceType.Strength, new PieceInformation("力", "自分の移動範囲内にいる逆位置の敵コマ1体を正位置にする。その後、このコマは逆位置になる。", "自分の移動範囲内にいる正位置のコマ1体を逆位置にする。その後、このコマは正位置になる。") },
        {PieceType.Hermit, new PieceInformation("隠者", "このコマは召喚時、透明になる。透明な状態で移動すると透明が解除される。透明じゃない状態で移動すると透明になる。", "味方のコマ1体を、次の相手のターンの終わりまで透明状態にする。") },
        {PieceType.WheelOfFortune, new PieceInformation("運命の輪", "指定した味方コマ1体とこのコマの位置を入れ替える。", "指定した味方コマ2体の位置を入れ替える。") },
        {PieceType.Justice, new PieceInformation("正義", "盤の中心を基準に、横に線対称な位置に移動する。", "盤の中心を基準に、点対象な位置に移動する。") },
        {PieceType.HangedMan, new PieceInformation("吊るされた男", "このコマは召喚する際に、「吊るされた男」以外のコマ１体に偽装する。動きや効果は変わらない。", "偽装しているコマに変身する。") },
        {PieceType.Death, new PieceInformation("死神", "", "移動範囲が広がる。") },
        {PieceType.Temperance, new PieceInformation("節制", "このコマから一歩前にいるコマは移動も効果も使用できない。", "このコマから上下左右1の範囲内にいるコマは移動も効果も使用できない。") },
        {PieceType.Devil, new PieceInformation("悪魔", "このコマを中心にした5×5の範囲内にいる味方コマは、敵コマの効果の対象にならない。", "このコマを中心にした5×5の範囲内にいる敵コマは効果の発動ができない。") },
        {PieceType.Tower, new PieceInformation("塔", "このコマが倒れたとき、このコマを中心にした5×5の範囲内にいるすべてのコマを取る。", "自爆することができる。") },
        {PieceType.Star, new PieceInformation("星", "このコマの移動範囲内に、コマを召喚できる。", "移動範囲が広がる。") },
        {PieceType.Moon, new PieceInformation("月", "正位置の太陽のコマに変身する。", "") },
        {PieceType.Sun, new PieceInformation("太陽", "正位置の月のコマに変身する。", "") },
        {PieceType.Judgement, new PieceInformation("審判", "敵コマを倒したとき、その敵コマを手札に加える。", "味方コマを倒せるようになる。味方コマも同様に、倒すことで手札に加えられる。") },
        {PieceType.World, new PieceInformation("世界", "このコマは好きな位置に召喚することができる。", "このコマは持ち主の手札に戻る。") },
    };

    /// <summary>
    /// PieceTypeからコマの名前を取得する
    /// </summary>
    /// <param name="pieceType"></param>
    /// <returns></returns>
    public static string GetNameFromPieceType(PieceType pieceType)
    {
        return _GetPieceInformation(pieceType).pieceName;
    }

    /// <summary>
    /// PieceTypeからコマの正位置の効果説明を取得する
    /// </summary>
    public static string GetUprightEffectDescriptionFromPieceType(PieceType pieceType)
    {
        return _GetPieceInformation(pieceType).uprightEffectDescription;
    }

    /// <summary>
    /// PieceTypeからコマの逆位置の効果説明を取得する
    /// </summary>
    public static string GetReverseEffectDescriptionFromPieceType(PieceType pieceType)
    {
        return _GetPieceInformation(pieceType).reverseEffectDescription;
    }

    /// <summary>
    /// PieceTypeからコマの説明クラスを取得する
    /// </summary>
    private static PieceInformation _GetPieceInformation(PieceType pieceType)
    {
        try
        {
            return _pieceInfo[pieceType];
        }
        catch (Exception)
        {
            throw new ArgumentException("定義が存在しないコマが指定されました。");
        }
    }
}