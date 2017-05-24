using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.Networking.Match;
using System.Collections.Generic;

public class GameManager : NetworkBehaviour
{
    private const string PLAYER_ID_PREFIX = "Player";
    public static string playerclass;
    public GameObject Soldiergo, Snipergo, Occultistgo;
    public GameObject pauseMenu;
    private NetworkManager networkManager;
    private static Dictionary<string, PlayerClass> players = new Dictionary<string, PlayerClass>();

    public static void RegisterPlayer(string _netID, PlayerClass _player)
    {
        string _playerID = PLAYER_ID_PREFIX + _netID;
        players.Add(_playerID, _player);
        _player.transform.name = _playerID;
    }
    public static PlayerClass GetPlayer(string _playerID)
    {
        return players[_playerID];
    }

    private void Start()
    {
        networkManager = NetworkManager.singleton;
        if (playerclass == null)
        {
            playerclass = "Occultist";
        }
        if (playerclass.CompareTo("ToggleSoldier") == 0)
        {
            playerclass = "Soldier";
        }
        else if (playerclass.CompareTo("ToggleSniper") == 0)
        {
            playerclass = "Sniper";
        }
        else
        {
            playerclass = "Occultist";
        }
        PauseMenu.IsOn = false;
        pauseMenu.SetActive(false);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
            PauseMenu.IsOn = pauseMenu.activeSelf;
        }
    }
    public void LeaveRoom()
    {
        JoinGame.JoiningGame = false;
        HostGame.creatingRoom = false;
        MatchInfo matchInfo = networkManager.matchInfo;
        networkManager.matchMaker.DropConnection(matchInfo.networkId, matchInfo.nodeId, 0, networkManager.OnDropConnection);
        networkManager.StopClient();
    }
    private void OnApplicationQuit()
    {
        LeaveRoom();
        Application.Quit();
    }
}