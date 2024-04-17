using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchLoot : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que ha colisionado tiene la etiqueta "loot"
        if (other.CompareTag("Loot"))
        {
            // Hacer que el objeto "loot" desaparezca
            Destroy(other.gameObject);
        }
    }
}
