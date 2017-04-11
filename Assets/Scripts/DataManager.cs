using Firebase;
using Firebase.Unity.Editor;
using Firebase.Database;
using UnityEngine;
using System.Collections.Generic;

public class DataManager : MonoBehaviour {

    private static Firebase.Database.DatabaseReference UsersReference;
    public Dictionary<string, GameObject> playersDictionary = new Dictionary<string, GameObject>();
    public string keysession;
    public GameObject playerPrefab;

    void Awake () {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://unity-project-34538.firebaseio.com/");
        UsersReference = FirebaseDatabase.DefaultInstance.RootReference.Database.GetReference("Users");
        keysession = UsersReference.Push().Key;
        UsersReference.ChildAdded += UsersReference_ChildAdded;
        UsersReference.ChildRemoved += UsersReference_ChildRemoved;
        UsersReference.ChildChanged += UsersReference_ChildChanged;
    }

    private void UsersReference_ChildChanged(object sender, ChildChangedEventArgs e)
    {
        if (e.Snapshot.Key.CompareTo(keysession)!=0)
        {
            playersDictionary[e.Snapshot.Key].transform.position = new Vector3(float.Parse(e.Snapshot.Child("x").Value.ToString()),
                                                  float.Parse(e.Snapshot.Child("y").Value.ToString()),
                                                  float.Parse(e.Snapshot.Child("z").Value.ToString()));
        }
    }

    private void UsersReference_ChildRemoved(object sender, ChildChangedEventArgs e)
    {
        Destroy(playersDictionary[e.Snapshot.Key]);
        playersDictionary.Remove(e.Snapshot.Key);
    }


    private void UsersReference_ChildAdded(object sender, ChildChangedEventArgs e)
    {
        if(e.Snapshot.Key.CompareTo(keysession) != 0)
        {
            GameObject temp = GameObject.Instantiate(playerPrefab);
            temp.transform.position = new Vector3(float.Parse(e.Snapshot.Child("x").Value.ToString()),
                                                  float.Parse(e.Snapshot.Child("y").Value.ToString()),
                                                  float.Parse(e.Snapshot.Child("z").Value.ToString()));
            playersDictionary.Add(e.Snapshot.Key, temp);
        }
    }


}
