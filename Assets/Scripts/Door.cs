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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "self")
        {
            Debug.Log("Level complete");
            PlayerStats.PlayerNextLevel = nameOfNextLevel;
            SceneManager.LoadScene("LevelTransitionMenu");
        }
    }
}
