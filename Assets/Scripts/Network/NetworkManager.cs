/**
 using System;
 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

[System.Serializable]
public class DefaultRoom
{
    public string Name;
    public int SceneIndex;
    public int MaxPlayers;
}

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public List<DefaultRoom> DefaultRooms;
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

    
    public void InitializeRoom(int defaultRoomIndex)
    {
        Debug.Log("DefaultRoomIndex: " + defaultRoomIndex);
        DefaultRoom roomSettings = DefaultRooms[defaultRoomIndex];
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = (byte)roomSettings.MaxPlayers;
        roomOptions.IsVisible = true;
        roomOptions.IsOpen = true;
        Debug.Log("RoomSettings.Name: " + roomSettings.Name);
        PhotonNetwork.JoinOrCreateRoom(roomSettings.Name, roomOptions, TypedLobby.Default);
    }
    
    
   

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("Joined lobby!");
        connectingText.SetActive(false);
        roomUI.SetActive(true);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room!");
        base.OnJoinedRoom();
        DefaultRoom roomSettings = DefaultRooms[PhotonNetwork.CurrentRoom.PlayerCount - 1];
        PhotonNetwork.LoadLevel(roomSettings.SceneIndex);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("A new player entered room!");
        base.OnPlayerEnteredRoom(newPlayer);
    }
}
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

[System.Serializable]
public class DefaultRoom
{
    public string Name;
    public int SceneIndex;
    public int MaxPlayers;
}

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public List<DefaultRoom> DefaultRooms;
    public GameObject roomUI;
    public GameObject connectingText;

    //void Start()
    //{
    //    ConnectToServer();
    //}

    public void ConnectToServer()
    {
        connectingText.SetActive(true);
        
        Debug.Log("Trying to connect to server...");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to server!");
        base.OnConnectedToMaster();

        PhotonNetwork.JoinLobby();

    }

    public void InitializeRoom(int defaultRoomIndex)
    {
        
        Debug.Log("DefaultRoomIndex: " + defaultRoomIndex);
        DefaultRoom roomSettings = DefaultRooms[defaultRoomIndex];
        
        
        PhotonNetwork.LoadLevel(roomSettings.SceneIndex);
        
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = (byte)roomSettings.MaxPlayers;
        roomOptions.IsVisible = true;
        roomOptions.IsOpen = true;
        Debug.Log("RoomSettings.Name: " + roomSettings.Name);
        
        Debug.Log("Joining/Creating Room");
        PhotonNetwork.JoinOrCreateRoom(roomSettings.Name, roomOptions, TypedLobby.Default);
        
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("Joined lobby!");
        connectingText.SetActive(false);
        roomUI.SetActive(true);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room!");
        base.OnJoinedRoom();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("A new player entered room!");
        base.OnPlayerEnteredRoom(newPlayer);
        
    }
}
