using UnityEngine.Networking;
using System.Collections;
using UnityEngine;

public class PlayerClass : MonoBehaviour {

    public float speed = 10f;
    public float range = 6f;
    public float firerate = 10f;
    public static string nickname;
    protected string keySession = DataManager.keySession;
    public static string playerclass;

    public NetworkManager networkManager;

    private void Start()
    {
        networkManager = NetworkManager.singleton;
        if (networkManager.matchMaker == null)
        {
            networkManager.StartMatchMaker();
        }
        playerclass = GameManager.playerclass;
        name = "Player";
        GetComponent<MoveBehaviour>().enabled = true;
    }
}
