using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class JoinGame : MonoBehaviour {

    private List<GameObject> roomList = new List<GameObject>();

    private NetworkManager networkManager;

	void Start () {
        networkManager = NetworkManager.singleton;
        if (networkManager.matchMaker == null)
        {
            networkManager.StartMatchMaker();
        }
	}

    public void RefreshRoomList()
    {
        networkManager.matchMaker.ListMatches(0, 20, "", false, 0, 0, OnMatchlist);
    }

    public void OnMatchlist (bool sucess, string extendedInfo, List<MatchInfoSnapshot> matchlist)
    {
        if(!sucess)
        {
            Debug.Log("Couldn't get room list");
            return;
        }
    }
}
