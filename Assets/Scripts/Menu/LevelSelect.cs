using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject LS;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClick1() {
        SceneManager.LoadScene("Test1Jacob");
    }
    public void OnClick2() {
        SceneManager.LoadScene("Level 2");
    }
    public void OnClickBack() {
        LS.SetActive(false);
    }
}
