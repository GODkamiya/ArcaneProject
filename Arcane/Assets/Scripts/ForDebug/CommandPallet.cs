using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// デバッグ用のコマンドパレットの内容を定義することが出来る
/// </summary>
public class CommandPallet : MonoBehaviour
{
    private TMP_InputField textMeshProUGUI;

    void Start()
    {
        textMeshProUGUI = GetComponent<TMP_InputField>();    
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Return)){
            CommandAnalyze();
            gameObject.SetActive(false);
        }
    }

    void CommandAnalyze(){
        string command = textMeshProUGUI.text;
        string[] args = command.Split(" ");
        textMeshProUGUI.text = "";
        if(args.Length <= 0) return;

        switch(args[0]){
            case "give":
                GiveCommand(args);
                break;
            default:
                return;
        }
    }

    /// <summary>
    /// 好きなコマを追加するgiveコマンドの定義
    /// give [コマンド名]
    /// </summary>
    void GiveCommand(string[] args){
        if(args.Length <= 1) return;

        PieceType pieceType = PieceType.Fool;
        switch(args[1]){
            case "magician":
                pieceType = PieceType.Magician;
                break;
            case "high-pirestess":
                pieceType = PieceType.HighPriestess;
                break;
            case "empress":
                pieceType = PieceType.Empress;
                break;
            case "emperor":
                pieceType = PieceType.Emperor;
                break;
            case "hierophant":
                pieceType = PieceType.Hierophant;
                break;
            case "lovers":
                pieceType = PieceType.Lovers;
                break;
            case "chariot":
                pieceType = PieceType.Chariot;
                break;
            case "strength":
                pieceType = PieceType.Strength;
                break;
            case "wheel-of-fortune":
                pieceType = PieceType.WheelOfFortune;
                break;
            case "justice":
                pieceType = PieceType.Justice;
                break;
            case "death":
                pieceType = PieceType.Death;
                break;
            case "hermit":
                pieceType = PieceType.Hermit;
                break;
            case "hanged-man":
                pieceType = PieceType.HangedMan;
                break;
            case "temeprance":
                pieceType = PieceType.Temperance;
                break;
            case "tower":
                pieceType = PieceType.Tower;
                break;
            case "star":
                pieceType = PieceType.Star;
                break;
            case "moon":
                pieceType = PieceType.Moon;
                break;
            case "sun":
                pieceType = PieceType.Sun;
                break;
            case "judgement":
                pieceType = PieceType.Judgement;
                break;
            case "world":
                pieceType = PieceType.World;
                break;
            case "devil":
                pieceType = PieceType.Devil;
                break;
            case "fool":
                pieceType = PieceType.Fool;
                break;
            default:
                return;
        }
        
        GameManager.singleton.GetLocalPlayerObject().AddHand(pieceType);
    }
}
