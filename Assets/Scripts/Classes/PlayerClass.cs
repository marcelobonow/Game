using UnityEngine.Networking.Match;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine;

public class PlayerClass : NetworkBehaviour {

    public float speed = 10f;
    public float range = 6f;
    public float firerate = 10f;
    public int maxHealth = 30;
    [SyncVar]
    public int health;
    public int damage = 2;
    [SyncVar]
    public string _nickName;
    public Text nick;
    public static string playerclass;
    public HostGame hostGame;
    private NetworkManager networkManager;

    private void Start()
    {
        networkManager = NetworkManager.singleton;
        if (isLocalPlayer)
        {
            GetComponent<MoveBehaviour>().enabled = true;
            playerclass = GameManager.playerclass;
            gameObject.name = "Player";
            hostGame = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<HostGame>();
            CmdChangeNickName(HostGame.playerName);
            nick.text = _nickName;
        }
        transform.name = "Player" + GetComponent<NetworkIdentity>().netId.Value.ToString();//
    }
    public override void OnStartClient()
    {
        string _netID = GetComponent<NetworkIdentity>().netId.ToString();
        PlayerClass _player = GetComponent<PlayerClass>();
        GameManager.RegisterPlayer(_netID, _player);
    }
    private void Awake()
    {
        SetDefault();
    }
    public void SetDefault()
    {
        health = maxHealth;
    }
    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log(transform.name + " Has " + health + " health");
        if(health<=0)
        { 
            health = 0;
            Die();
        }
    }
    private void Die()
    {
        Destroy(gameObject);
        Debug.Log("morreu");//remover a camera do parent
                            //respawnar
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
