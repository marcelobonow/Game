using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using UnityEngine;


public class FireBaseStart : MonoBehaviour {

    private void Start()
    {
        //set the firebase database url.
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://unity-project-34538.firebaseio.com/");
    }
}
