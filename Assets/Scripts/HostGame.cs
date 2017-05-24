using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;

public class HostGame : MonoBehaviour
{

    private uint roomSize = 4;
    private string roomName;
    public NetworkManager networkManager;
    public string playerName;
    public static bool creatingRoom;
    public GameObject NicknameText, RoomNameText,HostButton,RefreshButton;
    [SerializeField]
    private Text Status;

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
        if (!JoinGame.JoiningGame && !creatingRoom)
        {
            if (roomName != "" && roomName != null && playerName != "" && playerName != null)
            {
                Status.text = "Creating room...";
                creatingRoom = true;
                HostButton.SetActive(false);
                NicknameText.SetActive(false);
                RoomNameText.SetActive(false);
                RefreshButton.SetActive(false);
                networkManager.matchMaker.CreateMatch(roomName, roomSize, true, "", "", "", 0, 0, networkManager.OnMatchCreate);
            }
            else
            {
                Debug.Log("erro");
            }
        }
    }
}
