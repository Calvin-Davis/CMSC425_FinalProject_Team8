using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : MonoBehaviour
{
    // Start is called before the first frame update
    public bool flip = false;
    CharacterController player;
    [SerializeField] public float _speed = 5;
    void Start()
    {
        
    }
    public bool getFlip() {
        return flip;
    }
    public float getSpeed() {
        return _speed;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("q")){
            flip = !flip;
        }
    }
}
