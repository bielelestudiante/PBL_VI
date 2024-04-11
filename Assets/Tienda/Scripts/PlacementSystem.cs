using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject mouseIndicator, cellIndicator;
    [SerializeField]
    private InputManager inputManager;
    [SerializeField]
    private Grid grid;

    public GameObject prefabToSpawn;

    [SerializeField]
    private ObjectDatabaseSO Database_tienda;
    private int ObjectToSpawnIndex = 1;

    private List<int> generatedNumbers = new List<int>();   
    private void Update()
    {
        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);
        mouseIndicator.transform.position = mousePosition;
        cellIndicator.transform.position = grid.CellToWorld(gridPosition);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < 5; i++)
            {
                int rand;
                do
                {
                    rand = Random.Range(1, 6);
                } 
                while (generatedNumbers.Contains(rand));

                generatedNumbers.Add(rand);              
            }
            PlaceObjectsDown(generatedNumbers);
            generatedNumbers.Clear();
        }
    }

    private void PlaceObjectsDown(List<int> generatedNumbers)
    {
        int positionList = 0;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                if(i == 0 && j == 0)
                {

                }
                else
                {
                    //GameObject gameObject = Instantiate(Database_tienda.objectsData[generatedNumbers[positionList]].Prefab);
                    
                    
                    Debug.Log(generatedNumbers[positionList]);
                    Debug.Log(i + " " + j);
                    positionList++;
                }
            }
        }
    }
}
