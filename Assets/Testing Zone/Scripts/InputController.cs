using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    PlayerInput playerInput;
    InputAction moveAction;
    InputAction dashAction;

    [SerializeField] float speed = 5f;
    [SerializeField] float gravity = 9.81f; // Gravedad en m/s^2
    [SerializeField] float forceMultiplier = 0.1f; // Multiplicador de fuerza
    [SerializeField] float dashSpeed = 20f; // Velocidad del dash
    [SerializeField] float dashDuration = 0.2f; // Espera del dash en segundos

    CharacterController characterController;

    Vector3 velocity; // Vector de velocidad para la gravedad
    bool isDashing = false; // Bandera para controlar si se está realizando un dash

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Move");
        dashAction = playerInput.actions.FindAction("Dash");
        characterController = GetComponent<CharacterController>();
        moveAction.Enable();
        dashAction.Enable();
    }

    void Update()
    {
        MovePlayer();

        // Verifica si se ha presionado el botón de dash y realiza el dash si es así
        if (dashAction.triggered && !isDashing)
        {
            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        isDashing = true;

        // Guarda la velocidad actual del jugador para restaurarla después del dash
        float originalSpeed = speed;

        // Establece la dirección del dash hacia adelante
        Vector3 dashDirection = characterController.velocity.normalized;

        // Calcula la velocidad del dash
        Vector3 dashVelocity = dashDirection * dashSpeed;

        // Aplica el impulso al jugador
        characterController.Move(dashVelocity * Time.deltaTime);

        // Espera la duración del dash
        yield return new WaitForSeconds(dashDuration);

        // Restaura la velocidad original del jugador
        speed = originalSpeed;

        isDashing = false;
    }

    void MovePlayer()
    {
        Vector2 direction = moveAction.ReadValue<Vector2>();
        Vector3 moveDirection = new Vector3(direction.x, 0, direction.y);

        moveDirection = transform.TransformDirection(moveDirection);
        characterController.Move(moveDirection * speed * Time.deltaTime);

        ApplyGravity();
    }

    void ApplyGravity()
    {
        if (!characterController.isGrounded)
        {
            velocity.y -= gravity * Time.deltaTime;
            characterController.Move(velocity * Time.deltaTime);
        }
        else
        {
            velocity.y = 0f;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rigidbody = hit.collider.attachedRigidbody;
        if (rigidbody != null && !rigidbody.isKinematic)
        {
            Vector3 force = hit.moveDirection * speed * forceMultiplier;
            rigidbody.AddForce(force, ForceMode.Acceleration);
        }
    }

    void OnDisable()
    {
        moveAction.Disable();
        dashAction.Disable();
    }

    void OnDestroy()
    {
        moveAction.Disable();
        dashAction.Disable();
    }
}

