using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISignalReceiver
{
    //used to play cutscenes/load scenes in certain order
    public void respondToSignal(int i);
}
