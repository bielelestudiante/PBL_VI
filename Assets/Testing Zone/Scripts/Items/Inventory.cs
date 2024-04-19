using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<GameObject> inventoryList = new List<GameObject>();

    // Método para agregar un objeto al inventario
    public void AddToInventory(GameObject item)
    {
        inventoryList.Add(item);
        Debug.Log("Item added to inventory: " + item.name);
        item.transform.parent = transform; // Para mantener el objeto dentro del inventario
        item.SetActive(false); // Desactivar el objeto en el inventario
    }
}
