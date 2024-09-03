using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MeshTransformation : MonoBehaviour
{
    MeshFilter _meshFilter;
    MeshCollider _meshCollider;
    Vector3[] vertices;
    private List<GameObject> _markerList = new List<GameObject>();
    private Vector3 targetVertice;
    void Awake()
    {
        _meshFilter = GetComponent<MeshFilter>();
        _meshCollider = GetComponent<MeshCollider>();

        vertices = _meshFilter.mesh.vertices;
        for (int i = 0; i < vertices.Length; i++)
        {
            Debug.Log($"psition {i} = {vertices[i]}");
            if (vertices[i].y > 0)
            {
                vertices[i] += new Vector3(0.3f, 0.3f, 0);
            }
        }
        _meshFilter.mesh.vertices = vertices;
        _meshCollider.sharedMesh = _meshFilter.mesh;
    }

    public void SetMarkerList(List<GameObject> markerList)
    {
        _markerList = markerList;
    }
    public void SelectVertice(Vector3 position)
    {
        vertices = _meshFilter.mesh.vertices;

        targetVertice = vertices
            .OrderBy(p => Vector3.Distance(p, position))
            .FirstOrDefault();
    }
    public void UpdateMesh(Vector3 position)
    {
        if (vertices == null || targetVertice == default(Vector3))
        {
            Debug.LogWarning("Vertices or target vertex not set.");
            return;
        }
        var newVertices = vertices.Select(p => p == targetVertice ? position : p).ToArray();
        _meshFilter.mesh.vertices = newVertices;
        _meshFilter.mesh.RecalculateBounds();
        _meshFilter.mesh.RecalculateNormals();
        _meshCollider.sharedMesh = _meshFilter.mesh;
    }
}
