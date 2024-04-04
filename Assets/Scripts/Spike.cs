using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Spike : MonoBehaviour
{
    // Start is called before the first frame update
    public string level;
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
            Debug.Log("Respawn");
            PlayerStats.ScenePlayerDiedOn = this.gameObject.scene.name;
            SceneManager.LoadScene("Respawn");
        }
    }
}
