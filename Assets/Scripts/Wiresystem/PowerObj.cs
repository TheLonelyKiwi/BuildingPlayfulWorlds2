using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//just a script for copying stuff from. Items in this script are used on all powersource, powerpoints and powerblocks
abstract public class PowerObj : MonoBehaviour
{

    public bool isPowered;
    public bool isConnected;
    
    private void WireUpdate() {}

    public void SetPowerState(bool isPowered)
    {
        this.isPowered = isPowered;
    }

    public bool GetPowerState()
    {
        return this.isPowered;
    }

    public void SetConnected(bool isConnected)
    {
        this.isConnected = isConnected;
    }

    public bool GetConnectedState()
    {
        return this.isConnected;
    }
}
