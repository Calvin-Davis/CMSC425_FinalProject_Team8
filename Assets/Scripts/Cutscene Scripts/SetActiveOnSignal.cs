using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Sets a game object to be active when receiving a signal

// Its use in the game currently is to set the robots to appear in the intro cutscene
public class SetActiveOnSignal : MonoBehaviour, ISignalReceiver
{
    public void respondToSignal(int i)
    {
        this.gameObject.SetActive(true);
    }
}
