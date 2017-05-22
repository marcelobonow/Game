using UnityEngine.Networking;
using UnityEngine;
using System.Collections.Generic;

public class GameManager : NetworkBehaviour
{
    public static string playerclass;
    public GameObject Soldiergo, Snipergo, Occultistgo;
    private NetworkManager networkManager;
    public GameObject playerGameObject;
    private void Start()
    {
        networkManager = NetworkManager.singleton;

        if (playerclass == null)
        {
            playerclass = "Occultist";
        }
        if (playerclass.CompareTo("ToggleSoldier") == 0)
        {
            PlayerDataPush.playerclass = "Soldier";
        }
        else if (playerclass.CompareTo("ToggleSniper") == 0)
        {
            PlayerDataPush.playerclass = "Sniper";
        }
        else
        {
            PlayerDataPush.playerclass = "Occultist";
        }
    }
}