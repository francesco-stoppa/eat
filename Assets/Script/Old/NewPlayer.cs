using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewPlayer : MonoBehaviour
{
    public bool full = false;

    // Player
    public GameObject body;
    public GameObject face;
    public GameObject mouth;
    public int move = 0;


    public bool canMove = true;
    public GameObject moveArea;
    public string lastInput;
    public GameObject winArea;

    public GameObject map;
    // EatableObject
    public GameObject cube;
    public GameObject eatArea;

    // Manager
    public List<GameObject> grid = new List<GameObject>();

    private void Start()
    {
        foreach(Transform child in map.transform)
        {
            grid.Add(child.gameObject);
        }
    }
    void Update()
    {
        CheckMove();
        Status();
        GiveAnInput();
        Split();
        Restart();
    }
    void Restart()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Debug.Log("Restart!");
        }
    }
    void CheckMove()
    {
        move = 0;
        foreach (GameObject tile in grid)
        {
            if (moveArea.transform.position == tile.transform.position)
            move++;
        }
        if (moveArea.transform.position == cube.transform.position)
            move++;
        if (move > 0)
        {
            canMove = true;
        }
        else // if (move == 0)
            canMove = false;

        if (eatArea.transform.position == cube.transform.position && cube.active == true)
            canMove = false;

    }
    void GiveAnInput()
    {
        // Move
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.eulerAngles = Vector3.down * -90;
            lastInput = "Up";
            CheckMove();
            if (canMove == true)
            { transform.position += Vector3.right; canMove = false; }
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.eulerAngles = Vector3.zero;
            lastInput = "Left";
            CheckMove();
            if (winArea.transform.position + new Vector3(0, -1,-1) == transform.position && full == true)
                canMove = false;

            if (canMove == true)
            {
                transform.position += Vector3.forward;
                canMove = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.eulerAngles = Vector3.down * 90;
            lastInput = "Down";
            CheckMove();
            if (canMove == true)
            { transform.position += Vector3.left; canMove = false; }
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.eulerAngles = Vector3.up * -180;
            lastInput = "Right";
            CheckMove();
            if (canMove == true)
            { transform.position += Vector3.back; canMove = false;  }
        }
    }
    void Status()
    {
        if (full == true)
        {
            body.transform.localScale = new Vector3(1.5f, 1, 1.5f);
            face.transform.localScale = new Vector3(1, 1, 1.5f);
        }
        else if (full == false)
        {
            body.transform.localScale = new Vector3(1, 1, 1);
            face.transform.localScale = new Vector3(1, 1, 1f);
        }

        if (transform.position + new Vector3(0, 1, 0) == winArea.transform.position || Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Debug.Log("YOU WIN!");
        }
    }

    void Split()
    {
        // Eat
        if (Input.GetKeyDown(KeyCode.Space) && !full)
        {
            mouth.transform.localScale = new Vector3(0.05f, 0.05f, 0.5f);
            if (eatArea.transform.position == cube.transform.position)
            {
                cube.SetActive(false);
                cube.transform.position += Vector3.up;
                mouth.transform.localScale = new Vector3(0.05f, 0.05f, 0.02f);
                full = true;
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space) && !full)
        {
            mouth.transform.localScale = new Vector3(0.05f, 0.05f, 0.02f);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && full)
        {
            if (lastInput == "Up" || lastInput == "Down" || lastInput == "Right" || lastInput == "Left")
            {
                if (canMove == true) //  && moveArea.transform.position != winArea.transform.position - Vector3.up)
                {
                    cube.transform.position = eatArea.transform.position;
                    cube.SetActive(true);
                    full = false;
                }
                else if (canMove == false)
                {
                    moveArea.transform.parent = null;
                    if (moveArea.transform.position != winArea.transform.position - Vector3.up)
                    {
                        cube.transform.position = moveArea.transform.position;
                        cube.SetActive(true);
                        full = false;
                    }
                    moveArea.transform.parent = transform;
                }
            }
        }
    }
}
