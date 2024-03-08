using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    Manager gameManager;
    // Input Map
    Pp input;

    bool full = false;

    public GameObject belly;
    public int move = 0;
    public EatArea Marea;

    public bool canMove = true;
    public GameObject moveArea;
    public Transform map;
    public string lastInput;
    public GameObject winArea;

    // public GameObject map;
    // EatableObject
    public GameObject cube;
    public GameObject eatArea;
    public EatArea area;

    // Manager
    public List<GameObject> me = new List<GameObject>();
    RaycastHit hit;

    private void Awake()
    {
        input = new Pp();

        input.PlayerAction.Eat.started += context => Eat();
        input.PlayerAction.Reset.started += context => Restart();
    }
    private void Start()
    {
        gameManager = Manager.Instance;

        foreach (Transform child in map)
        {
            me.Add(child.gameObject);
        }
    }
    void Update()
    {
        Status();
        GiveAnInput();
    }
    protected void Cast()
    {
        if (Physics.Raycast(transform.position + Vector3.up, transform.TransformDirection(Vector3.forward), out hit, 1))
        {
            Debug.DrawRay(transform.position + Vector3.up, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
            Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(transform.position + Vector3.up, transform.TransformDirection(Vector3.forward) * 1, Color.green);
            Debug.Log("Did not Hit");
        }
    }

    
    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void CheckMove()
    {
        move = 0;
        foreach (GameObject tile in me)
        {
            if (moveArea.transform.position == tile.transform.position)
                move++;
        }
        
        /*if (eatArea.transform.position == transform.position)
            move = 0;*/

        if (move > 0)
        {
            canMove = true;
        }
        else // if (move == 0)
            canMove = false;

        
    }
    void GiveAnInput()
    {
        // Move
        if (input.PlayerMovement.Up.triggered)
        {
            transform.eulerAngles = Vector3.down * -90;
            lastInput = "Up";
            CheckMove();
            Cast();
            if (canMove == true)
            { transform.position += Vector3.right;}
        }
        else if (input.PlayerMovement.Left.triggered)
        {
            transform.eulerAngles = Vector3.zero;
            lastInput = "Left";
            CheckMove();
            Cast();
            if (winArea.transform.position + new Vector3(0, -1, -1) == transform.position && full == true)
                canMove = false;

            if (canMove == true)
            {
                transform.position += Vector3.forward;
                canMove = false;
            }
        }
        else if (input.PlayerMovement.Down.triggered)
        {
            transform.eulerAngles = Vector3.down * 90;
            lastInput = "Down";
            CheckMove();
            Cast();
            if (canMove == true)
            { transform.position += Vector3.left; canMove = false; }
        }
        else if (input.PlayerMovement.Right.triggered)
        {
            transform.eulerAngles = Vector3.up * -180;   
            lastInput = "Right";
            CheckMove();
            Cast();
            if (canMove == true)
            { transform.position += Vector3.back; canMove = false; }
        }
        
    }
    void Status()
    {
        if (transform.position + new Vector3(0, 1, 0) == winArea.transform.position || Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Debug.Log("YOU WIN!");
        }
    }

    void Eat()
    {
        if (hit.collider != null)
        {
            // DOTO
        }

        /*
        // start animation (attenzione se è full oppure no)
        if (belly == null && !full)
        {
            belly = area.GetSee();
            Debug.Log(belly);
            belly.SetActive(false);
            belly.transform.position = Vector3.up * 2;
            full = true;
        }
        else if (belly != null && full)
        {
            belly.SetActive(true);
            belly.transform.position = eatArea.transform.position;
            full = false;
            belly = null;
        }*/
    }

    private void OnEnable()
    {
        input.Enable();
    }
    private void OnDisable()
    {
        input.Disable();
    }
}


