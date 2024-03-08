using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public bool full = false;

    // Player
    public GameObject body;
    public GameObject face;
    public GameObject eatArea;
    public GameObject mouth;

    // Manager
    public GameManager gameManager;
    public string lastInput;
    public GameObject cube;

    void Update()
    {
        Status();
        GiveAnInput();
    }
    void GiveAnInput()
    {
        // Move
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.eulerAngles = Vector3.down * -90;
            lastInput = "Up";
            if (gameManager.grid[(int)transform.position.x + 1, (int)transform.position.z].contained == null)
                transform.position += Vector3.right; 
            else return;
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.eulerAngles = Vector3.zero;
            lastInput = "Left";
            if (transform.position == gameManager.winPath && full == false || Input.GetKey(KeyCode.Escape))
            {
                transform.position += Vector3.forward;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
            }
            if (gameManager.grid[(int)transform.position.x, (int)transform.position.z + 1].contained == null)
                transform.position += Vector3.forward; 
            else return; 
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.eulerAngles = Vector3.down * 90;
            lastInput = "Down";
            if (gameManager.grid[(int)transform.position.x-1, (int)transform.position.z].contained == null)
                transform.position += Vector3.left;
            else return;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.eulerAngles = Vector3.up * -180;
            lastInput = "Right";
            if (gameManager.grid[(int)transform.position.x, (int)transform.position.z-1].contained == null)
                transform.position += Vector3.back;
            else return;
        }

        // Eat
        if (Input.GetKeyDown(KeyCode.Space) && full == false)
        {
            mouth.transform.localScale = new Vector3(0.05f, 0.05f, 0.5f);
            if (lastInput == "Up" && gameManager.grid[(int)transform.position.x+1, (int)transform.position.z].contained != null)
            {
                foreach(GameObject cube in gameManager.eatableObj)
                {
                    if (cube.transform.position == new Vector3(transform.position.x + 1, 1, transform.position.z))
                    {
                        Destroy(cube);
                        gameManager.eatableObj.Remove(cube);
                        gameManager.grid[(int)transform.position.x + 1, (int)transform.position.z].contained = null;
                        full = true;
                        mouth.transform.localScale = new Vector3(0.05f, 0.05f, 0.02f);
                    }
                }
            }
            else if (lastInput == "Down" && gameManager.grid[(int)transform.position.x - 1, (int)transform.position.z].contained != null)
            {
                foreach (GameObject cube in gameManager.eatableObj)
                {
                    if (cube.transform.position == new Vector3(transform.position.x - 1, 1, transform.position.z))
                    {
                        Destroy(cube);
                        gameManager.eatableObj.Remove(cube);
                        gameManager.grid[(int)transform.position.x - 1, (int)transform.position.z].contained = null;
                        full = true;
                        mouth.transform.localScale = new Vector3(0.05f, 0.05f, 0.02f);
                    }
                }
            }
            else if (lastInput == "Right" && gameManager.grid[(int)transform.position.x, (int)transform.position.z - 1].contained != null)
            {
                foreach (GameObject cube in gameManager.eatableObj)
                {
                    if (cube.transform.position == new Vector3(transform.position.x, 1, transform.position.z - 1))
                    {
                        Destroy(cube);
                        gameManager.eatableObj.Remove(cube);
                        gameManager.grid[(int)transform.position.x, (int)transform.position.z - 1].contained = null;
                        full = true;
                        mouth.transform.localScale = new Vector3(0.05f, 0.05f, 0.02f);
                    }
                }
            }
            else if (lastInput == "Left" && gameManager.grid[(int)transform.position.x, (int)transform.position.z + 1].contained != null)
            {
                foreach (GameObject cube in gameManager.eatableObj)
                {
                    if (cube.transform.position == new Vector3(transform.position.x, 1, transform.position.z + 1))
                    {
                        Destroy(cube);
                        gameManager.eatableObj.Remove(cube);
                        gameManager.grid[(int)transform.position.x, (int)transform.position.z + 1].contained = null;
                        full = true;
                        mouth.transform.localScale = new Vector3(0.05f, 0.05f, 0.02f);
                    }
                }
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space) && full == false)
        {
            mouth.transform.localScale = new Vector3(0.05f, 0.05f, 0.02f);
        }
        
        // Split
        if (Input.GetKeyDown(KeyCode.Space) && full == true)
        {
            if (lastInput == "Up") // && gameManager.grid[(int)transform.position.x + 1, (int)transform.position.z].contained == null)
            {
                Instantiate(cube, new Vector3((int)transform.position.x + 1, 1, (int)transform.position.z), Quaternion.identity);
                full = false;
            }
            else if (lastInput == "Down") // && gameManager.grid[(int)transform.position.x - 1, (int)transform.position.z].contained == null)
            {
                Instantiate(cube, new Vector3((int)transform.position.x - 1, 1, (int)transform.position.z), Quaternion.identity);
                full = false;
            }
            else if (lastInput == "Right") // && gameManager.grid[(int)transform.position.x, (int)transform.position.z -1].contained == null)
            {
                Instantiate(cube, new Vector3((int)transform.position.x, 1, (int)transform.position.z - 1), Quaternion.identity);
                full = false;
            }
            else if(lastInput == "Left") // && gameManager.grid[(int)transform.position.x, (int)transform.position.z +1].contained == null)
            {
                Instantiate(cube, new Vector3((int)transform.position.x, 1, (int)transform.position.z + 1), Quaternion.identity);
                full = false;
            }
        }
    }
    void Status()
    {
        if (full == true)
        {
            body.transform.localScale = new Vector3(1.5f, 1, 1.5f);
            face.transform.localScale = new Vector3(1, 1, 1.5f);

            // face.transform.position = transform.position + new Vector3(0, 0, 0.1f);
        }
        else if (full == false)
        {
            body.transform.localScale = new Vector3(1, 1, 1);
            face.transform.localScale = new Vector3(1, 1, 1f);

            // face.transform.position = transform.position;
        }
    }
}
