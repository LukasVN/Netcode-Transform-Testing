using System.Collections;
using System.Collections.Generic;
using Unity.Netcode.Components;
using UnityEngine;

public class RewindNetworkTransform : NetworkTransform
{
    private bool isServerAuthoritative;

    private void Start() {
        isServerAuthoritative = false;
    }

    protected override bool OnIsServerAuthoritative(){
        return isServerAuthoritative;
    }

    public void SetServerAuthoritative(){
        isServerAuthoritative = true;
    }

    public void SetClientAuthoritative(){
        isServerAuthoritative = false;
    }
}
