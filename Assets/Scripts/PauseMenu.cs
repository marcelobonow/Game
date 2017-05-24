using UnityEngine;

public class PauseMenu : MonoBehaviour {
    public static bool IsOn;
    public GameObject pauseMenuPanel;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            IsOn = true;

        }
    }
}
