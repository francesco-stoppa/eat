using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;

public class InputPlayer : MonoBehaviour
{
    // Game Manager
    Manager gameManager;
    // Input Map
    Pp input;
    // Check Movement
    bool canMove;
    bool iFindSomething = false;
    // eat
    GameObject belly;
    // Win condition ?

    // map
    public List<GameObject> map = new List<GameObject>();


    private void Awake()
    {
        input = new Pp();
        // set input action
        input.PlayerAction.Eat.started += context => Eat();
        input.PlayerAction.Reset.started += context => Reset();
    }
    private void Start()
    {
        gameManager = Manager.Instance;
        Transform lui = gameManager.GetMap().transform;
        foreach (Transform child in lui)
        {
            map.Add(child.gameObject);
        }
    }
    void Update()
    {
        BaseInput();
    }



    // remember to move the obstacle when you eat it
    void Eat()
    {
        Vector3 nextMovePos = transform.position + Vector3.forward - Vector3.up;

        // start animation (attenzione se è full oppure no)
        if (belly == null)
        {
            belly.SetActive(false);
            belly.transform.position = Vector3.up * 2;
        }
        else if (belly != null)
        {
            belly.SetActive(true);
            belly.transform.position = nextMovePos;
        }
    }
    void Reset() { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }
    void BaseInput()
    {
        if (input.PlayerMovement.Up.triggered)
        {
            transform.eulerAngles = Vector3.down * -90;
            CheckMove();
            if (canMove == true)
            { transform.position += Vector3.right; canMove = false; }
        }
        else if (input.PlayerMovement.Left.triggered)
        {
            transform.eulerAngles = Vector3.zero;
            CheckMove();
            /*Vector3 winPos = gameManager.GetWinColumns().transform.position;
            if (winPos + new Vector3(0, -2, -1) == transform.position && belly != null)
                canMove = false;*/
            if (canMove == true)
            {
                transform.position += Vector3.forward;
                canMove = false;
            }
        }
        else if (input.PlayerMovement.Down.triggered)
        {
            transform.eulerAngles = Vector3.down * 90;
            CheckMove();
            if (canMove == true)
                transform.position += Vector3.left;
        }
        else if (input.PlayerMovement.Right.triggered)
        {
            transform.eulerAngles = Vector3.up * -180;
            CheckMove();
            if (canMove == true)
            { transform.position += Vector3.back; canMove = false; }
        }
    }
    void CheckMove()
    {
        canMove = false;
        Vector3 nextMovePos = transform.position + Vector3.down + Vector3.forward;

        /*foreach (GameObject tile in gameManager.GetGrid())
        {
            if (ball.transform.position == tile.transform.position)
                canMove = true;
        }*/
        int move = 0;
        foreach (GameObject tile in gameManager.GetGrid())
        {
            if (nextMovePos == tile.transform.position)
                move++;
        }
       
        if (move > 0)
        {
            canMove = true;
        }
        else // if (move == 0)
            canMove = false;


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
