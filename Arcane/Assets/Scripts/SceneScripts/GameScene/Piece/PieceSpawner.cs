using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceSpawner : MonoBehaviour
{
    public static PieceSpawner singleton;

    [SerializeField]
    private GameObject foolPrefab,magicianPrefab,highpriestessPrefab,empressPrefab,emperorPrefab,hierophantPrefab,loversPrefab,
    chariotPrefab,strengthPrefab,hermitPrefab,wheelOfFortunePrefab,justicePrefab,hangedManPrefab,
    deathPrefab,temperancePrefab,devilPrefab,towerPrefab,starPrefab,sunPrefab,moonPrefab,JudgementPrefab,worldPrefab;

    public PieceSpawner(){
        singleton = this;
    }

    /// コマのPrefabを返却する
    public GameObject GetPiecePrefab(PieceType pieceType){
        switch(pieceType){
            case PieceType.Fool:
                return foolPrefab;
            case PieceType.Magician:
                return magicianPrefab;
            case PieceType.HighPriestess:
                return highpriestessPrefab;
            case PieceType.Empress:
                return empressPrefab;
            case PieceType.Emperor:
                return emperorPrefab;
            case PieceType.Hierophant:
                return hierophantPrefab;
            case PieceType.Lovers:
                return loversPrefab;
            case PieceType.Chariot:
                return chariotPrefab;
            case PieceType.Strength:
                return strengthPrefab;
            case PieceType.Hermit:
                return hermitPrefab;
            case PieceType.WheelOfFortune:
                return wheelOfFortunePrefab;
            case PieceType.Justice:
                return justicePrefab;
            case PieceType.HangedMan:
                return hangedManPrefab;
            case PieceType.Death:
                return deathPrefab;
            case PieceType.Temperance:
                return temperancePrefab;
            case PieceType.Devil:
                return devilPrefab;
            case PieceType.Tower:
                return towerPrefab;
            case PieceType.Star:
                return starPrefab;
            case PieceType.Moon:
                return moonPrefab;
            case PieceType.Sun:
                return sunPrefab;
            case PieceType.Judgement:
                return JudgementPrefab;
            case PieceType.World:
                return worldPrefab;
            default:
                throw new System.Exception();
        }
    }
}
