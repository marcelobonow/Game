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
            UIManager.database.Child(UIManager.keySession).Child("Class").SetValueAsync("Soldier");
            Instantiate(Soldiergo).AddComponent<MoveBehaviour>();
        }
        else if (playerclass.CompareTo("ToggleSniper") == 0)
        {
            UIManager.database.Child(UIManager.keySession).Child("Class").SetValueAsync("Sniper");
            Instantiate(Snipergo).AddComponent<MoveBehaviour>();
        }
        else
        {
            UIManager.database.Child(UIManager.keySession).Child("Class").SetValueAsync("Occultist");
            Instantiate(Occultistgo).AddComponent<MoveBehaviour>();
        }
    }
}