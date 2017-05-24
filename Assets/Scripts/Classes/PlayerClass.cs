using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine;

public class PlayerClass : NetworkBehaviour {

    [SerializeField]
    Camera maincamera;
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
    [SyncVar]
    [SerializeField]
    private bool _isDead = false;
    public bool isDead
    {
        get { return _isDead; }
        protected set { _isDead = value;}
    }
    [SerializeField]
    private GameObject player;


    private void Start()
    {

        networkManager = NetworkManager.singleton;
        if (isLocalPlayer)
        {
            maincamera = Camera.main;
            SetDefault();
            playerclass = GameManager.playerclass;
            hostGame = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<HostGame>();
        }
    }
    public override void OnStartClient()
    {
        string _netID = GetComponent<NetworkIdentity>().netId.ToString();
        PlayerClass _player = GetComponent<PlayerClass>();
        GameManager.RegisterPlayer(_netID, _player);
    }

    public void SetDefault()
    {
        isDead = false;
        player.SetActive(true);
        maincamera.transform.SetParent(gameObject.transform);
        maincamera.transform.position = new Vector3(gameObject.transform.position.x, 10f, gameObject.transform.position.z);
        maincamera.orthographicSize = range * 1.5f;
        GetComponent<MoveBehaviour>().enabled = true;
        CmdChangeNickName(HostGame.playerName);
        nick.text = _nickName;
        health = maxHealth;
    }
    public void TakeDamage(int amount)
    {
        if (isDead)
            return;
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
        isDead = true;
        maincamera.transform.SetParent(gameObject.transform.parent);
        player.SetActive(false);
        GetComponent<MoveBehaviour>().enabled = false;
        StartCoroutine(Respawn());
        Debug.Log("morreu");//remover a camera do parent
                            //respawnar
    }
    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(2);
        SetDefault();
        transform.position = networkManager.GetStartPosition().position;
        transform.rotation = networkManager.GetStartPosition().rotation;
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
