using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{
    private PowerOutput source;
    private PowerInput dest; 

    private bool isPowered;

    public void SetPowerState(bool isPowered)
    {
        this.isPowered = isPowered;
    }

    public bool GetPowerState()
    {
        return isPowered;
    }
    
    public void CreateWire(PowerOutput source, PowerInput dest)
    {
        this.source = source;
        this.dest = dest;
        
        this.source.AddWire(this);
        this.dest.AddWire(this);
    }

    public void DestroyWire()
    {
        source.RemoveWire(this);
        dest.RemoveWire();
        
        //destroy game obj 
    }

    public int GetDestPowerDrain()
    {
        if (dest.GetOutputState())
        {
            return dest.gameObject.GetComponent<PowerOutput>().GetPowerDrain();
        }
        else
        {
            return 0; 
            //add check if devices is drawing power. 
        }
    }
}
