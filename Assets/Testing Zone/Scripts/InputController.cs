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
    [SerializeField] float dashDistance = 5f; // Distancia del dash
    [SerializeField] float dashCooldown = 1f; // Tiempo de espera para volver a dashear
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

        // Calcula el incremento de velocidad por paso del dash
        float dashIncrement = dashDistance / (dashDuration / Time.deltaTime);


        // Aplica el impulso al jugador en incrementos pequeños durante la duración del dash
        for (float t = 0; t < dashDuration; t += Time.deltaTime)
        {
            Vector3 dashVelocity = dashDirection * dashIncrement;
            characterController.Move(dashVelocity * Time.deltaTime);
            yield return null;
        }

        // Restaura la velocidad original del jugador
        speed = originalSpeed;

        // Espera el tiempo de cooldown antes de permitir otro dash
        yield return new WaitForSeconds(dashCooldown);

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

