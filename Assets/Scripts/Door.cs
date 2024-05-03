using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    // Use this to indicate the name of the scene that holds the next level
    public string nameOfNextLevel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //when player touches door send them to the the level complete screen and prepare
    //to load next level
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "self")
        {
            PlayerStats.PlayerNextLevel = nameOfNextLevel;
            SceneManager.LoadScene("LevelTransitionMenu");
        }
    }
}
