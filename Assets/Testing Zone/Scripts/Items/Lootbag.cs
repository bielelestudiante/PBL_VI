using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lootbag : MonoBehaviour
{
    public List<GameObject> lootPrefabList = new List<GameObject>();

    // Referencia al script de Inventory
    public Inventory inventory;

    private Collider lootCollider;

    void Start()
    {
        // Obtener el collider del objeto Lootbag
        lootCollider = GetComponent<Collider>();

        // Asegurarse de que el collider esté configurado como trigger
        if (lootCollider != null)
        {
            lootCollider.isTrigger = true;
        }
        else
        {
            Debug.LogError("Lootbag collider not found.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Verificar si el collider que entra es el jugador u otro objeto que pueda recoger el botín
        if (other.CompareTag("Player"))
        {
            // Obtener una posición ligeramente aleatoria para evitar superposiciones
            Vector3 spawnPosition = transform.position + new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f));

            // Instanciar el botín y agregarlo al inventario
            InstantiateLoot(spawnPosition);

            // Desactivar el collider del Lootbag para evitar que se recoja más de una vez
            lootCollider.enabled = false;
        }
    }

    GameObject GetDroppedItem()
    {
        int randomNumber = Random.Range(1, 101); // Generar un número aleatorio entre 1 y 100

        int prefabIndex; // Variable para almacenar el índice del prefab de botín seleccionado

        // Asignar una probabilidad diferente a cada elemento de la lista usando un switch
        switch (randomNumber)
        {
            case int n when (n <= 40): // 40% de probabilidad para el elemento 0
                prefabIndex = 0;
                break;
            case int n when (n <= 43): // 3% de probabilidad para el elemento 1
                prefabIndex = 1;
                break;
            case int n when (n <= 46): // 3% de probabilidad para el elemento 2
                prefabIndex = 2;
                break;
            case int n when (n <= 49): // 3% de probabilidad para el elemento 3
                prefabIndex = 3;
                break;
            case int n when (n <= 52): // 3% de probabilidad para el elemento 4
                prefabIndex = 4;
                break;
            default: // Para números mayores a 52, no aparecerá nada
                return null;
        }

        // Retornar el prefab de botín seleccionado
        return lootPrefabList[prefabIndex];
    }

    public void InstantiateLoot(Vector3 spawnPosition)
    {
        GameObject droppedItemPrefab = GetDroppedItem();

        if (droppedItemPrefab != null)
        {
            // Instanciar el objeto de botín en la posición especificada
            GameObject droppedItem = Instantiate(droppedItemPrefab, spawnPosition, Quaternion.identity);

            // Agregar el objeto de botín al inventario
            inventory.AddToInventory(droppedItem);
        }
        else
        {
            Debug.LogWarning("No loot prefab to instantiate.");
        }
    }

}

