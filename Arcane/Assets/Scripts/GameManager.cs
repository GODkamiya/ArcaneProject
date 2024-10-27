using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    BoardManager boardManager;
    [SerializeField]
    GameObject piecePrefab;
    // Start is called before the first frame update
    void Start()
    {
        boardManager.SetBoard();
        boardManager.SetPiece(Instantiate(piecePrefab),5,5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
