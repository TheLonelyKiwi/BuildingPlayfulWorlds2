using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using UnityEngine.Splines;

/*Draws spline curves around objects where wires have to be generated. Uses unity spline addon. 
*kind thanks to Laurens Lancel for helping me with this and sharing some code from a personal project. 
*/
public class WireSpline : MonoBehaviour
{
    [SerializeField] private LayerMask ignoreLayer;
    [SerializeField] private float smoothingAngle = 0.5f;
    
    private SplineContainer spline;

    [SerializeField] private Transform source, dest;
    private Transform sourceGroundPoint; //only used when wire is drawn from player. 
    
    private Vector3 sourceStart, destStart;
    [SerializeField] private bool isPlayerWire;
    private void Start()
    {
        spline = GetComponent<SplineContainer>();
    }
    
    private void Update()
    {
        FindStartingPositions();
        
        RaycastHit[] hits = Physics.RaycastAll(source.position, dest.position - source.position, Mathf.Infinity);
        int posCount = FindPositionCount(hits); 
        Vector3[] positions = FindVectorPositions(posCount, hits);
        
        UpdateSpline(positions);
    }

    private void FindStartingPositions()
    {
        Vector3 sPos = source.position;
        Vector3 dPos = dest.position;

        sourceStart = sPos + (dPos - sPos).normalized;
        destStart = dPos + (sPos - dPos).normalized;
    }

    private int FindPositionCount(RaycastHit[] cast)
    {
        int posCount = 1; 
        
        
        if (sourceGroundPoint != null)
        {
            posCount++;
        }
        
        if (cast.Length > 0)
        {
            posCount += cast.Length;
        }

        return posCount;
    }

    private Vector3[] FindVectorPositions(int positionCount, RaycastHit[] hits)
    {
        Vector3[] positions = new Vector3[positionCount];
        
        positions[0] = sourceStart;
        positions[^1] = destStart;

        int pointsFromIndex = 1; 
        
        if (isPlayerWire) //check if drawn from player
        {
            positions[^2] = sourceGroundPoint.position;
            pointsFromIndex = 2;
        }
        
        for(int i = 1; i < positionCount - pointsFromIndex; i++)
        {
            Vector3 center = hits[i - 1].collider.gameObject.transform.position;
            
            Vector3 bisector = (positions[i - 1].normalized + positions[i + 1].normalized).normalized * smoothingAngle;
            positions[i] = center + bisector * 5;
        }
        
        /*sourceStart = (positions[1] - sourceStart).normalized;
        if (isPlayerWire)
        {
            destStart = (sourceStart - positions[^3]).normalized;
        }
        else
        {
            destStart = (sourceStart - positions[^2]).normalized;
        } */

        return positions;
    }
    private void UpdateSpline(Vector3[] positions)
    {
        spline.Spline.Clear();

        foreach (Vector3 point in positions)
        {
            var bezier = new BezierKnot(point);
            spline.Spline.Add(bezier, TangentMode.AutoSmooth);
        }

        spline.Spline.GetCurve(1);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(source.position, dest.position - source.position);
    }
}
