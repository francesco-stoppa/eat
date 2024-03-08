using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxcast : MonoBehaviour
{
    float m_MaxDistance;
    float m_Speed;
    bool m_HitDetect;
    RaycastHit m_Hit;

    void Start()
    {
        //Choose the distance the Box can reach to
        m_MaxDistance = 1;
    }

    

    void FixedUpdate()
    {
        //Test to see if there is a hit using a BoxCast
        //Calculate using the center of the GameObject's Collider(could also just use the GameObject's position), half the GameObject's size, the direction, the GameObject's rotation, and the maximum distance as variables.
        //Also fetch the hit data
        m_HitDetect = Physics.BoxCast(Vector3.one + Vector3.up, transform.localScale/2, transform.forward / 2, out m_Hit, transform.rotation, m_MaxDistance);
        if (m_HitDetect)
        {
            //Output the name of the Collider your Box hit
            Debug.Log("Hit : " + m_Hit.collider.name);
        }
    }

    //Draw the BoxCast as a gizmo to show where it currently is testing. Click the Gizmos button to see this
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Vector3.one + Vector3.up, transform.localScale / 2);
    }
}
