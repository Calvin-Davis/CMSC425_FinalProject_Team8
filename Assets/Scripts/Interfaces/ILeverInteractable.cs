using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILeverInteractable
{
    //must be implemented in objects intended to interact with levels
    public void LeverToggle(bool newState);
}
