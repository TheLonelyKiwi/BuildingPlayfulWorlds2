using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireCreator : MonoBehaviour
{
    [SerializeField] private GameObject wirePrefab;
    private Wire activeWire;
    
    List<Wire> wires = new List<Wire>();

    public void GenerateNewWire(GameObject source, GameObject target)
    {
        GameObject newBridge = Instantiate(wirePrefab, transform.position, Quaternion.identity);
        activeWire = newBridge.GetComponent<Wire>();
        activeWire.CreateWire(source.GetComponent<PowerOutput>(), target.GetComponent<PowerInput>());
        wires.Add(activeWire);
    }
}
