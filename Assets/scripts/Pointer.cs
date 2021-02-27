using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * looking for a physic object to show user curernt things that they're pointing to
 */

public class Pointer : MonoBehaviour
{
    public float defaultLength = 3.0f; // pointer's length
    private LineRenderer pointer = null; // take an array of two and draw a line between each one
    
    void Awake()
    {
        pointer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        UpdateLength();
    }

    // set position of the pointer from our hand to the object
    private void UpdateLength()
    {
        pointer.SetPosition(0, transform.position);
        pointer.SetPosition(1, CalculateEnd());
    }

    // look for the end of the pointer
    private Vector3 CalculateEnd()
    {
        RaycastHit hit = createForwardRaycast();
        Vector3 endPosition = DefaultEnd(defaultLength);
        if (hit.collider)
        {
            endPosition = hit.point;
        }
        return endPosition;
    }

    private RaycastHit createForwardRaycast()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward); // a ray from our hand

        Physics.Raycast(ray, out hit, defaultLength); // store data from ray cast to "hit"
        return hit;
    }

    private Vector3 DefaultEnd(float length)
    {
        return transform.position + transform.forward * length;
    }

}
