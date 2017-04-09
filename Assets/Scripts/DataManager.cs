using Firebase;
using Firebase.Unity.Editor;
using Firebase.Database;
using UnityEngine;
using System.Collections.Generic;

public class DataManager : MonoBehaviour {

    private float lastUpdate;
    private static Firebase.Database.DatabaseReference db;
    public Dictionary<string, GameObject> players;
    public List<string> keys;
    public string keysession;
    public GameObject player;

    void Awake () {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://unity-project-34538.firebaseio.com/");
        db = FirebaseDatabase.DefaultInstance.RootReference.Database.GetReference("Users");
        keysession = db.Push().Key;
        db.GetValueAsync().ContinueWith(task =>
        {
            foreach(var data in task.Result.Children)
            {
                if (data.Key != keysession)
                {
                    players.Add(data.Key, GameObject.Instantiate(player));
                    Debug.Log(data.Key);
                }
            }
        });
    }
	
	//I am too sleepy now to understand anything, tomorrow I redo everything
	void Update () {
        if (lastUpdate > 0.1)
        {
            foreach (string playerkey in players.Keys)
            {
                if (playerkey != keysession)
                {
                    Firebase.Database.DatabaseReference playerdatabase = FirebaseDatabase.
                                                                         DefaultInstance.RootReference.Database
                                                                         .GetReference("users").Child(playerkey);
                    playerdatabase.GetValueAsync().ContinueWith(task =>
                    {
                        if (task.IsCompleted)
                        {
                            var test = task.Result;
                            players[playerkey].transform.position = new Vector3((float)test.Child("x").Value,
                                                                    (float)test.Child("y").Value,
                                                                    (float)test.Child("z").Value);
                        }
                    });
                }
            }
        }
    }

}
