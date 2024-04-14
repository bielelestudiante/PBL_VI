using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // El transform del jugador a seguir
    public Vector3 offset;   // La distancia entre el jugador y la cámara

    void LateUpdate()
    {
        if (target != null)
        {
            // Mantener la misma posición que el jugador pero con la altura de la cámara
            transform.position = target.position + offset;
        }
    }
}

