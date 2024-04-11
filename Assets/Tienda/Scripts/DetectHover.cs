using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectHover : MonoBehaviour
{
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }
    private void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == gameObject) // Check if it's this object
            {
                // Implement hover logic (e.g., change color, display UI)
                Debug.Log("Hovering over object with raycast!");
                // ... (your hover logic here)
            }
        }
    }
}
