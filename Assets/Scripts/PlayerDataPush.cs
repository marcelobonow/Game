using UnityEngine;
using System.Collections.Generic;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class PlayerDataPush : MonoBehaviour {

    public static string Nickname;
    public static string playerclass;

    private string keySession;

    void Start () { 
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://unity-project-34538.firebaseio.com/");
        keySession = DataManager.keySession;
        DataManager.database.Child(keySession).Child("Class").SetValueAsync(playerclass);
        DataManager.database.Child(keySession).Child("Nickname").SetValueAsync(Nickname);
	}
}
