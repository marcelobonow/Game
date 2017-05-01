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
    public static string keySession;
    public GameObject Soldiergo,Snipergo,Occultistgo;
    public static DatabaseReference database;
    private float timer, temptimer;

    void Awake () {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://unity-project-34538.firebaseio.com/");
        database = FirebaseDatabase.DefaultInstance.RootReference.Child("Users");
        keySession = UIManager.keySession;
        if (keySession == null)
        {
            keySession = database.Push().Key;
        }
        database.ChildAdded += UsersReference_ChildAdded;
        database.ChildRemoved += UsersReference_ChildRemoved;
        database.ChildChanged += UsersReference_ChildChanged;
    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        foreach (string key in buffer.Keys)
        {
            playersDictionary[key].transform.position = buffer[key];
        }
    }

    private void UsersReference_ChildChanged(object sender, ChildChangedEventArgs e)
    {
        
        if(timer > 0.2f)
        {
            if (e.Snapshot.Key.CompareTo(keySession) != 0)
            {
                buffer[e.Snapshot.Key] = new Vector3(float.Parse(e.Snapshot.Child("x").Value.ToString()),
                                                      float.Parse(e.Snapshot.Child("y").Value.ToString()),
                                                      float.Parse(e.Snapshot.Child("z").Value.ToString()));
            }
            timer = 0;
        }
        timer += Time.deltaTime;
    }

    private void UsersReference_ChildRemoved(object sender, ChildChangedEventArgs e)
    {
        Destroy(playersDictionary[e.Snapshot.Key]);
        playersDictionary.Remove(e.Snapshot.Key);
        buffer.Remove(e.Snapshot.Key);
    }


    private void UsersReference_ChildAdded(object sender, ChildChangedEventArgs e)
    {
        GameObject Temp;
        if(e.Snapshot.Key.CompareTo(keySession) != 0)
        {
            if (e.Snapshot.Child("Class").Value.ToString().CompareTo("Soldier") == 0)
            {
                Temp = GameObject.Instantiate(Soldiergo);
            }
            else if(e.Snapshot.Child("Class").Value.ToString().CompareTo("Sniper") == 0)
            {
                Temp = GameObject.Instantiate(Snipergo);
            }
            else
            {
                Temp = GameObject.Instantiate(Occultistgo);
            }
                Temp.transform.position = new Vector3(float.Parse(e.Snapshot.Child("x").Value.ToString()),
                                                  float.Parse(e.Snapshot.Child("y").Value.ToString()),
                                                  float.Parse(e.Snapshot.Child("z").Value.ToString()));
            Temp.name = e.Snapshot.Child("Nickname").Value.ToString();
            playersDictionary.Add(e.Snapshot.Key, Temp);
            buffer.Add(e.Snapshot.Key, Temp.transform.position);
        }
    }


    private void OnApplicationQuit()
    {
        database.Child(keySession).RemoveValueAsync();                  //removes the node in the database
        Application.Quit();
    }

}
