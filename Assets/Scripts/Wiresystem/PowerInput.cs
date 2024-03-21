using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerInput : MonoBehaviour
{
    private Wire wire;

    private bool hasOutput; 
    
    private bool isPowered;
    private bool isConnected = false;
    
    private void WireUpdate()
    {
        if (isConnected)
        {
            if (wire.GetPowerState()) //sets output side of distribution block to false. 
            {
                SetPowerState(true);
            }
            else
            {
                SetPowerState(false);
            }
        }
        else
        {
            SetPowerState(false);
        }
    }
    
    public void AddWire(Wire wire)
    {
        if (!isConnected)
        {
            isConnected = true;
            this.wire = wire; 
        }
        WireUpdate();
    }
    
    public void RemoveWire()
    {
        isConnected = false;
        this.wire = null;
        WireUpdate();
    }
    
    public bool GetOutputState()
    {
        return hasOutput;
    }
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
