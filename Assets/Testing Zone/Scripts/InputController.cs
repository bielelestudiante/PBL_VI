using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{

    private Vector2 _inputMovement;
    public Vector2 InputMove { get { return _inputMovement; } }

    private void OnMove(InputValue input)
    {
        _inputMovement = input.Get<Vector2>();

    }
}
