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
                    rand = Random.Range(0, 11);
                } 
                while (generatedNumbers.Contains(rand));

                generatedNumbers.Add(rand);
                Debug.Log(rand);
            }
            generatedNumbers.Clear();
        }
    }
}
