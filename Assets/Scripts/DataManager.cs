using Firebase;
using Firebase.Unity.Editor;
using Firebase.Database;
using UnityEngine;
using System.Collections.Generic;

public class DataManager : MonoBehaviour {

    public enum Classes
    {
        Soldier,
        Sniper,
        Occulist
    }

    private Dictionary<string, Vector3> buffer = new Dictionary<string, Vector3>();
    public Dictionary<string, GameObject> playersDictionary = new Dictionary<string, GameObject>();
    public static string keysession;
    public GameObject Soldiergo,Snipergo,Occultistgo;

    void Awake () {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://unity-project-34538.firebaseio.com/");
        keysession = UIManager.keySession;
        UIManager.database.ChildAdded += UsersReference_ChildAdded;
        UIManager.database.ChildRemoved += UsersReference_ChildRemoved;
        UIManager.database.ChildChanged += UsersReference_ChildChanged;
    }

    private void FixedUpdate()
    {
        foreach (string key in buffer.Keys)
        {
            playersDictionary[key].transform.position = buffer[key];
        }
    }

    private void UsersReference_ChildChanged(object sender, ChildChangedEventArgs e)
    {
        if (e.Snapshot.Key.CompareTo(keysession)!=0)
        {
            buffer[e.Snapshot.Key]= new Vector3(float.Parse(e.Snapshot.Child("x").Value.ToString()),
                                                  float.Parse(e.Snapshot.Child("y").Value.ToString()),
                                                  float.Parse(e.Snapshot.Child("z").Value.ToString()));
        }
    }

    private void UsersReference_ChildRemoved(object sender, ChildChangedEventArgs e)
    {
        Destroy(playersDictionary[e.Snapshot.Key]);
        playersDictionary.Remove(e.Snapshot.Key);
        buffer.Remove(e.Snapshot.Key);
    }


    private void UsersReference_ChildAdded(object sender, ChildChangedEventArgs e)
    {
        GameObject temp = new GameObject();
        if(e.Snapshot.Key.CompareTo(keysession) != 0)
        {
            if (e.Snapshot.Child("Class").Value.ToString().CompareTo("Soldier") == 0)
            {
                temp = GameObject.Instantiate(Soldiergo);
            }
            else if(e.Snapshot.Child("Class").Value.ToString().CompareTo("Sniper") == 0)
            {
                temp = GameObject.Instantiate(Snipergo);
            }
            else if(e.Snapshot.Child("Class").Value.ToString().CompareTo("Occultist") == 0)
            {
                temp = GameObject.Instantiate(Occultistgo);
            }
            temp.transform.position = new Vector3(float.Parse(e.Snapshot.Child("x").Value.ToString()),
                                                  float.Parse(e.Snapshot.Child("y").Value.ToString()),
                                                  float.Parse(e.Snapshot.Child("z").Value.ToString()));
            playersDictionary.Add(e.Snapshot.Key, temp);
            buffer.Add(e.Snapshot.Key, temp.transform.position);
        }
    }

    private void OnApplicationQuit()
    {
        UIManager.database.Child(keysession).RemoveValueAsync();                  //removes the node in the database
        Application.Quit();
    }

}
