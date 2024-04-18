using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveOnSignal : MonoBehaviour, ISignalReceiver
{
    public void respondToSignal(int i)
    {
        this.gameObject.SetActive(true);
    }
}
