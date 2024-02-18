using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{
    private PowerBlockOutput source; 
    //dest, probs power PowerInput? 

    private bool isPowered;

    public void SetPowerState(bool isPowered)
    {
        this.isPowered = isPowered;
    }

    public bool GetPowerState()
    {
        return isPowered;
    }
}
