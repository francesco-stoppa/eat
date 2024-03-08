using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EatArea : MonoBehaviour
{
    GameObject see;
    public GameObject GetSee() { return see; }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "eatable")
        {
            see = other.gameObject;
        }
    }
}
