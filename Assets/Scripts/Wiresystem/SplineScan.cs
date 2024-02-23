using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;


/*
 * Summary: runs an object over generated spline and returns a list of additonial obsctacles that have to be dealt with. 
 */
public class SplineScan : MonoBehaviour
{
    private SplineAnimate animator; //animator attached to rider obj 
    private SplineContainer parentContainer; //this is the container that holds the spline.

    private List<Vector3> collidedObj; 

    private LayerMask ignoreLayer; 

    public void RunRider(SplineContainer parent, LayerMask ignoreLayer) 
    {
        this.ignoreLayer = ignoreLayer;
        
        parentContainer = parent;
        animator.Container.Spline = parentContainer.Splines[0];
        animator.Play();
    }

    private Vector3[] convertData() //converts list to Vector3[]
    {
        int length = collidedObj.Count;
        Vector3[] points = new Vector3[length];

        for (int i = 0; i < length; i++)
        {
            points[i] = collidedObj[i];
        }

        return points; 
    }
    void Start()
    {
        animator = GetComponent<SplineAnimate>();

        collidedObj = new List<Vector3>();
    }

    private void Update()
    {
        if (!animator.IsPlaying) //when finished moving across Spline destroy self. 
        {
            //return points list for comparisson 
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer != ignoreLayer)
        {
            collidedObj.Add(collider.gameObject.transform.position);
        }
    }
}
