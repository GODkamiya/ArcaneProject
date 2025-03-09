using System;

/// <summary>
/// コマの種類を定義する
/// </summary>
public enum PieceType{
    Fool,Magician,HighPriestess,Empress,Emperor,Hierophant,Lovers,Chariot,Strength,
    Hermit,WheelOfFortune,Justice,HangedMan,Death,Temperance,Devil,Tower,Star,Moon,
    Sun,Judgement,World
}

public class PieceTypeExtension{
    public static string GetNameFromPieceType(PieceType pieceType){
        switch (pieceType)
        {
            case PieceType.Fool:
                return "愚者";
            case PieceType.Magician:
                return "魔術師";
            case PieceType.HighPriestess:
                return "女教皇";
            case PieceType.Empress:
                return "女帝";
            case PieceType.Emperor:
                return "皇帝";
            case PieceType.Hierophant:
                return "教皇";
            case PieceType.Lovers:
                return "恋人";
            case PieceType.Chariot:
                return "戦車";
            case PieceType.Strength:
                return "力";
            case PieceType.Hermit:
                return "隠者";
            case PieceType.WheelOfFortune:
                return "運命の輪";
            case PieceType.Justice:
                return "正義";
            case PieceType.HangedMan:
                return "吊るされた男";
            case PieceType.Death:
                return "死神";
            case PieceType.Temperance:
                return "節制";
            case PieceType.Devil:
                return "悪魔";
            case PieceType.Tower:
                return "塔";
            case PieceType.Star:
                return "星";
            case PieceType.Moon:
                return "月";
            case PieceType.Sun:
                return "太陽";
            case PieceType.Judgement:
                return "審判";
            case PieceType.World:
                return "世界";
        }
        return "";
    }
}