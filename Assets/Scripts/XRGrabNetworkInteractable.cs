using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Photon.Pun;

public class XRGrabNetworkInteractable : XRGrabInteractable
{
    private PhotonView _photonView;

    private void Start()
    {
        _photonView = GetComponent<PhotonView>();
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {   
        _photonView.RequestOwnership();
        base.OnSelectEntered(args); 
    }
}
