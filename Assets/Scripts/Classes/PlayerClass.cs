using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine;

public class PlayerClass : NetworkBehaviour {

    public float speed = 10f;
    public float range = 6f;
    public float firerate = 10f;
    [SyncVar]
    public string _nickName;
    public Text nick;
    public static string playerclass;
    public HostGame hostGame;
    public int counter;
    public int teste;

    public void Start()
    {
        if (isLocalPlayer)
        {
            GetComponent<MoveBehaviour>().enabled = true;
            playerclass = GameManager.playerclass;
            gameObject.name = "Player";
            hostGame = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<HostGame>();
            CmdChangeNickName(hostGame.playerName);
            nick.text = _nickName;
        }
    }
    private void FixedUpdate()
    {
        nick.text = _nickName;
    }
    [Command]
    void CmdChangeNickName(string _value)
    {
        _nickName = _value;
    }
}
