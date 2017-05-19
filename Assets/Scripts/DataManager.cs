using Firebase;
using Firebase.Unity.Editor;
using Firebase.Database;
using UnityEngine;
using System.Collections.Generic;

public class DataManager : MonoBehaviour {

    public static string keySession;
    public static DatabaseReference database;

    void Awake()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://unity-project-34538.firebaseio.com/");
        database = FirebaseDatabase.DefaultInstance.RootReference.Child("Users");
        keySession = UIManager.keySession;
        if (keySession == null)
        {
            keySession = database.Push().Key;
        }
    }
    private void OnApplicationQuit()
    {
        database.Child(keySession).RemoveValueAsync();                  //removes the node in the database
        Application.Quit();
    }

}
