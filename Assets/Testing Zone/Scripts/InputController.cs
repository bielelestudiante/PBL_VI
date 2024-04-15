using System.Collections;
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
    [SerializeField] float dashDistance = 5f; // Distancia del dash
    [SerializeField] float dashCooldown = 1f; // Tiempo de espera para volver a dashear
    [SerializeField] float dashDuration = 0.2f; // Espera del dash en segundos

    CharacterController characterController;
    Camera mainCamera;

    Vector3 velocity; // Vector de velocidad para la gravedad
    bool isDashing = false; // Bandera para controlar si se está realizando un dash
    private bool isMousePressed = false;
    private bool canRotate = true; // Indica si el personaje puede rotar hacia la dirección del ratón

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Move");
        dashAction = playerInput.actions.FindAction("Dash");
        characterController = GetComponent<CharacterController>();
        mainCamera = Camera.main; // Obtenemos la cámara principal
        moveAction.Enable();
        dashAction.Enable();
    }

    IEnumerator CooldownRotation()
    {
        canRotate = false; // El personaje no puede rotar
        yield return new WaitForSeconds(3f); // Espera 3 segundos
        canRotate = true; // El personaje puede rotar nuevamente
    }
    void Update()
    {
        MovePlayer();

        // Verifica si se ha presionado el botón de dash y realiza el dash si es así
        if (dashAction.triggered && !isDashing)
        {
            StartCoroutine(Dash());
        }

        // Si el botón izquierdo del mouse está presionado y si el personaje puede rotar
        if (Mouse.current.leftButton.isPressed && !isMousePressed && canRotate)
        {
            isMousePressed = true;
            RotatePlayerTowardsMouse(); // Llama a la función para que el personaje mire hacia el ratón
            StartCoroutine(CooldownRotation()); // Inicia la corrutina de tiempo de espera
        }

        // Restablece la variable isMousePressed cuando se suelta el botón izquierdo del mouse
        if (!Mouse.current.leftButton.isPressed && isMousePressed)
        {
            isMousePressed = false;
        }
    }

    void RotatePlayerTowardsMouse()
    {
        // Obtiene la posición del ratón en la pantalla
        Vector3 mousePosition = Mouse.current.position.ReadValue();

        // Convierte la posición del ratón de la pantalla a un rayo en el espacio del juego
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        // Si el rayo intersecta el plano del suelo, calcula la longitud del rayo
        if (groundPlane.Raycast(ray, out rayLength))
        {
            // Obtiene el punto de intersección del rayo con el plano
            Vector3 pointToLook = ray.GetPoint(rayLength);

            // Calcula la dirección desde el personaje hacia el punto de intersección
            Vector3 lookDirection = pointToLook - transform.position;
            lookDirection.y = 0; // Asegura que el personaje no mire hacia arriba o abajo

            // Si la dirección es válida (no es el vector cero)
            if (lookDirection.sqrMagnitude > 0.001f)
            {
                // Invierte la dirección de rotación para que el personaje mire correctamente
                Quaternion lookRotation = Quaternion.LookRotation(-lookDirection);
                transform.rotation = lookRotation;
                Debug.DrawLine(transform.position, transform.position + lookDirection.normalized * 3, Color.blue);
            }
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
