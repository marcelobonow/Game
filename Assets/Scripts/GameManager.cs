using UnityEngine;

public class GameManager : MonoBehaviour {

    public static string playerclass;
    public GameObject Soldiergo, Snipergo, Occultistgo;

    private void Start()
    {
        GameObject temp;

        if (playerclass == null)
        {
            playerclass = "Occultist";
        }
        if (playerclass.CompareTo("ToggleSoldier") == 0)
        {
            PlayerDataPush.playerclass = "Soldier";
            temp = Instantiate(Soldiergo);
        }
        else if (playerclass.CompareTo("ToggleSniper") == 0)
        {
            PlayerDataPush.playerclass = "Sniper";
            temp = Instantiate(Snipergo);
        }
        else
        {
            PlayerDataPush.playerclass = "Occultist";
            temp = Instantiate(Occultistgo);
        }
        temp.GetComponent<MoveBehaviour>().enabled = true;
        temp.name = "Player";
        
    }
}