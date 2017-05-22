using UnityEngine.Networking;
using UnityEngine;

public class HostGame : MonoBehaviour {

    private uint roomSize = 4;
    private string roomName;
    public static NetworkManager networkManager;

    void Start () {
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

    public void CreateRoom()
    {
        if (roomName != "" && roomName != null)
        {
            networkManager.matchMaker.CreateMatch(roomName, roomSize, true, "", "", "", 0, 0, networkManager.OnMatchCreate);
        }
        else
        {
            Debug.Log("erro");
        }
    }
}
