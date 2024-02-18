using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PowerBlockOutput))]
public class PowerBlockInput : MonoBehaviour
{
    private Wire wire;
    private PowerBlockOutput output; 
    
    public bool isPowered;
    public bool isConnected = false;

    private void Start()
    {
        output = GetComponent<PowerBlockOutput>();
    }

    private void WireUpdate()
    {
        if (isConnected)
        {
            if (wire.GetPowerState()) //sets output side of distribution block to false. 
            {
                isPowered = true; 
                output.SetPowerState(true);
            }
            else
            {
                isPowered = false; 
                output.SetPowerState(false);
            }
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
