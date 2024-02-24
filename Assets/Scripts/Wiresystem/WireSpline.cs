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
    [SerializeField] private GameObject splineTester; 
    [SerializeField] private LayerMask ignoreLayer;
    [SerializeField] private float smoothingAngle = 0.5f;
    [SerializeField] private float bisectorDodgeAmount = 5f;
    [SerializeField] private bool isPlayerWire;
    
    private SplineContainer spline;

    [SerializeField] private Transform source, dest;
    private Transform sourceGroundPoint; //only used when wire is drawn from player. 
    
    private Vector3 sourceStart, destStart;
    
    private int positionCount;
    private RaycastHit[] hits;
    private void Start()
    {
        spline = GetComponent<SplineContainer>();
        
        GenerateSpline();
    }
    
    private void GenerateSpline()
    {
        FindStartingPositions();
        
        hits = Physics.RaycastAll(source.position, dest.position - source.position, Mathf.Infinity);
        positionCount = FindPositionCount(); 
        Vector3[] positions = FindVectorPositions();
        
        UpdateSpline(positions);

        GameObject tester = Instantiate(splineTester, transform.position, quaternion.identity);
        tester.GetComponent<SplineScan>().RunRider(this, spline, ignoreLayer);
    }

    private void FindStartingPositions()
    {
        Vector3 sPos = source.position;
        Vector3 dPos = dest.position;

        sourceStart = sPos + (dPos - sPos).normalized;
        destStart = dPos + (sPos - dPos).normalized;
    }

    private int FindPositionCount()
    {
        int posCount = 1; 
        
        
        if (sourceGroundPoint != null)
        {
            posCount++;
        }
        
        if (hits.Length > 0)
        {
            posCount += hits.Length;
        }

        return posCount;
    }

    private Vector3[] FindVectorPositions()
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
            positions[i] = center + bisector * bisectorDodgeAmount;
        }
        return positions;
    }

    public void GenerateNewPositions(Vector3[] newPoints)
    {
        int posCount = positionCount + newPoints.Length;
        int pointsFromIndex = 1; 
        
        Vector3[] positions = new Vector3[posCount];
        
        positions[0] = sourceStart;
        positions[^1] = destStart;
        
        if (isPlayerWire) //check if drawn from player
        {
            positions[^2] = sourceGroundPoint.position;
            pointsFromIndex = 2;
        }

        for (int i = 1; i < posCount - pointsFromIndex; i++)
        {
            Vector3 center = hits[i - 1].collider.gameObject.transform.position;
            
            Vector3 bisector = (positions[i - 1].normalized + positions[i + 1].normalized).normalized * smoothingAngle;
            positions[i] = center + bisector * bisectorDodgeAmount;
        }
        
        UpdateSpline(positions);
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
