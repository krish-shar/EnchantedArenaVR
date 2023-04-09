using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class LeaveGame : MonoBehaviourPunCallbacks
{
    public InputHelpers.Button inputHelpers = InputHelpers.Button.MenuButton;
    public XRNode controller = XRNode.LeftHand;
    
    void Update()
    {
        InputHelpers.IsPressed(InputDevices.GetDeviceAtXRNode(controller), inputHelpers, out bool isPressed);

        if (isPressed)
        {
            PhotonNetwork.Disconnect();
            PhotonNetwork.LoadLevel(0);
        }
    }
}