using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Player1 : MonoBehaviour
{
    // lettura input
    Pp playerInput;

    void Awake()
    {
        playerInput = new Pp();
    }

    private void Start()
    {
        
    }

    void ProcessMove(Vector2 input)
    {
        Vector3 MoveDirection = new Vector3(input.normalized.x, 0, input.normalized.y);
        transform.position += MoveDirection.normalized;
    }
    
    // controllo della griglia per il movimento
    // mangiare il cubo (con controllo del cubo)
    // sputo del cubo (controllo se c'è un dislivello)
    #region PlayerInput
    private void OnEnable()
    {
        playerInput.Enable();
    }
    private void OnDisable()
    {
        playerInput.Disable();
    }
    #endregion
}