using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newTile : MonoBehaviour
{
    public NewPlayer newPlayer;
    void Start()
    {
        newPlayer.grid.Add(gameObject);
    }
}
