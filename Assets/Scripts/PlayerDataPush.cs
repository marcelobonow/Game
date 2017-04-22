using UnityEngine;
using System.Collections.Generic;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class PlayerDataPush : MonoBehaviour {

    public static string Nickname;
    private Vector3 lastTransform;
    private string keySession;
    private Dictionary<string, object> data = new Dictionary<string, object>();
    private int counter;

    void Start () {
        counter = 0;
        data.Add("x", transform.position.x);
        data.Add("y", transform.position.y);
        data.Add("z", transform.position.z);
        data.Add("Nickname", Nickname);
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://unity-project-34538.firebaseio.com/");
        keySession = UIManager.keySession;
        UIManager.database.Child(keySession).UpdateChildrenAsync(data);
	}
	
	void FixedUpdate ()
    {
        if (counter > 2)
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
                Debug.Log("escrevendo: " + transform.position.x);
                UIManager.database.Child(keySession).UpdateChildrenAsync(data);

            }
        }
        counter++;
	}

}
