using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanishWithLever : MonoBehaviour, ILeverInteractable
{
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LeverToggle(bool newState)
    {
        this.gameObject.SetActive(newState);
    }
}
