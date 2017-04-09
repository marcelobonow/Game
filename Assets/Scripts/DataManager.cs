using Firebase;
using Firebase.Unity.Editor;
using Firebase.Database;
using UnityEngine;
using System.Collections.Generic;

public class DataManager : MonoBehaviour {

    private float lastUpdate;
    private static Firebase.Database.DatabaseReference db;
    private GameObject temp;
    public Dictionary<string, GameObject> playersDictionary = new Dictionary<string, GameObject>();
    public List<string> keys;
    public string keysession;
    public GameObject playerPrefab;

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
                    GameObject temp = GameObject.Instantiate(playerPrefab); //Instantiate a GameObject
                    playersDictionary.Add(data.Key, temp);                  //Add on dictionary 
                    //Set the transform.position of the Game object instantiated to be that of the database
                    temp.transform.position = new Vector3(float.Parse(data.Child("x").Value.ToString()),
                                              float.Parse (data.Child("y").Value.ToString()),
                                              float.Parse (data.Child("z").Value.ToString()));
                }
            }

        });
    }
	
	void Update () {
        if (lastUpdate > 0.1 && playersDictionary.Keys != null) //check if there is other players and some time has passed
        {
            foreach (string playerkey in playersDictionary.Keys)
            {
                Firebase.Database.DatabaseReference playerdatabase = FirebaseDatabase. //set a variable to reference the data of player's position
                                                                   DefaultInstance.RootReference.Database
                                                                   .GetReference("Users").Child(playerkey);
                playerdatabase.GetValueAsync().ContinueWith(task =>
                {
                    if (task.IsCompleted)
                    {
                        var Result = task.Result;
                        //changes the position of the other player;
                        playersDictionary[playerkey].transform.position = new Vector3(float.Parse (Result.Child("x").Value.ToString()),
                                                                          float.Parse(Result.Child("y").Value.ToString()),
                                                                          float.Parse(Result.Child("z").Value.ToString()));
                        }
                    });
                }
            }
        lastUpdate += Time.deltaTime;
    }

}
