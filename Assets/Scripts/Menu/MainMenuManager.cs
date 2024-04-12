using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject LevelSelect;
    public void OnClickPlay(string scene) {
        SceneManager.LoadScene(scene);
    }
    public void OnClickLS() {
        LevelSelect.SetActive(true);
    }
    public void OnClickQuit() {
        Application.Quit();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
