using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerOutput : MonoBehaviour
{
    private List<Wire> wires; //list of all the wires connected

    public int connectionPoints = 3; //amount of sockets on the powerblock
    private bool isConnected = false; 
    private bool isPowered;

    private void Start()
    {
        wires = new List<Wire>();
    }

    private void WireUpdate() //custom update
    {
        if (isConnected)
        {
            foreach (Wire i in wires)
            {
                i.SetPowerState(isPowered);
            }
        }
    }
    
    public void AddWire(Wire wire)
    {
        isConnected = true;
        if (wires.Count <= connectionPoints)
        {
            wires.Add(wire);
        }
        WireUpdate();
    }

    public void RemoveWire(Wire wire)
    {
        wires.Remove(wire);
        if (wires.Count == 0)
        {
            isConnected = false;
        }
        
        WireUpdate();
    }

    public bool CheckIfAvailable() //check if there are free sockets available 
    {
        if (wires.Count == connectionPoints)
        {
            return false; 
        }
        else
        {
            return true; 
        }
    }

    public int GetPowerDrain() //part of system to check if max power drain isn't being exceeded. 
    {
        int drain = 0;
        foreach (Wire i in wires)
        {
            drain += i.GetDestPowerDrain();
        }
        return drain;
    }

    public void SetPowerState(bool isPowered)
    {
        this.isPowered = isPowered;
        WireUpdate();
    }
    
    public bool GetPowerState()
    {
        return this.isPowered;
    }
}


