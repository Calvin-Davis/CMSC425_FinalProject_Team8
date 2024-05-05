using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCollider : MonoBehaviour
{
    public TutorialMessages controller;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Used to know when to display the tutorial messages
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "self")
        {
            controller.progress();
            Destroy(this);
        }
    }
}
