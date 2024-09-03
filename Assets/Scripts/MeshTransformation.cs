using UnityEngine;
using System.Linq;

public class MeshTransformation : MonoBehaviour
{
    private MeshFilter _meshFilter;
    private MeshCollider _meshCollider;
    private Vector3[] _vertices;
    private Vector3 _targetVertice;

    void Awake()
    {
        _meshFilter = GetComponent<MeshFilter>();
        _meshCollider = GetComponent<MeshCollider>();

        _vertices = _meshFilter.mesh.vertices;
        for (int i = 0; i < _vertices.Length; i++)
        {
            if (_vertices[i].y > 0)
            {
                _vertices[i] += new Vector3(0.3f, 0.3f, 0);
            }
        }
        _meshFilter.mesh.vertices = _vertices;
        _meshCollider.sharedMesh = _meshFilter.mesh;
    }

    // 選択している頂点を決定
    public void SelectVertice(Vector3 position)
    {
        _vertices = _meshFilter.mesh.vertices;

        _targetVertice = _vertices
            .OrderBy(p => Vector3.Distance(p, position))
            .FirstOrDefault();
    }

    // メッシュを更新
    public void UpdateMesh(Vector3 position)
    {
        if (_vertices == null || _targetVertice == default(Vector3))
        {
            Debug.LogWarning("_vertices or target vertex not set.");
            return;
        }

        var new_vertices = _vertices.Select(p => p == _targetVertice ? position : p).ToArray();

        _meshFilter.mesh.vertices = new_vertices;
        _meshFilter.mesh.RecalculateBounds();
        _meshFilter.mesh.RecalculateNormals();
        _meshCollider.sharedMesh = _meshFilter.mesh;
    }
}
