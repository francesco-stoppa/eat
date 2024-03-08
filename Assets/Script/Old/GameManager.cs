using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject map;
    public int rows;
    public int columns;
    public Tile tile;
    public Tile[,] grid;

    // Win condition
    public Vector3 winPath;
    public GameObject winColumns;

    // Eatable object in game
    public List<GameObject> eatableObj = new List<GameObject>();

    // new
    public GameObject button;
    public GameObject wall;

    private void Awake()
    {
        // Create grid
        grid = new Tile[columns, rows];
        for (int i = 0; i<= columns-1; i++)
        {
            for(int j = 0; j <= rows-1; j++)
            {
                 grid[i,j] = Instantiate<Tile>(tile, new Vector3(i, 0, j), new Quaternion(), map.gameObject.transform);
                if (grid[i, j] == grid[columns - 1, rows - 1])
                {
                    grid[i, j] = Instantiate<Tile>(tile, new Vector3(columns - 1, 0, rows), new Quaternion(), map.gameObject.transform);
                    winColumns.transform.position = new Vector3(columns - 1, 1, rows);
                    winPath = new Vector3(columns - 1, 0, rows-1);
                }
            }
        }
    }
    private void Start()
    {
        if (button != null)
        {
            grid[(int)button.transform.position.x, (int)button.transform.position.z].contained = button;
            grid[columns - 1, rows - 1].contained = wall;
            wall.transform.position = new Vector3(columns - 1, 1, rows - 0.75f);
        }
    }
    private void Update()
    {
        if (button != null)
        {
            if (grid[(int)button.transform.position.x, (int)button.transform.position.z].contained != button)
            {
                grid[columns - 1, rows - 1].contained = null;
                wall.transform.position = new Vector3(columns - 1, 0, rows - 1);
            }
            else
                grid[(int)button.transform.position.x, (int)button.transform.position.z].contained = button;
        }
    }
}
