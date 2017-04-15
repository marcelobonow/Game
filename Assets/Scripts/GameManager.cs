using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static string playerclass;
    public GameObject Soldiergo, Snipergo, Occultistgo;

    private void Start()
    {
        if (playerclass.CompareTo("ToggleSoldier") == 0)
        {
            Instantiate(Soldiergo).AddComponent<MoveBehaviour>();
        }
        else if (playerclass.CompareTo("ToggleSniper") == 0)
        {
            Instantiate(Snipergo).AddComponent<MoveBehaviour>();
        }
        else
        {
            Instantiate(Occultistgo).AddComponent<MoveBehaviour>();
        }
    }
}