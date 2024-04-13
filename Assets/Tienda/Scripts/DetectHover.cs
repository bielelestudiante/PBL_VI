using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectHover : MonoBehaviour
{
    private Camera mainCamera;
    private Renderer myRenderer;
    private Color originalColor;

    private void Start()
    {
        mainCamera = Camera.main;
        myRenderer = GetComponent<Renderer>();
        originalColor = myRenderer.material.color;
    }
    private void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                ChangeColorOnHover();
                Debug.Log("Hovering over object with raycast!");
            }
            else
            {
                myRenderer.material.color = originalColor;
            }
        }
    }

    private void ChangeColorOnHover()
    {
        myRenderer.material.color = Color.yellow;
    }
}
