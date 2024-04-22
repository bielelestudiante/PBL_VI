using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Dictionary<int, Loot> inventoryItems = new Dictionary<int, Loot>();
    private int nextItemId = 1; // Para asignar IDs únicos

    public void AddItemToInventory(Loot loot)
    {
        // Asignar un ID único al loot
        loot.id = nextItemId;
        nextItemId++;

        // Guardar el loot en el inventario usando su ID como clave
        inventoryItems.Add(loot.id, loot);

        Debug.Log("Added item to inventory: " + loot.lootName);
    }

    public void RetrieveItemFromInventory(int itemId)
    {
        if (inventoryItems.ContainsKey(itemId))
        {
            Loot retrievedLoot = inventoryItems[itemId];
            Debug.Log("Retrieved Item: " + retrievedLoot.lootName);

            // Aquí puedes hacer lo que necesites con el loot recuperado
        }
        else
        {
            Debug.LogWarning("Item with ID " + itemId + " not found in inventory.");
        }
    }
}
