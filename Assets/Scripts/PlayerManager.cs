using UnityEngine.Networking;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : NetworkBehaviour {

    public List<Transform> spawnPoints;
    [SyncVar]
    [SerializeField]
    private int nextSpawnPoint;
    public Text nickName;
    [SerializeField]
    private Rigidbody rb;

    public int NextSpawnPoint
    {
        get
        {
            return nextSpawnPoint;
        }

        set
        {
            nextSpawnPoint = value > 3 ? 0 : value;
        }
    }

    private void Start()
    {
        if (spawnPoints == null)
        {
            GameObject[] temp = GameObject.FindGameObjectsWithTag("Spawn Point");
            foreach (GameObject _startPosition in temp)
            {
                spawnPoints.Add(_startPosition.transform);
            }
        }
        if(rb==null)
        { 
        rb = gameObject.GetComponent<Rigidbody>();
        }
    }

    public override void OnStartClient()
    {
        rb.transform.position = spawnPoints[NextSpawnPoint].transform.position;
        rb.transform.rotation = spawnPoints[NextSpawnPoint].transform.rotation;
        NextSpawnPoint++;
    }
}
