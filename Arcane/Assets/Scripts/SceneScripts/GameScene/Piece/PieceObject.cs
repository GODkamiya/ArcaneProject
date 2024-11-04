using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using TMPro;
using UnityEngine;

public abstract class PieceObject : NetworkBehaviour
{
    public abstract String GetName();

    public void RenderName(){
        GetComponentInChildren<TextMeshPro>().text = GetName();
    }
}
