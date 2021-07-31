using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public static bool collided = false;
    

    void OnCollisionEnter(Collision other)
    {

        Debug.Log("Collider entered : " + other.gameObject.name);
        if (other.gameObject.CompareTag("object"))
        {
            collided = true;
            Rigidbody rbdy = other.gameObject.GetComponent<Rigidbody>();
            
            //Stop Moving/Translating
            rbdy.velocity = Vector3.zero;
           
            //Stop rotating
            rbdy.angularVelocity = Vector3.zero;
            Debug.Log("collided" + collided);
        }
    }

    void OnCollisionExit(Collision col)
    {
        Debug.Log("Collider exited : " + col.gameObject.name);
        if (col.gameObject.CompareTag("object"))
        {
            collided = false;
        }

        Debug.Log(collided);
    }
}
