using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Splines;


/*
 * Summary: runs an object over generated spline and returns a list of additonial obsctacles that have to be dealt with. 
 */
public class SplineScan : MonoBehaviour
{
    private SplineAnimate animator; //animator attached to rider obj 
    private WireSpline parentContainer; //this is the container that holds the spline.

    private List<Vector3> collidedObj;
    private LayerMask ignoreLayer; 

    public void RunRider(WireSpline parent, SplineContainer path, LayerMask ignoreLayer) 
    {
        this.ignoreLayer = ignoreLayer;
        
        //runs collider over previously generated Spline
        parentContainer = parent;
        animator.Container = path;
        gameObject.GetComponent<BoxCollider>().enabled = true; 
        animator.Play();
    }
    void Awake()
    {
        animator = GetComponent<SplineAnimate>();

        collidedObj = new List<Vector3>();
    }

    private void Update()
    {
        if (!animator.IsPlaying) //when finished moving across Spline destroy self. 
        {
            parentContainer.GenerateNewPositions(collidedObj.ToArray());
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        //adds point of impact as a new points that has to be avoided. 
        if (collider.gameObject.layer != ignoreLayer)
        {
            Debug.Log(collider.gameObject.name);
            collidedObj.Add(collider.ClosestPoint(transform.position));
        }
    }
}
