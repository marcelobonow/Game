using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;

public class HostGame : MonoBehaviour
{

    private uint roomSize = 4;
    private string roomName;
    public static NetworkManager networkManager;
    public string playerName;

    void Start()
    {
        networkManager = NetworkManager.singleton;
        if (networkManager.matchMaker == null)
        {
            networkManager.StartMatchMaker();
        }
    }

    public void SetRoomName(string _RoomName)
    {
        roomName = _RoomName;
    }
    public void SetPlayerName(string _name)
    {
        playerName = _name;
    }

    public void CreateRoom()
    {
        if (roomName != "" && roomName != null && playerName != "" && playerName != null)
        {
            networkManager.matchMaker.CreateMatch(roomName, roomSize, true, "", "", "", 0, 0, networkManager.OnMatchCreate);
        }
        else
        {
            Debug.Log("erro");
        }
    }
}
