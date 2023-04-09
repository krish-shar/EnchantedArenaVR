using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.InputSystem.LowLevel;

[System.Serializable]
public class DefaultRoom_
{
    public string Name;
    public int SceneIndex;
    public int MaxPlayers;
}

public class ManagingNetworkTest : MonoBehaviourPunCallbacks
{
    
    public List<DefaultRoom_> DefaultRooms;
    public GameObject roomUI;
    public GameObject connectingText;
    
    public void ConnectToServer()
    {
        connectingText.SetActive(true);
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Trying to connect to server...");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to server!");
        base.OnConnectedToMaster();
        PhotonNetwork.JoinLobby();
    }

    public void InitializeRoom(int index)
    {
        DefaultRoom_ roomSettings = DefaultRooms[index];
        
        PhotonNetwork.LoadLevel(roomSettings.SceneIndex);
        
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = (byte)roomSettings.MaxPlayers;
        roomOptions.IsVisible = true;
        roomOptions.IsOpen = true;

        PhotonNetwork.JoinOrCreateRoom(roomSettings.Name, roomOptions, TypedLobby.Default);
    }
    
    public override void OnJoinedLobby()
    {
        connectingText.SetActive(false);
        roomUI.SetActive(true);
        base.OnJoinedLobby();
        Debug.Log("Joined Lobby");
    }
    
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("Joined Room");
       
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("Player entered room");
        base.OnPlayerEnteredRoom(newPlayer);
    }
}
