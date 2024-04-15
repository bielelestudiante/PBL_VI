using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lootbag : MonoBehaviour
{
    public GameObject droppedItemPrefab;
    public List<Loot> lootList = new List<Loot>();
    
    Loot GetDroppedItem()
    {
        int randomNumber = Random.Range(1, 101); // 1 - 100
        List<Loot> possibleItems = new List<Loot>();

        foreach (Loot item in lootList)
        {
            if (randomNumber <= item.dropChance)
            {
                possibleItems.Add(item);
            }
        }

        if (possibleItems.Count > 0)
        {
            // Selecciona un objeto aleatorio de la lista de objetos posibles
            return possibleItems[Random.Range(0, possibleItems.Count)];
        }
        else
        {
            Debug.Log("No loot dropped");
            return null;
        }
    }

    public void InstantiateLoot(Vector3 spawnPosition)
    {
        Loot droppedItem = GetDroppedItem();
        if (droppedItem != null)
        {
            GameObject lootGameObject = Instantiate(droppedItemPrefab, spawnPosition, Quaternion.identity);

            // Accede al componente de renderizado del objeto 3D y establece su modelo
            lootGameObject.GetComponent<MeshRenderer>().material.mainTexture = droppedItem.lootObject.GetComponent<MeshRenderer>().material.mainTexture;

            float dropForce = 300f;
            Vector3 dropDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)); // Modificamos para objetos 3D
            lootGameObject.GetComponent<Rigidbody>().AddForce(dropDirection * dropForce, ForceMode.Impulse); // Modificamos para objetos 3D
        }
    }
}
