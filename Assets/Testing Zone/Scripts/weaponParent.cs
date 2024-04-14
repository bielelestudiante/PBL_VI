using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponParen : MonoBehaviour
{

    [SerializeField]
    private GameObject _Punta;
    [SerializeField]
    private GameObject _Base;
    [SerializeField]
    private GameObject _trailMesh;
    [SerializeField]
    private int _trailFrameLength;

    private Mesh _mesh;
    private Vector3[] _vertices;
    private int[] _triangles;
    private int _frameCount;
    private Vector3 _previousPuntaPositions;
    private Vector3 _previousBasePosition;

    private const int NUM_VERTICES = 12;

    // Start is called before the first frame update
    void Start()
    {
        _mesh = new Mesh();
        _trailMesh.GetComponent<MeshFilter>().mesh = _mesh;

        _vertices = new Vector3[_trailFrameLength * NUM_VERTICES];
        _triangles = new int[_vertices.Length];

        _previousPuntaPositions = _Punta.transform.position;
        _previousBasePosition = _Base.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (_frameCount == (_trailFrameLength * NUM_VERTICES))
        {
            _frameCount = 0;
        }

        int index = _frameCount * NUM_VERTICES;

        _vertices[index] = _Base.transform.position;
        _vertices[index + 1] = _Punta.transform.position;
        _vertices[index + 2] = _previousPuntaPositions;

        _vertices[index + 3] = _Base.transform.position;
        _vertices[index + 4] = _previousPuntaPositions;
        _vertices[index + 5] = _Punta.transform.position;

        _vertices[index + 6] = _previousPuntaPositions;
        _vertices[index + 7] = _Base.transform.position;
        _vertices[index + 8] = _previousBasePosition;

        _vertices[index + 9] = _previousPuntaPositions;
        _vertices[index + 10] = _previousBasePosition;
        _vertices[index + 11] = _Base.transform.position;

        _triangles[index] = index;
        _triangles[index + 1] = index + 1;
        _triangles[index + 2] = index + 2;
        _triangles[index + 3] = index + 3;
        _triangles[index + 4] = index + 4;
        _triangles[index + 5] = index + 5;
        _triangles[index + 6] = index + 6;
        _triangles[index + 7] = index + 7;
        _triangles[index + 8] = index + 8;
        _triangles[index + 9] = index + 9;
        _triangles[index + 10] = index + 10;
        _triangles[index + 11] = index + 11;

        _mesh.vertices = _vertices;
        _mesh.triangles = _triangles;

        _previousPuntaPositions = _Punta.transform.position;
        _previousBasePosition = _Base.transform.position;

        _frameCount++;
    }
}
