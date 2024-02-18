using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PowerBlockInput))]
public class PowerBlockOutput : MonoBehaviour
{
    private List<Wire> wires; //list of all the wries
    private PowerBlockInput input;

    public int connectionPoints; //amount of sockets on the powerblock
    private bool isConnected = false; 
    private bool isPowered;

    private void Start()
    {
        wires = new List<Wire>();
        input = GetComponent<PowerBlockInput>();
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


