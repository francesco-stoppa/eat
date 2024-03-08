using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.FindAnyObjectByType<GameManager>();
    }
    void Start()
    {
        gameManager.grid[(int)transform.position.x, (int)transform.position.z].contained = this.gameObject;
        gameManager.eatableObj.Add(gameObject);
    }
    private void Update()
    {
        if (transform.position.z == -1)
            transform.position = new Vector3((int)transform.position.x, 0, (int)transform.position.z);
        else if (transform.position.x == -1)
            transform.position = new Vector3((int)transform.position.x - 1, 0, (int)transform.position.z);
        else if (transform.position.z == gameManager.rows)
            transform.position = new Vector3((int)transform.position.x, 0, (int)transform.position.z);
        else if (transform.position.x == gameManager.columns)
            transform.position = new Vector3((int)transform.position.x, 0, (int)transform.position.z);
    }
}
