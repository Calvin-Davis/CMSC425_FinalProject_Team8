using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransitionManager : MonoBehaviour
{
    public void OnClickMainMenu()
    {
        Debug.Log("Respawning");
        SceneManager.LoadScene("MainMenu");
    }
    public void OnClickNext()
    {
        Debug.Log("Going to next level");
        SceneManager.LoadScene(PlayerStats.PlayerNextLevel);

    }
    public void OnClickQuit()
    {
        Debug.Log("Respawning");
        Application.Quit();
    }
    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
