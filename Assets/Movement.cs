using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if  (Input.GetKeyDown(KeyCode.W))
        {
            player.transform.position += new Vector3(1, 0, 0);    
        }
    }
}
