using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Loot", menuName = "Loot")]
public class Loot : ScriptableObject
{
    public int id; // ID único para el loot
    public string lootName;
    public GameObject lootObject;
    public int dropChance;

    // Constructor para inicializar el loot con su nombre y probabilidad de drop
    public Loot(string lootName, int dropChance)
    {
        this.lootName = lootName;
        this.dropChance = dropChance;
    }
}
