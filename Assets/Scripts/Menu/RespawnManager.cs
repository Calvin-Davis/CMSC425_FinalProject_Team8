using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnManager : MonoBehaviour
{
    
    public void OnClickMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }
    //Note uses player stats to determine what level player just died on
    public void OnClickRespawn() {
        Debug.Log("Respawning");
        SceneManager.LoadScene(PlayerStats.ScenePlayerDiedOn);
        
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
