using UnityEngine.Networking;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : NetworkBehaviour {

    public List<Transform> spawnPoints;
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
        if (rb == null)
        {
            rb = gameObject.GetComponent<Rigidbody>();
        }
        if(isClient)
        {
        StartCoroutine(Spawn(2));
        //rb.transform.position = spawnPoints[NextSpawnPoint].transform.position;
        //rb.transform.rotation = spawnPoints[NextSpawnPoint].transform.rotation;
        }
    }
    IEnumerator Spawn(int time)
    {
        CmdSetSpawnPoint();
        Debug.Log(time);
        yield return new WaitForSeconds(time);
        rb.transform.rotation = spawnPoints[NextSpawnPoint].transform.rotation;
        rb.transform.position = spawnPoints[NextSpawnPoint].transform.position;
    }
    [Command]
    public void CmdSetSpawnPoint()
    {
        NextSpawnPoint++;
        RpcSetSpawnPoint(NextSpawnPoint);
    }
    [ClientRpc]
    public void RpcSetSpawnPoint(int _value)
    {
        Debug.Log(NextSpawnPoint);
        NextSpawnPoint = _value;
    }

}
