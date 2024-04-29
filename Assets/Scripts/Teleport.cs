using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public float x = 0;
    [SerializeField] public float y = 1;
    [SerializeField] public float z = 0;

    public bool useTransform = false;

    public Transform destination;

    public AudioSource teleSound;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        teleSound.PlayDelayed(0);
        
        if (!useTransform)
        {
            other.transform.position = new Vector3(x, y, z);
        } else
        {
            other.transform.position = destination.position;
        }
    }
}
