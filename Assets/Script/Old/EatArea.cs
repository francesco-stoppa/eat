using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EatArea : MonoBehaviour
{
    public GameObject player;
    public bool full = false;

    private void Awake()
    {
        player = GetComponent<GameObject>();    
    }
    // Update is called once per frame
    void Update()
    {
        if (full == true)
            player.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);

        // Split
        if (Input.GetKeyDown(KeyCode.Space) && full == true)
        {
            player.transform.localScale = new Vector3(1, 1, 1);
            full = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Finish" && Input.GetKeyDown(KeyCode.Space))
        {
            full = true;
            Destroy(other.gameObject);
        }    
    }
}
