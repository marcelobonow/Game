using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System.Collections.Generic;

public class PlayerDataPush : MonoBehaviour {

    public DataManager datamanager;
    private Firebase.Database.DatabaseReference db;
    private string key;
    private Vector3 lastTransform;

    void Start () {
        datamanager = GameObject.Find("Game Manager").GetComponent<DataManager>();
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://unity-project-34538.firebaseio.com/");
        db = FirebaseDatabase.DefaultInstance.RootReference.Database.GetReference("Users");
        key = datamanager.keysession;
        db = db.Child(key);
	}
	
	void FixedUpdate ()
    {
        if (lastTransform.x != transform.position.x ||
            lastTransform.y != transform.position.y || 
            lastTransform.z != transform.position.z)
        {
            lastTransform = transform.position;
            db.Child("x").SetValueAsync(transform.position.x);
            db.Child("y").SetValueAsync(transform.position.y);
            db.Child("z").SetValueAsync(transform.position.z);
        }
	}

    private void OnApplicationQuit()
    {
        db.RemoveValueAsync();
        Application.Quit();
    }

}
