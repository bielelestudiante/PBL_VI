using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lootbag : MonoBehaviour
{
    public List<GameObject> lootPrefabList = new List<GameObject>();

    void Start()
    {
        // Aqu� puedes configurar manualmente la lista de prefabs de bot�n si lo deseas
        // lootPrefabList.Add(prefab1);
        // lootPrefabList.Add(prefab2);
        // ...
    }

    GameObject GetDroppedItem()
    {
        int randomNumber = Random.Range(1, 101); // Generar un n�mero aleatorio entre 1 y 100

        int prefabIndex; // Variable para almacenar el �ndice del prefab de bot�n seleccionado

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
            default: // Para n�meros mayores a 52, no aparecer� nada
                return null;
        }

        // Retornar el prefab de bot�n seleccionado
        return lootPrefabList[prefabIndex];
    }

    public void InstantiateLoot(Vector3 spawnPosition)
    {
        GameObject droppedItemPrefab = GetDroppedItem();

        if (droppedItemPrefab != null)
        {
            Instantiate(droppedItemPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("No loot prefab to instantiate.");
        }
    }

    private void Die()
    {
        InstantiateLoot(transform.position); // Instanciar el bot�n en la posici�n del enemigo
        Destroy(gameObject); // Destruir el objeto que contiene el script Lootbag
    }
}
