using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lootbag : MonoBehaviour
{
    public List<Loot> lootList = new List<Loot>(); // Lista de loot disponibles
    public Inventory inventory;

    private Collider lootCollider;

    void Start()
    {
        lootCollider = GetComponent<Collider>();
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
        if (other.CompareTag("Player"))
        {
            Vector3 spawnPosition = transform.position + new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f));
            InstantiateLoot(spawnPosition);
            lootCollider.enabled = false;
        }
    }

    Loot GetDroppedItem()
    {
        int randomNumber = Random.Range(1, 101);

        Loot selectedLoot = null;

        foreach (Loot loot in lootList)
        {
            if (randomNumber <= loot.dropChance)
            {
                selectedLoot = loot;
                break;
            }
        }

        return selectedLoot;
    }

    public void InstantiateLoot(Vector3 spawnPosition)
    {
        Loot droppedLoot = GetDroppedItem();

        if (droppedLoot != null && droppedLoot.lootObject != null)
        {
            GameObject droppedItem = Instantiate(droppedLoot.lootObject, spawnPosition, Quaternion.identity);
            
            // Agregar el ítem al inventario
            inventory.AddItemToInventory(droppedLoot);

            // Destruir el objeto instanciado (lootbag)
            Destroy(gameObject);
        }
        else
        {
            Debug.LogWarning("No loot to instantiate.");
        }
    }
}
