using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnManager : MonoBehaviour
{
    public string level = "MainMenu";
    public void UpdateRespawnLevel(string levelname) {
        level = levelname;
    }
    public void OnClickMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }
    public void OnClickRespawn() {
        SceneManager.LoadScene("Test1Jacob");
        
    }
    public void OnClickQuit() {
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
