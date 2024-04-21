using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public GameObject receiverObject;
    public Transform handle;
    public float turnRadius;

    private bool on;
    private Quaternion onPos;
    private Quaternion offPos;
    private ILeverInteractable receiver;

    // Start is called before the first frame update
    void Start()
    {
        on = false;
        onPos = Quaternion.Euler(0, 90, -90 - turnRadius);
        offPos = Quaternion.Euler(0, 90, -90 + turnRadius);

        handle.localRotation = offPos;

        receiver = receiverObject.GetComponent<ILeverInteractable>();
        if (receiver == null)
        {
            Debug.LogWarning("The receiver does not implement ILeverInteractable!", this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        on = !on;
        if (on)
        {
            handle.localRotation = onPos;
        } else
        {
            handle.localRotation = offPos;
        }

        Debug.Log("Lever received collision");
        receiver.LeverToggle(on);
    }
}
