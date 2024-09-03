using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class VerticesMarkerGenerater : MonoBehaviour
{
    [SerializeField]
    private GameObject _marker;
    MeshFilter _meshFilter;
    Vector3 _originVector;
    private List<GameObject> _markerList = new List<GameObject>();
    
    void Start()
    {
        _meshFilter = GetComponent<MeshFilter>();
        _originVector = transform.position;
        InstatiateMarker();
    }

    // マーカーを生成する処理
    private void InstatiateMarker()
    {
        Vector3[] vertices = _meshFilter.mesh.vertices;
        vertices = vertices.Distinct().ToArray();

        foreach (var item in vertices)
        {
            var _maker = Instantiate(_marker, _originVector + item, Quaternion.identity,gameObject.transform);
            _markerList.Add(_maker);
        }
    }
}
