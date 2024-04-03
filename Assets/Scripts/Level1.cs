using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1 : MonoBehaviour
{
    public string nameOfNextLevel;

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
