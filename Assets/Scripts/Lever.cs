using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public GameObject receiverObject;

    private bool on;
    private GameObject offLever;
    private GameObject onLever;
    private ILeverInteractable receiver;

    // Start is called before the first frame update
    void Start()
    {
        on = false;
        offLever = this.gameObject.transform.GetChild(0).gameObject;
        onLever = this.gameObject.transform.GetChild(1).gameObject;
        offLever.SetActive(true);
        onLever.SetActive(false);

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
        offLever.SetActive(!on);
        onLever.SetActive(on);

        Debug.Log("Lever received collision");
        receiver.LeverToggle(on);
    }
}
