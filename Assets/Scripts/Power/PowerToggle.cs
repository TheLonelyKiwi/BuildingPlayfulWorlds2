using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerToggle : MonoBehaviour
{
    private bool receivingPower;

    private int devicesConnected;
    private int maxDevices; 

    public void UpdateInput(bool powerstate, int devices, int maxDevice)
    {
        receivingPower = powerstate;
        devicesConnected = devices;
        maxDevices = maxDevice;
        
        if (!powerstate)
        {
            UpdateOutputPower(powerstate);
        }
    }

    public void UpdateOutputPower(bool powerstate)
    {
        //
    }
}
