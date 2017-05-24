using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.Networking.Match;

public class GameManager : NetworkBehaviour
{
    public static string playerclass;
    public GameObject Soldiergo, Snipergo, Occultistgo;
    public GameObject pauseMenu;
    private NetworkManager networkManager;

    private void Start()
    {
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
            Debug.Log(PauseMenu.IsOn);
        }
    }
    public void LeaveRoom()
    {
        JoinGame.JoiningGame = false;
        HostGame.creatingRoom = false;
        MatchInfo matchInfo = networkManager.matchInfo;
        networkManager.matchMaker.DropConnection(matchInfo.networkId, matchInfo.nodeId, 0, networkManager.OnDropConnection);
        networkManager.StopHost();
    }
}