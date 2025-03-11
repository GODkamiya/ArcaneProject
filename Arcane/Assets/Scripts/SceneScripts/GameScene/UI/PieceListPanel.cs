using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PieceListPanel : MonoBehaviour
{
    [SerializeField]
    GameObject pieceButtonPrefab;

    public void Initialize(List<PieceType> pieceTypes,UnityAction<PieceType> onSelectPieceType){
        void Call(PieceType selectedPieceType){
            onSelectPieceType(selectedPieceType);
            Destroy(gameObject);
        }
        foreach(PieceType pieceType in pieceTypes){
            GameObject pieceButton = Instantiate(pieceButtonPrefab);
            pieceButton.transform.SetParent(transform);
            pieceButton.GetComponentInChildren<TextMeshProUGUI>().text = PieceTypeExtension.GetNameFromPieceType(pieceType);
            pieceButton.GetComponent<Button>().onClick.AddListener(() => Call(pieceType));
        }
    }
}
