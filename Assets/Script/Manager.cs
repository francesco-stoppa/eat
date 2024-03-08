using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    // Singelton 
    public static Manager Instance { get; private set; }

    public List<GameObject> grid = new List<GameObject>();

    [Header("Game Manager")]
    [SerializeField] GameObject player;
    [SerializeField] GameObject map;
    [SerializeField] GameObject winColumns;
    [SerializeField] GameObject[] whiteCube;
    void Awake()
    {
        #region Singelton
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        #endregion 
    }
    void Start()
    {
        foreach (Transform child in map.transform)
        {
            grid.Add(child.gameObject);
        }
    }
    public GameObject GetMap() { return map; }
    public GameObject GetWinColumns() { return winColumns; }
    public List<GameObject> GetGrid() { return grid; }
    public GameObject[] GetWhiteCube() { return whiteCube; }
}
