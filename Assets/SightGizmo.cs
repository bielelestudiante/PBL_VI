using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightGizmo : MonoBehaviour
{
    public float sightRadius = 5.0f;
    public int segments = 32;
    public Color gizmoColor = Color.red;

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;

        Vector3 center = transform.position;
        float angle = 0;
        float step = Mathf.PI * 2 / segments;

        for (int i = 0; i < segments; i++)
        {
            Vector3 prevPoint = new Vector3(Mathf.Cos(angle) * sightRadius, 0, Mathf.Sin(angle) * sightRadius);
            angle += step;
            Vector3 curPoint = new Vector3(Mathf.Cos(angle) * sightRadius, 0, Mathf.Sin(angle) * sightRadius);

            Gizmos.DrawLine(center + prevPoint, center + curPoint);
        }
    }
}
