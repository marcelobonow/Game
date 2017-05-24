using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class JoinGame : MonoBehaviour {

    public static bool JoiningGame;
    public GameObject NicknameText, RoomNameText, HostButton,RefreshButton;
    private List<GameObject> roomList = new List<GameObject>();
    [SerializeField]
    private GameObject roomListItemPrefab;
    [SerializeField]
    private Transform roomListParent;
    [SerializeField]
    private Text Status;
    private NetworkManager networkManager;

	void Start () {
        networkManager = NetworkManager.singleton;
        if (NetworkManager.singleton.matchMaker == null)
        {
            NetworkManager.singleton.StartMatchMaker();
        }
        RefreshRoomList();
	}

    public void RefreshRoomList()
    {
        if(!HostGame.creatingRoom)
        {
            Status.text = "Loading...";
            ClearRoomList();
            NetworkManager.singleton.matchMaker.ListMatches(0, 10, "", true, 0, 0, OnMatchlist);
        }
    }

    public void OnMatchlist (bool sucess, string extendedInfo, List<MatchInfoSnapshot> matchList)
    {
        Status.text = "";
        if(!sucess || matchList == null)
        {
            Status.text = "Couldn't get room list";
            return;
        }
        foreach (MatchInfoSnapshot match in matchList)
        {
            GameObject _roomListItemGO = Instantiate(roomListItemPrefab);
            _roomListItemGO.transform.SetParent(roomListParent);
            _roomListItemGO.transform.localScale = new Vector3(1, 1, 1);
            RoomListItem _roomListItem = _roomListItemGO.GetComponent<RoomListItem>();
            if (_roomListItem != null)
            {
                _roomListItem.Setup(match,JoinRoom);
            }
            roomList.Add(_roomListItemGO);
        }
        if(roomList.Count == 0)
        {
            Status.text = "There are no rooms";
        }
    }
    public void ClearRoomList()
    {
        for (int i = 0; i < roomList.Count; i++)
        {
            Destroy(roomList[i]);
        }
        roomList.Clear();
    }
    public void JoinRoom(MatchInfoSnapshot _match)
    {
        if(!HostGame.creatingRoom && !JoiningGame)
        {
            JoiningGame = true;
            HostButton.SetActive(false);
            NicknameText.SetActive(false);
            RoomNameText.SetActive(false);
            RefreshButton.SetActive(false);
            Status.text = "Joining...";
            networkManager.matchMaker.JoinMatch(_match.networkId, "", "", "", 0, 0, networkManager.OnMatchJoined);
            ClearRoomList();
        }
        else
        {
            Status.text = "Wait for the room to be created";
        }
       
    }
}
