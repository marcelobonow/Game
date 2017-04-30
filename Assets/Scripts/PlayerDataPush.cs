using UnityEngine;
using System.Collections.Generic;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class PlayerDataPush : MonoBehaviour {

    public static string Nickname;
    public static string playerclass;

    private Vector3 lastTransform;
    private string keySession;
    private Dictionary<string, object> data = new Dictionary<string, object>();
    private int counter;
    public int limit = 2;

    void Start () {
        counter = 0;
        data.Add("x", transform.position.x);
        data.Add("y", transform.position.y);
        data.Add("z", transform.position.z);
        data.Add("Nickname", Nickname);
        data.Add("Class", playerclass);
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://unity-project-34538.firebaseio.com/");
        keySession = DataManager.keySession;

        DataManager.database.Child(keySession).UpdateChildrenAsync(data);
	}
	
	void Update()
    {
        if (counter > limit)
        {
            counter = 0;
            if (lastTransform.x != transform.position.x ||
                lastTransform.y != transform.position.y ||
                lastTransform.z != transform.position.z)        //if the position of the player changes, it sends the new
                                                                //position to the database
            {
                lastTransform = transform.position;
                data["x"] = transform.position.x;
                data["y"] = transform.position.y;
                data["z"] = transform.position.z;
                DataManager.database.Child(keySession).UpdateChildrenAsync(data);

            }
        }
        counter++;
	}

}
