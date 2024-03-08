using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour
{
    public GameObject wall;
    public GameObject player;
    public GameObject cube;
    public GameObject block;
    public NewPlayer newPlayer;

    void Start()
    {
        newPlayer = player.GetComponent<NewPlayer>();
        newPlayer.grid.Remove(block);
    }
    private void Update()
    {
        if (cube.transform.position - new Vector3(0, 0.4f, 0) == transform.position || player.transform.position + new Vector3(0, 0.6f, 0) == transform.position)
        {
            wall.SetActive(false);
            if(!newPlayer.grid.Contains(block))
                newPlayer.grid.Add(block);
        }
        else
        {
            wall.SetActive(true);
            if (newPlayer.grid.Contains(block))
                newPlayer.grid.Remove(block);
        }

    }
}
