using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutletController : MonoBehaviour
{
    [SerializeField] private float triggerRadius = 3.0f;
    
    
    void Start()
    {
        gameObject.GetComponent<SphereCollider>().radius = triggerRadius;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
