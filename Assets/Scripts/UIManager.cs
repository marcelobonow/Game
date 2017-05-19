using Firebase;
using Firebase.Unity.Editor;
using Firebase.Database;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class UIManager : MonoBehaviour {

    public static string keySession;
    public static DatabaseReference database;
    public InputField nickNameInput;
    public ToggleGroup ClassSelectGroup;
 

    void Start () {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://unity-project-34538.firebaseio.com/");
        database = FirebaseDatabase.DefaultInstance.RootReference.Child("Users");
        keySession = database.Push().Key;
    }

    public void SendData()
    {
        GameManager.playerclass = ClassSelectGroup.ActiveToggles().FirstOrDefault().name;
        PlayerDataPush.Nickname = nickNameInput.text;
        SceneManager.LoadScene(1);        
    }

    private void OnApplicationQuit()
    {
        database.Child(keySession).RemoveValueAsync();                  //removes the node in the database
        Application.Quit();
    }
}
