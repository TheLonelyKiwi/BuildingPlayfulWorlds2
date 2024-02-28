using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondGridTest : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Transform movePoint;

    [SerializeField] private LayerMask obstacle;
    void Start()
    {
        movePoint.parent = null;
    }
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
        {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
            }
        }
        
        if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
        {
            if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                movePoint.position += new Vector3(0f, 0f, Input.GetAxisRaw("Vertical"));
            }
        }
    }
}
