using Firebase;
using Firebase.Unity.Editor;
using Firebase.Database;
using UnityEngine;
using System.Collections.Generic;

public class DataManager : MonoBehaviour {

    private float lastUpdate;
    private static Firebase.Database.DatabaseReference db;
    public string key;
    public GameObject player;

    void Awake()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://unity-project-34538.firebaseio.com/");
        db = FirebaseDatabase.DefaultInstance.RootReference.Database.GetReference("Users");
        key = db.Push().Key;

    }
    //we need a way to get every game object and his key and update their position
    //and, in time to time, compare the list of firebase with the list of players.
}
