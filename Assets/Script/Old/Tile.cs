using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int x { get; }
    public int z { get; }
    public GameObject contained;
    public Tile(int x, int z, GameObject contained)
    {
        this.x = x;
        this.z = z;
        this.contained = contained;
    }
}
