using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lootbag : MonoBehaviour
{
    public List<GameObject> lootPrefabList = new List<GameObject>();

    void Start()
    {
        // Aquí puedes configurar manualmente la lista de prefabs de botín si lo deseas
        // lootPrefabList.Add(prefab1);
        // lootPrefabList.Add(prefab2);
        // ...
    }

    GameObject GetDroppedItem()
    {
        if (lootPrefabList.Count > 0)
        {
            int randomIndex = Random.Range(0, lootPrefabList.Count);
            return lootPrefabList[randomIndex];
        }
        else
        {
            Debug.LogWarning("No loot prefabs available.");
            return null;
        }
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
        InstantiateLoot(transform.position); // Instanciar el botín en la posición del enemigo
        Destroy(gameObject); // Destruir el objeto que contiene el script Lootbag
    }
}
