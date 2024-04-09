using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    PlayerInput playerInput;

    // NUEVO
    InputAction moveAction;
    // NUEVO

    [SerializeField] float speed = 5f;

    CharacterController characterController;

    // NUEVO
    //public object InputMove { get; internal set; }
    // NUEVO

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Move");
        characterController = GetComponent<CharacterController>();
        moveAction.Enable();
    }

    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        Vector2 direction = moveAction.ReadValue<Vector2>();
        transform.position += new Vector3(direction.x, 0, direction.y) * speed * Time.deltaTime;

    }

    void OnDisable()
    {
        moveAction.Disable();
    }

    void OnDestroy()
    {
        moveAction.Disable();
    }
}
