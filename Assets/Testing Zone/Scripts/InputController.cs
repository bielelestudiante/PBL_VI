using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    PlayerInput playerInput;

    // NUEVO
    private InputAction moveAction;
    // NUEVO

    InputAction Move;

    // NUEVO
    public object InputMove { get; internal set; }
    // NUEVO

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Move");
    }

    void Uptade()
    {

    }

    void MovePlayer()
    {
        Vector2 direction = moveAction.ReadValue<Vector2>();
        transform.position += new Vector3(direction.x, 0, direction.y) * Time.deltaTime;
    }
}
