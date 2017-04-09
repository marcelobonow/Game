using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class PlayerDataPush : MonoBehaviour {

    private Firebase.Database.DatabaseReference playerDataBase;
    private Vector3 lastTransform;
    private string key;
    public DataManager datamanager;

    void Start () {
        datamanager = GameObject.Find("Game Manager").GetComponent<DataManager>();
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://unity-project-34538.firebaseio.com/");
        playerDataBase = FirebaseDatabase.DefaultInstance.RootReference.Database.GetReference("Users");
        key = datamanager.keysession;
        playerDataBase = playerDataBase.Child(key); //playerDataBase reference to the player node on database
	}
	
	void FixedUpdate ()
    {
        if (lastTransform.x != transform.position.x ||
            lastTransform.y != transform.position.y || 
            lastTransform.z != transform.position.z)        //if the position of the player changes, it sends the new
                                                            //position to the database
        {
            lastTransform = transform.position;
            playerDataBase.Child("x").SetValueAsync(transform.position.x);
            playerDataBase.Child("y").SetValueAsync(transform.position.y);
            playerDataBase.Child("z").SetValueAsync(transform.position.z);
        }
	}

    private void OnApplicationQuit()
    {
        playerDataBase.RemoveValueAsync();                  //removes the node in the database
        Application.Quit();
    }

}
