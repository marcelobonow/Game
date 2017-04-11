using UnityEngine;
using System.Collections.Generic;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class PlayerDataPush : MonoBehaviour {

    public DataManager datamanager;
    private Firebase.Database.DatabaseReference playerDataBase;
    private Vector3 lastTransform;
    private string key;
    private Dictionary<string, object> data = new Dictionary<string, object>();

    void Start () {
        data.Add("x", transform.position.x);
        data.Add("y", transform.position.y);
        data.Add("z", transform.position.z);
        datamanager = GameObject.Find("Game Manager").GetComponent<DataManager>();
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://unity-project-34538.firebaseio.com/");
        playerDataBase = FirebaseDatabase.DefaultInstance.RootReference.Database.GetReference("Users");
        key = datamanager.keysession;
        playerDataBase = playerDataBase.Child(key); //playerDataBase reference to the player node on database
        playerDataBase.UpdateChildrenAsync(data);
	}
	
	void FixedUpdate ()
    {
        if (lastTransform.x != transform.position.x ||
            lastTransform.y != transform.position.y || 
            lastTransform.z != transform.position.z)        //if the position of the player changes, it sends the new
                                                            //position to the database
        {
            lastTransform = transform.position;
            data["x"] = transform.position.x;
            data["y"] = transform.position.y;
            data["z"] = transform.position.z;
            playerDataBase.UpdateChildrenAsync(data);
        }
	}

    private void OnApplicationQuit()
    {
        playerDataBase.RemoveValueAsync();                  //removes the node in the database
        Application.Quit();
    }

}
